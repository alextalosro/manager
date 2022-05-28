using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;

namespace EcolorProductionManager
{
    public class OPCUAClass
    {
        public string ServerAddress { get; set; }
        public string ServerPortNumber { get; set; }
        public bool SecurityEnabled { get; set; }
        public string MyApplicationName { get; set; }
        public Session OPCSession { get; set; }
        public string OPCNameSpace { get; set; }
        public Dictionary<string, TagClass> TagList { get; set; }

        public bool SessionRenewalRequired { get; set; }
        public double SessionRenewalPeriodMins { get; set; }
        public DateTime LastTimeSessionRenewed { get; set; }
        public DateTime LastTimeOPCServerFoundAlive { get; set; }
        public bool ClassDisposing { get; set; }
        public bool InitialisationCompleted { get; set; }
        private Thread RenewerTHread { get; set; }

        public OPCUAClass(string serverAddres, string serverport, Dictionary<string, TagClass> taglist, bool sessionrenewalRequired, double sessionRenewalMinutes, string nameSpace)
        {
            ServerAddress = serverAddres;
            ServerPortNumber = serverport;
            MyApplicationName = "InterLock Client";
            TagList = taglist;
            SessionRenewalRequired = sessionrenewalRequired;
            SessionRenewalPeriodMins = sessionRenewalMinutes;
            OPCNameSpace = nameSpace;
            LastTimeOPCServerFoundAlive = DateTime.Now;
            InitializeOPCUAClient();

            if (SessionRenewalRequired)
            {
                LastTimeSessionRenewed = DateTime.Now;
                RenewerTHread = new Thread(renewSessionThread);
                RenewerTHread.Start();
            }
        }

        //class destructor
        ~OPCUAClass()
        {

            ClassDisposing = true;
            try
            {

                OPCSession.Close();
                OPCSession.Dispose();
                OPCSession = null;
                RenewerTHread.Abort();
            }
            catch { }

        }

        private void renewSessionThread()
        {
            while (!ClassDisposing)
            {
                if ((DateTime.Now - LastTimeSessionRenewed).TotalMinutes > SessionRenewalPeriodMins
                    || (DateTime.Now - LastTimeOPCServerFoundAlive).TotalSeconds > 60)
                {
                    Console.WriteLine("Renewing Session");
                    try
                    {
                        OPCSession.Close();
                        OPCSession.Dispose();
                    }
                    catch { }
                    InitializeOPCUAClient();
                    LastTimeSessionRenewed = DateTime.Now;

                }
                Thread.Sleep(2000);

            }

        }

