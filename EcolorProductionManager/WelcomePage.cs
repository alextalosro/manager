using Opc.Ua.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace EcolorProductionManager
{
    public partial class WelcomePage : Form
    {
        //Connection status
        public static bool isClientConnected;

        //creating a object that encapsulates the netire OPC UA Server related work
        OPCUAClass myOPCUAServer;

        //creating a dictionary of Tags that would be captured from the OPC UA Server
        Dictionary<String, OPCUAClass.TagClass> TagList = new Dictionary<String, OPCUAClass.TagClass>();

        //Create reasomModal instance
        ReasonModal reasonModal = new ReasonModal();

        //Server namespace
        string nameSpace = "2";

        //Session renewal interval
        int renewSessionAfterMinutes = 1;

        //Session renewal
        bool isSessionRenewalRequired = true;

        //Server address
        string serverAddress = "127.0.0.1";
        string serverPort = "49320";

        public WelcomePage()
        {
            InitializeComponent();


            try
            {
                //Tag list for read async subscription.
                TagList.Add("TestLiniaA", new OPCUAClass.TagClass("TestLiniaA", "Ambalaj Linia A.Linia A PLC.TestLiniaA"));
                TagList.Add("Interlock", new OPCUAClass.TagClass("Interlock", "Ambalaj Linia A.Linia A PLC.Interlock"));
                TagList.Add("Anthon 2 Interlock Scanare", new OPCUAClass.TagClass("Anthon 2 Interlock Scanare", "Anthon 2.Anthon 2 Alim PLC.Anthon 2 Interlock Scanare"));
                TagList.Add("Anthon 3 Interlock Scanare", new OPCUAClass.TagClass("Anthon 3 Interlock Scanare", "Anthon 3.Anthon 3 Alim PLC.Anthon 3 Interlock Scanare"));
                TagList.Add("BedBox Interlock", new OPCUAClass.TagClass("BedBox Interlock", "BedBox.BedBoxPLC.BedBox Interlock"));
                TagList.Add("Biesse Interlock Scanare", new OPCUAClass.TagClass("Biesse Interlock Scanare", "Biesse.Biesse PLC.Biesse Interlock Scanare"));
                TagList.Add("Interlock", new OPCUAClass.TagClass("Interlock", "Debitare_folie.PLC_debitare_folie.Interlock"));
                TagList.Add("Homag 1 Interlock Scanare", new OPCUAClass.TagClass("Homag 1 Interlock Scanare", "Homag 1.Homag 1 PLC.Homag 1 Interlock Scanare"));
                TagList.Add("Homag 2 Interlock Scanare Infeed 2", new OPCUAClass.TagClass("Homag 2 Interlock Scanare Infeed 2", "Homag 2.Homag 2 Infeed PLC.Homag 2 Interlock Scanare Infeed 2"));
                TagList.Add("Homag 2 Interlock Scanare Infeed 1", new OPCUAClass.TagClass("Homag 2 Interlock Scanare Infeed 1", "Homag 2.Homag 2 Infeed PLC.Homag 2 Interlock Scanare Infeed 1"));
                TagList.Add("Homag 3 Interlock Scanare Infeed 2", new OPCUAClass.TagClass("Homag 3 Interlock Scanare Infeed 2", "Homag 3.Homag 3 Infeed PLC.Homag 3 Interlock Scanare Infeed 2"));
                TagList.Add("Homag 3 Interlock Scanare Infeed 1", new OPCUAClass.TagClass("Homag 3 Interlock Scanare Infeed 1", "Homag 3.Homag 3 Infeed PLC.Homag 3 Interlock Scanare Infeed 1"));
                TagList.Add("Homag 4 Interlock Scanare", new OPCUAClass.TagClass("Homag 4 Interlock Scanare", "Homag 4.Homag 4 Infeed PLC.Homag 4 Interlock Scanare"));
                TagList.Add("Homag 5 Interlock Scanare", new OPCUAClass.TagClass("Homag 5 Interlock Scanare", "Homag 5.Homag 5 Infeed PLC.Homag 5 Interlock Scanare"));
                TagList.Add("Homag 6 Interlock Scanare Infeed 2", new OPCUAClass.TagClass("Homag 6 Interlock Scanare Infeed 2", "Homag 6.Homag 6 Infeed PLC.Homag 6 Interlock Scanare Infeed 2"));
                TagList.Add("Homag 6 Interlock Scanare Infeed 1", new OPCUAClass.TagClass("Homag 6 Interlock Scanare Infeed 1", "Homag 6.Homag 6 Infeed PLC.Homag 6 Interlock Scanare Infeed 1"));
                TagList.Add("Homag 7 Interlock Scanare", new OPCUAClass.TagClass("Homag 7 Interlock Scanare", "Homag 7.Homag 7 Infeed PLC.Homag 7 Interlock Scanare"));
                TagList.Add("Koch 1 Interlock Scanare", new OPCUAClass.TagClass("Koch 1 Interlock Scanare", "Koch 1.Koch 1 PLC.Koch 1 Interlock Scanare"));
                TagList.Add("Koch 2 Interlock Scanare", new OPCUAClass.TagClass("Koch 2 Interlock Scanare", "Koch 2.Koch 2 PLC.Koch 2 Interlock Scanare"));
                TagList.Add("Output Interlock", new OPCUAClass.TagClass("Output Interlock", "Koch 3.Interlock Koch 3.Output Interlock"));
                TagList.Add("L2 Interlock Scanare", new OPCUAClass.TagClass("L2 Interlock Scanare", "L2_Caserat.L2 PLC.L2 Interlock Scanare"));
                TagList.Add("L3 Interlock Scanare", new OPCUAClass.TagClass("L3 Interlock Scanare", "L3_Caserat.L3 PLC.L3 Interlock Scanare"));
                TagList.Add("L4_Interlock_scanare", new OPCUAClass.TagClass("L4_Interlock_scanare", "L4_Caserat.L4 PLC.L4_Interlock_scanare"));
                TagList.Add("L5 Caserat Interlock Scanare", new OPCUAClass.TagClass("L5 Caserat Interlock Scanare", "L5 Caserat.L5 Caserat PLC.L5 Caserat Interlock Scanare"));
                TagList.Add("Output interlock 2 dreapta", new OPCUAClass.TagClass("Output interlock 2 dreapta", "Linia 1 caserat.Adam L Caserat 1.Output interlock 2 dreapta"));
                TagList.Add("Output interlock 1 stanga", new OPCUAClass.TagClass("Output interlock 1 stanga", "Linia 1 caserat.Adam L Caserat 1.Output interlock 1 stanga"));
                TagList.Add("Output interlock", new OPCUAClass.TagClass("Output interlock", "Process 2.Interlock Process 2.Output interlock"));
                TagList.Add("Tivox interlock scanare", new OPCUAClass.TagClass("Tivox interlock scanare", "Tivox.Tivox PLC.Tivox interlock scanare"));
                TagList.Add("Weeke 5 Interlock Scanare", new OPCUAClass.TagClass("Weeke 5 Interlock Scanare", "Weeke 5.Weeke 5 PLC.Weeke 5 Interlock Scanare"));

                myOPCUAServer = new OPCUAClass(serverAddress, serverPort, TagList, isSessionRenewalRequired, renewSessionAfterMinutes, nameSpace);
                isClientConnected = true;
            }
            catch (Exception ex)
            {
                string aditionalErrorMessage = "Conexiunea cu serverul nu a putut fi stabilita !";
                if (ex.Message == "Error establishing a connection: BadNotConnected")
                {
                    MessageBox.Show(ex.Message + " " + "Details: " + aditionalErrorMessage);
                    isClientConnected = false;
                }
                else
                {
                    MessageBox.Show(ex.Message);
                    isClientConnected = false;
                }
            }

            //Background worker
            backgroundWorker1.WorkerReportsProgress = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {   //Testing purpose
            //var tagCurrentValue2 = TagList["Anthon 2 Interlock Scanare"].CurrentValue;
            //var tagLastGoodValue2 = TagList["Anthon 2 Interlock Scanare"].LastGoodValue;
            //var lastTimeTagupdated2 = TagList["Anthon 2 Interlock Scanare"].LastUpdatedTime;
            while (true)
            {
                //Linia A
                var tagCurrentValue0 = TagList["TestLiniaA"].CurrentValue;
                if (tagCurrentValue0 != null)
                {
                    if (tagCurrentValue0.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonLiniaATestLiniaALock.BackColor = Color.LightGreen;
                            buttonLiniaATestLiniaAUnlock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonLiniaATestLiniaAUnlock.BackColor = Color.LightGreen;
                            buttonLiniaATestLiniaALock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                var tagCurrentValue1 = TagList["Interlock"].CurrentValue;
                if (tagCurrentValue1 != null)
                {
                    if (tagCurrentValue1.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonLiniaAInterlockLock.BackColor = Color.LightGreen;
                            buttonLiniaAInterlockUnlock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonLiniaAInterlockLock.BackColor = Color.LightGreen;
                            buttonLiniaAInterlockUnlock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Anthon 2
                var tagCurrentValue2 = TagList["Anthon 2 Interlock Scanare"].CurrentValue;
                if (tagCurrentValue2 != null)
                {
                    if (tagCurrentValue2.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonAnthon2InterlockScanareLock.BackColor = Color.LightGreen;
                            buttonAnthon2InterlockScanareUnlock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonAnthon2InterlockScanareUnlock.BackColor = Color.LightGreen;
                            buttonAnthon2InterlockScanareLock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Anthon 3
                var tagCurrentValue3 = TagList["Anthon 3 Interlock Scanare"].CurrentValue;
                if (tagCurrentValue3 != null)
                {
                    if (tagCurrentValue3.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonAnthon3InterlockScanareLock.BackColor = Color.LightGreen;
                            buttonAnthon3InterlockScanareUnlock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonAnthon3InterlockScanareUnlock.BackColor = Color.LightGreen;
                            buttonAnthon3InterlockScanareLock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Bedbox
                var tagCurrentValue4 = TagList["BedBox Interlock"].CurrentValue;
                if (tagCurrentValue4 != null)
                {
                    if (tagCurrentValue4.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonBedBoxBedBoxInterlockLock.BackColor = Color.LightGreen;
                            buttonBedBoxBedBoxInterlockUnlock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonBedBoxBedBoxInterlockUnlock.BackColor = Color.LightGreen;
                            buttonBedBoxBedBoxInterlockLock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Biesses
                var tagCurrentValue5 = TagList["Biesse Interlock Scanare"].CurrentValue;
                if (tagCurrentValue5 != null)
                {
                    if (tagCurrentValue5.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonBiesseInterlockScanareLock.BackColor = Color.LightGreen;
                            buttonBiesseInterlockScanareUnlock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonBiesseInterlockScanareUnlock.BackColor = Color.LightGreen;
                            buttonBiesseInterlockScanareLock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Debitare folie
                var tagCurrentValue6 = TagList["Interlock"].CurrentValue;
                if (tagCurrentValue6 != null)
                {
                    if (tagCurrentValue6.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonDebitareFolieInterlockLock.BackColor = Color.LightGreen;
                            buttonDebitareFolieInterlockUnlock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonDebitareFolieInterlockUnlock.BackColor = Color.LightGreen;
                            buttonDebitareFolieInterlockLock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Homag 1
                var tagCurrentValue7 = TagList["Homag 1 Interlock Scanare"].CurrentValue;
                if (tagCurrentValue7 != null)
                {
                    if (tagCurrentValue7.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonHomag1InterlockScanareLock.BackColor = Color.LightGreen;
                            buttonHomag1InterlockScanareUnlock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonHomag1InterlockScanareUnlock.BackColor = Color.LightGreen;
                            buttonHomag1InterlockScanareLock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Homag 2
                var tagCurrentValue8 = TagList["Homag 2 Interlock Scanare Infeed 2"].CurrentValue;
                if (tagCurrentValue8 != null)
                {
                    if (tagCurrentValue8.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonHomag2InterlockScanareInfeed2Lock.BackColor = Color.LightGreen;
                            buttonHomag2InterlockScanareInfeed2Unlock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonHomag2InterlockScanareInfeed2Unlock.BackColor = Color.LightGreen;
                            buttonHomag2InterlockScanareInfeed2Lock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                var tagCurrentValue9 = TagList["Homag 2 Interlock Scanare Infeed 1"].CurrentValue;
                if (tagCurrentValue9 != null)
                {
                    if (tagCurrentValue9.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonHomag2InterlockScanareInfeed1Lock.BackColor = Color.LightGreen;
                            buttonHomag2InterlockScanareInfeed1Unlock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            buttonHomag2InterlockScanareInfeed1Unlock.BackColor = Color.LightGreen;
                            buttonHomag2InterlockScanareInfeed1Lock.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Homag 3
                var tagCurrentValue10 = TagList["Homag 3 Interlock Scanare Infeed 2"].CurrentValue;
                if (tagCurrentValue10 != null)
                {
                    if (tagCurrentValue10.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button25.BackColor = Color.LightGreen;
                            button26.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button26.BackColor = Color.LightGreen;
                            button25.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                var tagCurrentValue11 = TagList["Homag 3 Interlock Scanare Infeed 1"].CurrentValue;
                if (tagCurrentValue11 != null)
                {
                    if (tagCurrentValue11.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button35.BackColor = Color.LightGreen;
                            button36.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button36.BackColor = Color.LightGreen;
                            button35.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Homag 4
                var tagCurrentValue12 = TagList["Homag 4 Interlock Scanare"].CurrentValue;
                if (tagCurrentValue12 != null)
                {
                    if (tagCurrentValue12.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button37.BackColor = Color.LightGreen;
                            button38.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button38.BackColor = Color.LightGreen;
                            button37.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Homag 5
                var tagCurrentValue13 = TagList["Homag 5 Interlock Scanare"].CurrentValue;
                if (tagCurrentValue13 != null)
                {
                    if (tagCurrentValue13.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button39.BackColor = Color.LightGreen;
                            button40.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button40.BackColor = Color.LightGreen;
                            button39.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Homag 6
                var tagCurrentValue14 = TagList["Homag 6 Interlock Scanare Infeed 2"].CurrentValue;
                if (tagCurrentValue14 != null)
                {
                    if (tagCurrentValue14.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button41.BackColor = Color.LightGreen;
                            button42.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button42.BackColor = Color.LightGreen;
                            button41.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                var tagCurrentValue15 = TagList["Homag 6 Interlock Scanare Infeed 1"].CurrentValue;
                if (tagCurrentValue15 != null)
                {
                    if (tagCurrentValue15.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button51.BackColor = Color.LightGreen;
                            button52.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button52.BackColor = Color.LightGreen;
                            button51.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Homag 7
                var tagCurrentValue16 = TagList["Homag 7 Interlock Scanare"].CurrentValue;
                if (tagCurrentValue16 != null)
                {
                    if (tagCurrentValue16.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button53.BackColor = Color.LightGreen;
                            button54.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button54.BackColor = Color.LightGreen;
                            button53.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Koch 1
                var tagCurrentValue17 = TagList["Koch 1 Interlock Scanare"].CurrentValue;
                if (tagCurrentValue17 != null)
                {
                    if (tagCurrentValue17.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button55.BackColor = Color.LightGreen;
                            button56.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button56.BackColor = Color.LightGreen;
                            button55.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Koch 2
                var tagCurrentValue18 = TagList["Koch 2 Interlock Scanare"].CurrentValue;
                if (tagCurrentValue18 != null)
                {
                    if (tagCurrentValue18.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button57.BackColor = Color.LightGreen;
                            button58.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button58.BackColor = Color.LightGreen;
                            button57.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Koch 3
                var tagCurrentValue19 = TagList["Output Interlock"].CurrentValue;
                if (tagCurrentValue19 != null)
                {
                    if (tagCurrentValue19.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button59.BackColor = Color.LightGreen;
                            button60.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button60.BackColor = Color.LightGreen;
                            button59.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //L2 Caserat
                var tagCurrentValue20 = TagList["L2 Interlock Scanare"].CurrentValue;
                if (tagCurrentValue20 != null)
                {
                    if (tagCurrentValue20.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button61.BackColor = Color.LightGreen;
                            button62.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button62.BackColor = Color.LightGreen;
                            button61.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //L3 Caserat
                var tagCurrentValue21 = TagList["L3 Interlock Scanare"].CurrentValue;
                if (tagCurrentValue21 != null)
                {
                    if (tagCurrentValue21.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button63.BackColor = Color.LightGreen;
                            button64.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button64.BackColor = Color.LightGreen;
                            button63.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //L4 Caserat
                var tagCurrentValue22 = TagList["L4_Interlock_scanare"].CurrentValue;
                if (tagCurrentValue22 != null)
                {
                    if (tagCurrentValue22.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button65.BackColor = Color.LightGreen;
                            button66.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button66.BackColor = Color.LightGreen;
                            button65.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //L5 Caserat
                var tagCurrentValue23 = TagList["L5 Caserat Interlock Scanare"].CurrentValue;
                if (tagCurrentValue23 != null)
                {
                    if (tagCurrentValue23.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button67.BackColor = Color.LightGreen;
                            button68.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button68.BackColor = Color.LightGreen;
                            button67.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Linia 1 Caserat
                var tagCurrentValue24 = TagList["Output interlock 2 dreapta"].CurrentValue;
                if (tagCurrentValue24 != null)
                {
                    if (tagCurrentValue24.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button72.BackColor = Color.LightGreen;
                            button71.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button71.BackColor = Color.LightGreen;
                            button72.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                var tagCurrentValue25 = TagList["Output interlock 1 stanga"].CurrentValue;
                if (tagCurrentValue25 != null)
                {
                    if (tagCurrentValue25.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button70.BackColor = Color.LightGreen;
                            button69.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button69.BackColor = Color.LightGreen;
                            button70.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Process 2
                var tagCurrentValue26 = TagList["Output interlock"].CurrentValue;
                if (tagCurrentValue26 != null)
                {
                    if (tagCurrentValue26.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button73.BackColor = Color.LightGreen;
                            button74.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button74.BackColor = Color.LightGreen;
                            button73.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Tivox
                var tagCurrentValue27 = TagList["Tivox interlock scanare"].CurrentValue;
                if (tagCurrentValue27 != null)
                {
                    if (tagCurrentValue27.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button75.BackColor = Color.LightGreen;
                            button76.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button76.BackColor = Color.LightGreen;
                            button75.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }

                //Weeke
                var tagCurrentValue28 = TagList["Weeke 5 Interlock Scanare"].CurrentValue;
                if (tagCurrentValue28 != null)
                {
                    if (tagCurrentValue28.ToString() == "True")
                    {
                        this.Invoke(new Action(() =>
                        {
                            button77.BackColor = Color.LightGreen;
                            button78.BackColor = SystemColors.ControlLight;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            button78.BackColor = Color.LightGreen;
                            button77.BackColor = SystemColors.ControlLight;
                        }));
                    }
                }
            }
        }

        private void WelcomePage_Load(object sender, EventArgs e)
        {
            registerButton.Enabled = LoginForm.isCurrentUserAdmin;
            logButton.Enabled = LoginForm.isCurrentUserAdmin;
            loggedUsername.Text = loggedUsername.Text + LoginForm.loggedUserFullName;
            this.Text = "DASHBOARD BYPASS INTERLOCK";

            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void WelcomePage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
            this.Dispose();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            UserRegistrationForm userRegistrationForm = new UserRegistrationForm();
            userRegistrationForm.Show();
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            LogForm logForm = new LogForm();
            logForm.Show();
        }

        private void AddLogItemToDatabase(ReasonModal reasonModal, Label label, Button button)
        {
            DateTime dateTime = DateTime.Now;
            string fullName = LoginForm.loggedUserFullName;
            string action = $"Statusul liniei ({label.Text.ToUpper()}) a fost schimbat in {button.Text.ToUpper()}.";
            var reason = reasonModal.Reason;

            //Query the db for username and password;
            string mainconn = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "INSERT INTO [dbo].[log_item] VALUES (@date_time, @fullname, @action, @reason)";
            sqlconn.Open();
            SqlCommand sqlCmd = new SqlCommand(sqlquery, sqlconn);
            sqlCmd.Parameters.AddWithValue("@date_time", dateTime);
            sqlCmd.Parameters.AddWithValue("@fullname", fullName);
            sqlCmd.Parameters.AddWithValue("@action", action);
            sqlCmd.Parameters.AddWithValue("@reason", reason);
            sqlCmd.ExecuteNonQuery();

            sqlconn.Close();
        }

        private async void buttonLock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Ambalaj Linia A.Linia A PLC.TestLiniaA", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelLiniaA, buttonLiniaATestLiniaALock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void buttonUnlock_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Ambalaj Linia A.Linia A PLC.TestLiniaA", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelLiniaA, buttonLiniaATestLiniaAUnlock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void buttonLiniaAInterlockLock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Ambalaj Linia A.Linia A PLC.Interlock", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelLiniaA, buttonLiniaAInterlockLock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void buttonLiniaAInterlockUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Ambalaj Linia A.Linia A PLC.Interlock", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelLiniaA, buttonLiniaAInterlockUnlock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button2Lock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Anthon 2.Anthon 2 Alim PLC.Anthon 2 Interlock Scanare", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelAnthon2, buttonAnthon2InterlockScanareLock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button2Unlock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Anthon 2.Anthon 2 Alim PLC.Anthon 2 Interlock Scanare", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelAnthon2, buttonAnthon2InterlockScanareUnlock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void buttonAnthon3InterlockScanareLock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Anthon 3.Anthon 3 Alim PLC.Anthon 3 Interlock Scanare", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelAnthon3, buttonAnthon3InterlockScanareLock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void buttonAnthon3InterlockScanareUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Anthon 3.Anthon 3 Alim PLC.Anthon 3 Interlock Scanare", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelAnthon3, buttonAnthon3InterlockScanareUnlock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void buttonBedBoxBedBoxInterlockLock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=BedBox.BedBoxPLC.BedBox Interlock", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelBedBox, buttonBedBoxBedBoxInterlockLock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void buttonBedBoxBedBoxInterlockUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=BedBox.BedBoxPLC.BedBox Interlock", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelBedBox, buttonBedBoxBedBoxInterlockUnlock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void buttonBiesseInterlockScanareLock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Biesse.Biesse PLC.Biesse Interlock Scanare", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelBiesse, buttonBiesseInterlockScanareLock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void buttonBiesseInterlockScanareUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Biesse.Biesse PLC.Biesse Interlock Scanare", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelBiesse, buttonBiesseInterlockScanareUnlock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void buttonDebitareFolieInterlockLock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Debitare_folie.PLC_debitare_folie.Interlock", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelDebitareFolie, buttonDebitareFolieInterlockLock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void buttonDebitareFolieInterlockUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Debitare_folie.PLC_debitare_folie.Interlock", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelDebitareFolie, buttonDebitareFolieInterlockUnlock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //Homag 1
        private async void buttonHomag1InterlockScanareLock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 1.Homag 1 PLC.Homag 1 Interlock Scanare", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag1, buttonHomag1InterlockScanareLock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void buttonHomag1InterlockScanareUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 1.Homag 1 PLC.Homag 1 Interlock Scanare", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag1, buttonHomag1InterlockScanareUnlock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //Homag 2
        private async void buttonHomag2InterlockScanareInfeed2Lock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 2.Homag 2 Infeed PLC.Homag 2 Interlock Scanare Infeed 2", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag2, buttonHomag2InterlockScanareInfeed2Lock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void buttonHomag2InterlockScanareInfeed2Unlock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 2.Homag 2 Infeed PLC.Homag 2 Interlock Scanare Infeed 2", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag2, buttonHomag2InterlockScanareInfeed2Unlock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void buttonHomag2InterlockScanareInfeed1Lock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 2.Homag 2 Infeed PLC.Homag 2 Interlock Scanare Infeed 1", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag2, buttonHomag2InterlockScanareInfeed1Lock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void buttonHomag2InterlockScanareInfeed1Unlock_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 2.Homag 2 Infeed PLC.Homag 2 Interlock Scanare Infeed 1", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag2, buttonHomag2InterlockScanareInfeed1Unlock);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //Homag 3
        private async void button25_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 3.Homag 3 Infeed PLC.Homag 3 Interlock Scanare Infeed 2", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag3, button25);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button26_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 3.Homag 3 Infeed PLC.Homag 3 Interlock Scanare Infeed 2", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag3, button26);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button35_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 3.Homag 3 Infeed PLC.Homag 3 Interlock Scanare Infeed 1", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag3, button35);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button36_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 3.Homag 3 Infeed PLC.Homag 3 Interlock Scanare Infeed 1", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag3, button36);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //Homag 4
        private async void button37_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 4.Homag 4 Infeed PLC.Homag 4 Interlock Scanare", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag4, button37);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button38_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 4.Homag 4 Infeed PLC.Homag 4 Interlock Scanare", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag4, button37);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //Homag 5
        private async void button39_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 5.Homag 5 Infeed PLC.Homag 5 Interlock Scanare", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag5, button39);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button40_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 5.Homag 5 Infeed PLC.Homag 5 Interlock Scanare", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag5, button40);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //Homag 6
        private async void button41_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 6.Homag 6 Infeed PLC.Homag 6 Interlock Scanare Infeed 2", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag6, button41);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button42_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 6.Homag 6 Infeed PLC.Homag 6 Interlock Scanare Infeed 2", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag6, button42);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button51_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 6.Homag 6 Infeed PLC.Homag 6 Interlock Scanare Infeed 1", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag6, button51);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button52_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 6.Homag 6 Infeed PLC.Homag 6 Interlock Scanare Infeed 1", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag6, button52);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //Homag 7
        private async void button53_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 7.Homag 7 Infeed PLC.Homag 7 Interlock Scanare", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag7, button53);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button54_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Homag 7.Homag 7 Infeed PLC.Homag 7 Interlock Scanare", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag7, button54);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //Koch 1
        private async void button55_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Koch 1.Koch 1 PLC.Koch 1 Interlock Scanare", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelKoch1, button55);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button56_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Koch 1.Koch 1 PLC.Koch 1 Interlock Scanare", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelKoch1, button56);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //Koch 2
        private async void button57_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    //myOPCUAServer.WriteNode("ns=2;s=Koch 2.Koch 2 PLC.Koch 2 Interlock Scanare", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelKoch2, button57);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button58_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Koch 2.Koch 2 PLC.Koch 2 Interlock Scanare", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelKoch2, button58);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //Koch 3
        private async void button59_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Koch 3.Interlock Koch 3.Output Interlock", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelKoch3, button59);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button60_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Koch 3.Interlock Koch 3.Output Interlock", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelKoch3, button60);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //L2 Caserat
        private async void button61_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=L2_Caserat.L2 PLC.L2 Interlock Scanare", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelL2Caserat, button61);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button62_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=L2_Caserat.L2 PLC.L2 Interlock Scanare", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelL2Caserat, button62);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //L3 Caserat
        private async void button63_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=L3_Caserat.L3 PLC.L3 Interlock Scanare", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelL3Caserat, button63);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button64_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=L3_Caserat.L3 PLC.L3 Interlock Scanare", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelL3Caserat, button64);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //L4 Caserat
        private async void button65_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=L4_Caserat.L4 PLC.L4_Interlock_scanare", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelL4Caserat, button65);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button66_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=L4_Caserat.L4 PLC.L4_Interlock_scanare", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelL4Caserat, button66);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //L5 Caserat
        private async void button67_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=L5 Caserat.L5 Caserat PLC.L5 Caserat Interlock Scanare", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelL5Caserat, button67);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button68_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=L5 Caserat.L5 Caserat PLC.L5 Caserat Interlock Scanare", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelL5Caserat, button68);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //Linia 1 Caserat
        private async void button72_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Linia 1 caserat.Adam L Caserat 1.Output interlock 2 dreapta", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelLinia1Caserat, button72);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button71_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Linia 1 caserat.Adam L Caserat 1.Output interlock 2 dreapta", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelLinia1Caserat, button71);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button70_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Linia 1 caserat.Adam L Caserat 1.Output interlock 1 stanga", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelLinia1Caserat, button70);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }
        private async void button69_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Linia 1 caserat.Adam L Caserat 1.Output interlock 1 stanga", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelLinia1Caserat, button69);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //Process 2
        private async void button73_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Process 2.Interlock Process 2.Output interlock", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelProcess2, button73);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        private async void button74_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Process 2.Interlock Process 2.Output interlock", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelProcess2, button74);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //Tivox
        private async void button75_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Tivox.Tivox PLC.Tivox interlock scanare", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelTivox, button75);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        private async void button76_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Tivox.Tivox PLC.Tivox interlock scanare", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelTivox, button76);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        //Weeke
        private async void button77_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Weeke 5.Weeke 5 PLC.Weeke 5 Interlock Scanare", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelWeeke, button77);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        private async void button78_Click(object sender, EventArgs e)
        {
            try
            {
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                    myOPCUAServer.WriteNode("ns=2;s=Weeke 5.Weeke 5 PLC.Weeke 5 Interlock Scanare", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelWeeke, button78);
                    this.Enabled = true;
                }
                else
                {
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }
    }
}