        public void InitializeOPCUAClient()
        {
            //Console.WriteLine("Step 1 - Create application configuration and certificate.");
            var config = new ApplicationConfiguration()
            {
                ApplicationName = MyApplicationName,
                ApplicationUri = Utils.Format(@"urn:{0}:" + MyApplicationName + "", ServerAddress),
                ApplicationType = ApplicationType.Client,
                SecurityConfiguration = new SecurityConfiguration
                {
                    ApplicationCertificate = new CertificateIdentifier { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\MachineDefault", SubjectName = Utils.Format(@"CN={0}, DC={1}", MyApplicationName, ServerAddress) },
                    TrustedIssuerCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\UA Certificate Authorities" },
                    TrustedPeerCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\UA Applications" },
                    RejectedCertificateStore = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\RejectedCertificates" },
                    AutoAcceptUntrustedCertificates = true,
                    AddAppCertToTrustedStore = true
                },
                TransportConfigurations = new TransportConfigurationCollection(),
                TransportQuotas = new TransportQuotas { OperationTimeout = 15000 },
                ClientConfiguration = new ClientConfiguration { DefaultSessionTimeout = 60000 },
                TraceConfiguration = new TraceConfiguration()
            };
            config.Validate(ApplicationType.Client).GetAwaiter().GetResult();
            if (config.SecurityConfiguration.AutoAcceptUntrustedCertificates)
            {
                config.CertificateValidator.CertificateValidation += (s, e) => { e.Accept = (e.Error.StatusCode == StatusCodes.BadCertificateUntrusted); };
            }

            var application = new ApplicationInstance
            {
                ApplicationName = MyApplicationName,
                ApplicationType = ApplicationType.Client,
                ApplicationConfiguration = config
            };
            application.CheckApplicationInstanceCertificate(false, 2048).GetAwaiter().GetResult();


            //string serverAddress = Dns.GetHostName();
            string serverAddress = ServerAddress; ;
            var selectedEndpoint = CoreClientUtils.SelectEndpoint("opc.tcp://" + serverAddress + ":" + ServerPortNumber + "", useSecurity: SecurityEnabled);

            // Console.WriteLine($"Step 2 - Create a session with your server: {selectedEndpoint.EndpointUrl} ");
            OPCSession = Session.Create(config, new ConfiguredEndpoint(null, selectedEndpoint, EndpointConfiguration.Create(config)), false, "", 60000, new UserIdentity(), null).GetAwaiter().GetResult();
            {

                //Console.WriteLine("Step 4 - Create a subscription. Set a faster publishing interval if you wish.");
                var subscription = new Subscription(OPCSession.DefaultSubscription) { PublishingInterval = 1000 };

                //Console.WriteLine("Step 5 - Add a list of items you wish to monitor to the subscription.");
                var list = new List<MonitoredItem> { };
                //list.Add(new MonitoredItem(subscription.DefaultItem) { DisplayName = "M0404.CPU945.iBatchOutput", StartNodeId = "ns=2;s=M0404.CPU945.iBatchOutput" });

                list.Add(new MonitoredItem(subscription.DefaultItem) { DisplayName = "ServerStatusCurrentTime", StartNodeId = "i=2258" });

                foreach (KeyValuePair<string, TagClass> td in TagList)
                {
                    list.Add(new MonitoredItem(subscription.DefaultItem) { DisplayName = td.Value.DisplayName, StartNodeId = "ns=" + OPCNameSpace + ";s=" + td.Value.NodeID + "" });

                }

                list.ForEach(i => i.Notification += OnTagValueChange);
                subscription.AddItems(list);

                //Console.WriteLine("Step 6 - Add the subscription to the session.");
                OPCSession.AddSubscription(subscription);
                subscription.Create();

            }




        }

        public class TagClass
        {

            public TagClass(string displayName, string nodeID)
            {
                DisplayName = displayName;
                NodeID = nodeID;

            }

            public DateTime LastUpdatedTime { get; set; }

            public DateTime LastSourceTimeStamp { get; set; }


            public string StatusCode { get; set; }

            public string LastGoodValue { get; set; }
            public string CurrentValue { get; set; }
            public string NodeID { get; set; }

            public string DisplayName { get; set; }


        }

        private void OnTagValueChange(MonitoredItem item, MonitoredItemNotificationEventArgs e)
        {

            foreach (var value in item.DequeueValues())
            {
                if (item.DisplayName == "ServerStatusCurrentTime")
                {
                    LastTimeOPCServerFoundAlive = value.SourceTimestamp.ToLocalTime();

                }
                if (item.DisplayName == "TestLiniaA")
                {

                }
                else
                {
                    if (value.Value != null)
                        Console.WriteLine("{0}: {1}, {2}, {3}", item.DisplayName, value.Value.ToString(), value.SourceTimestamp.ToLocalTime(), value.StatusCode);
                    else
                        Console.WriteLine("{0}: {1}, {2}, {3}", item.DisplayName, "Null Value", value.SourceTimestamp, value.StatusCode);

                    if (TagList.ContainsKey(item.DisplayName))
                    {
                        if (value.Value != null)
                        {
                            TagList[item.DisplayName].LastGoodValue = value.Value.ToString();
                            TagList[item.DisplayName].CurrentValue = value.Value.ToString();
                            TagList[item.DisplayName].LastUpdatedTime = DateTime.Now;
                            TagList[item.DisplayName].LastSourceTimeStamp = value.SourceTimestamp.ToLocalTime();
                            TagList[item.DisplayName].StatusCode = value.StatusCode.ToString();

                        }
                        else
                        {
                            TagList[item.DisplayName].StatusCode = value.StatusCode.ToString();
                            TagList[item.DisplayName].CurrentValue = null;

                        }

                    }

                }

            }
            InitialisationCompleted = true;
        }


        /// <summary>
        /// write a note to server(you should use try catch)
        /// </summary>
        /// <typeparam name="T">The type of tag to write on</typeparam>
        /// <param name="tag">节点名称</param>
        /// <param name="value">值</param>
        /// <returns>if success True,otherwise False</returns>
        public bool WriteNode<T>(string tag, T value)
        {
            WriteValue valueToWrite = new WriteValue()
            {
                NodeId = new NodeId(tag),
                AttributeId = Attributes.Value
            };
            valueToWrite.Value.Value = value;
            valueToWrite.Value.StatusCode = StatusCodes.Good;
            valueToWrite.Value.ServerTimestamp = DateTime.MinValue;
            valueToWrite.Value.SourceTimestamp = DateTime.MinValue;

            WriteValueCollection valuesToWrite = new WriteValueCollection
            {
                valueToWrite
            };

            // 写入当前的值

            OPCSession.Write(
                null,
                valuesToWrite,
                out StatusCodeCollection results,
                out DiagnosticInfoCollection diagnosticInfos);

            ClientBase.ValidateResponse(results, valuesToWrite);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, valuesToWrite);

            if (StatusCode.IsBad(results[0]))
            {
                throw new ServiceResultException(results[0]);
            }

            return !StatusCode.IsBad(results[0]);
        }
    }
}
