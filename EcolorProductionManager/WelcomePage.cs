using DataAccessLayer;
using Opc.Ua.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
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
                TagList.Add("Interlock", new OPCUAClass.TagClass("Interlock", "Ambalaj Linia A.Linia A PLC.Dsh bypass"));
                TagList.Add("Anthon 2 Interlock Scanare", new OPCUAClass.TagClass("Anthon 2 Interlock Scanare", "Anthon 2.Anthon 2 Alim PLC.Dsh bypass"));
                TagList.Add("Anthon 3 Interlock Scanare", new OPCUAClass.TagClass("Anthon 3 Interlock Scanare", "Anthon 3.Anthon 3 Alim PLC.Dsh bypass"));
                TagList.Add("BedBox Interlock", new OPCUAClass.TagClass("BedBox Interlock", "BedBox.BedBoxPLC.Dsh bypass"));
                TagList.Add("Biesse Interlock Scanare", new OPCUAClass.TagClass("Biesse Interlock Scanare", "Biesse.Biesse PLC.Dsh bypass"));
                TagList.Add("Interlock2", new OPCUAClass.TagClass("Interlock", "Debitare_folie.PLC_debitare_folie.Dsh bypass"));
                TagList.Add("Homag 1 Interlock Scanare", new OPCUAClass.TagClass("Homag 1 Interlock Scanare", "Homag 1.Homag 1 PLC.Dsh bypass"));
                TagList.Add("Homag 2 Interlock Scanare Infeed 2", new OPCUAClass.TagClass("Homag 2 Interlock Scanare Infeed 2", "Homag 2.Homag 2 Infeed PLC.Dsh bypass 2"));
                TagList.Add("Homag 2 Interlock Scanare Infeed 1", new OPCUAClass.TagClass("Homag 2 Interlock Scanare Infeed 1", "Homag 2.Homag 2 Infeed PLC.Dsh bypass 1"));
                TagList.Add("Homag 3 Interlock Scanare Infeed 2", new OPCUAClass.TagClass("Homag 3 Interlock Scanare Infeed 2", "Homag 3.Homag 3 Infeed PLC.Dsh bypass 2"));
                TagList.Add("Homag 3 Interlock Scanare Infeed 1", new OPCUAClass.TagClass("Homag 3 Interlock Scanare Infeed 1", "Homag 3.Homag 3 Infeed PLC.Dsh bypass 1"));
                TagList.Add("Homag 4 Interlock Scanare", new OPCUAClass.TagClass("Homag 4 Interlock Scanare", "Homag 4.Homag 4 Infeed PLC.Dsh bypass"));
                TagList.Add("Homag 5 Interlock Scanare", new OPCUAClass.TagClass("Homag 5 Interlock Scanare", "Homag 5.Homag 5 Infeed PLC.Dsh bypass"));
                TagList.Add("Homag 6 Interlock Scanare Infeed 2", new OPCUAClass.TagClass("Homag 6 Interlock Scanare Infeed 2", "Homag 6.Homag 6 Infeed PLC.Dsh bypass 2"));
                TagList.Add("Homag 6 Interlock Scanare Infeed 1", new OPCUAClass.TagClass("Homag 6 Interlock Scanare Infeed 1", "Homag 6.Homag 6 Infeed PLC.Dsh bypass 1"));
                TagList.Add("Homag 7 Interlock Scanare", new OPCUAClass.TagClass("Homag 7 Interlock Scanare", "Homag 7.Homag 7 Infeed PLC.Dsh bypass"));
                TagList.Add("Koch 1 Interlock Scanare", new OPCUAClass.TagClass("Koch 1 Interlock Scanare", "Koch 1.Koch 1 PLC.Dsh bypass"));
                TagList.Add("Koch 2 Interlock Scanare", new OPCUAClass.TagClass("Koch 2 Interlock Scanare", "Koch 2.Koch 2 PLC.Dsh bypass"));
                TagList.Add("Output Interlock", new OPCUAClass.TagClass("Output Interlock", "Koch 3.Interlock Koch 3.Dsh bypass"));
                TagList.Add("L2 Interlock Scanare", new OPCUAClass.TagClass("L2 Interlock Scanare", "L2_Caserat.L2 PLC.Dsh bypass"));
                TagList.Add("L3 Interlock Scanare", new OPCUAClass.TagClass("L3 Interlock Scanare", "L3_Caserat.L3 PLC.Dsh bypass"));
                TagList.Add("L4_Interlock_scanare", new OPCUAClass.TagClass("L4_Interlock_scanare", "L4_Caserat.L4 PLC.Dsh bypass"));
                TagList.Add("L5 Caserat Interlock Scanare", new OPCUAClass.TagClass("L5 Caserat Interlock Scanare", "L5 Caserat.L5 Caserat PLC.Dsh bypass"));
                TagList.Add("Output interlock 2 dreapta", new OPCUAClass.TagClass("Output interlock 2 dreapta", "Linia 1 caserat.Adam L Caserat 1.Dsh bypass 2"));
                TagList.Add("Output interlock 1 stanga", new OPCUAClass.TagClass("Output interlock 1 stanga", "Linia 1 caserat.Adam L Caserat 1.Dsh bypass 1"));
                TagList.Add("Output interlock", new OPCUAClass.TagClass("Output interlock", "Process 2.Interlock Process 2.Dsh bypass"));
                TagList.Add("Tivox interlock scanare", new OPCUAClass.TagClass("Tivox interlock scanare", "Tivox.Tivox PLC.Dsh bypass"));
                TagList.Add("Weeke 5 Interlock Scanare", new OPCUAClass.TagClass("Weeke 5 Interlock Scanare", "Weeke 5.Weeke 5 PLC.Dsh bypass"));

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
        }

        private async void WelcomePage_Load(object sender, EventArgs e)
        {
            registerButton.Enabled = LoginForm.isCurrentUserAdmin;
            logButton.Enabled = LoginForm.isCurrentUserAdmin;
            loggedUsername.Text = LoginForm.loggedUserFullName;
            this.Text = "DASHBOARD BYPASS INTERLOCK";


            //Async get status timer.
            Timer timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 500;
            timer.Enabled = true;
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            var results = await Task.Run(() => GetTagsStatus());

            //Linia A
            //Culoare
            buttonLiniaAInterlockLock.BackColor = results[1] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            buttonLiniaAInterlockUnlock.BackColor = results[1] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            buttonLiniaAInterlockLock.Text = results[1] == "True" ? "Bypass Activ" : "Activare";
            buttonLiniaAInterlockUnlock.Text = results[1] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Anthon 2
            //Culoare
            buttonAnthon2InterlockScanareLock.BackColor = results[2] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            buttonAnthon2InterlockScanareUnlock.BackColor = results[2] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            buttonAnthon2InterlockScanareLock.Text = results[2] == "True" ? "Bypass Activ" : "Activare";
            buttonAnthon2InterlockScanareUnlock.Text = results[2] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Anthon 3
            //Culoare
            buttonAnthon3InterlockScanareLock.BackColor = results[3] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            buttonAnthon3InterlockScanareUnlock.BackColor = results[3] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            buttonAnthon3InterlockScanareLock.Text = results[3] == "True" ? "Bypass Activ" : "Activare";
            buttonAnthon3InterlockScanareUnlock.Text = results[3] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //BedBox
            //Culoare
            buttonBedBoxBedBoxInterlockLock.BackColor = results[4] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            buttonBedBoxBedBoxInterlockUnlock.BackColor = results[4] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            buttonBedBoxBedBoxInterlockLock.Text = results[4] == "True" ? "Bypass Activ" : "Activare";
            buttonBedBoxBedBoxInterlockUnlock.Text = results[4] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Biesses
            //Culoare
            buttonBiesseInterlockScanareLock.BackColor = results[5] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            buttonBiesseInterlockScanareUnlock.BackColor = results[5] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            buttonBiesseInterlockScanareLock.Text = results[5] == "True" ? "Bypass Activ" : "Activare";
            buttonBiesseInterlockScanareUnlock.Text = results[5] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Debitare folie
            //Culoare
            buttonDebitareFolieInterlockLock.BackColor = results[6] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            buttonDebitareFolieInterlockUnlock.BackColor = results[6] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            buttonDebitareFolieInterlockLock.Text = results[6] == "True" ? "Bypass Activ" : "Activare";
            buttonDebitareFolieInterlockUnlock.Text = results[6] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Homag 1
            //Culoare
            buttonHomag1InterlockScanareLock.BackColor = results[7] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            buttonHomag1InterlockScanareUnlock.BackColor = results[7] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            buttonHomag1InterlockScanareLock.Text = results[7] == "True" ? "Bypass Activ" : "Activare";
            buttonHomag1InterlockScanareUnlock.Text = results[7] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Homag 2
            //Culoare
            buttonHomag2InterlockScanareInfeed2Lock.BackColor = results[8] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            buttonHomag2InterlockScanareInfeed2Unlock.BackColor = results[8] == "False" ? Color.Red : SystemColors.ControlLight;
            buttonHomag2InterlockScanareInfeed1Lock.BackColor = results[9] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            buttonHomag2InterlockScanareInfeed1Unlock.BackColor = results[9] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            buttonHomag2InterlockScanareInfeed2Lock.Text = results[8] == "True" ? "Bypass Activ" : "Activare";
            buttonHomag2InterlockScanareInfeed2Unlock.Text = results[8] == "False" ? "Bypass Inactiv" : "Dezactivare";
            buttonHomag2InterlockScanareInfeed1Lock.Text = results[9] == "True" ? "Bypass Activ" : "Activare";
            buttonHomag2InterlockScanareInfeed1Unlock.Text = results[9] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Homag 3
            //Culoare
            button25.BackColor = results[10] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button26.BackColor = results[10] == "False" ? Color.Red : SystemColors.ControlLight;
            button35.BackColor = results[11] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button36.BackColor = results[11] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button25.Text = results[10] == "True" ? "Bypass Activ" : "Activare";
            button26.Text = results[10] == "False" ? "Bypass Inactiv" : "Dezactivare";
            button35.Text = results[11] == "True" ? "Bypass Activ" : "Activare";
            button36.Text = results[11] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Homag 4
            //Culoare
            button37.BackColor = results[12] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button38.BackColor = results[12] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button37.Text = results[12] == "True" ? "Bypass Activ" : "Activare";
            button38.Text = results[12] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Homag 5
            //Culoare
            button39.BackColor = results[13] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button40.BackColor = results[13] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button39.Text = results[13] == "True" ? "Bypass Activ" : "Activare";
            button40.Text = results[13] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Homag 6
            //Culoare
            button41.BackColor = results[14] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button42.BackColor = results[14] == "False" ? Color.Red : SystemColors.ControlLight;
            button51.BackColor = results[15] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button52.BackColor = results[15] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button41.Text = results[14] == "True" ? "Bypass Activ" : "Activare";
            button42.Text = results[14] == "False" ? "Bypass Inactiv" : "Dezactivare";
            button51.Text = results[15] == "True" ? "Bypass Activ" : "Activare";
            button52.Text = results[15] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Homag 7
            //Culoare
            button53.BackColor = results[16] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button54.BackColor = results[16] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button53.Text = results[16] == "True" ? "Bypass Activ" : "Activare";
            button54.Text = results[16] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Koch 1
            //Culoare
            button55.BackColor = results[17] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button56.BackColor = results[17] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button55.Text = results[17] == "True" ? "Bypass Activ" : "Activare";
            button56.Text = results[17] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Koch 2
            //Culoare
            button57.BackColor = results[18] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button58.BackColor = results[18] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button57.Text = results[18] == "True" ? "Bypass Activ" : "Activare";
            button58.Text = results[18] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Koch 3
            //Culoare
            button59.BackColor = results[19] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button60.BackColor = results[19] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button59.Text = results[19] == "True" ? "Bypass Activ" : "Activare";
            button60.Text = results[19] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //L2 Caserat
            //Culoare
            button61.BackColor = results[20] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button62.BackColor = results[20] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button61.Text = results[20] == "True" ? "Bypass Activ" : "Activare";
            button62.Text = results[20] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //L3 Caserat
            //Culoare
            button63.BackColor = results[21] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button64.BackColor = results[21] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button63.Text = results[21] == "True" ? "Bypass Activ" : "Activare";
            button64.Text = results[21] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //L4 Caserat
            //Culoare
            button65.BackColor = results[22] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button66.BackColor = results[22] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button65.Text = results[22] == "True" ? "Bypass Activ" : "Activare";
            button66.Text = results[22] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //L5 Caserat
            //Culoare
            button67.BackColor = results[23] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button68.BackColor = results[23] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button67.Text = results[23] == "True" ? "Bypass Activ" : "Activare";
            button68.Text = results[23] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Linia 1 Caserat
            //Culoare
            button72.BackColor = results[24] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button71.BackColor = results[24] == "False" ? Color.Red : SystemColors.ControlLight;
            button70.BackColor = results[25] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button69.BackColor = results[25] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button72.Text = results[24] == "True" ? "Bypass Activ" : "Activare";
            button71.Text = results[24] == "False" ? "Bypass Inactiv" : "Dezactivare";
            button70.Text = results[25] == "True" ? "Bypass Activ" : "Activare";
            button69.Text = results[25] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Process 2
            //Culoare
            button73.BackColor = results[26] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button74.BackColor = results[26] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button73.Text = results[26] == "True" ? "Bypass Activ" : "Activare";
            button74.Text = results[26] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Tivox
            //Culoare
            button75.BackColor = results[27] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button76.BackColor = results[27] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button75.Text = results[27] == "True" ? "Bypass Activ" : "Activare";
            button76.Text = results[27] == "False" ? "Bypass Inactiv" : "Dezactivare";


            //Weeke
            //Culoare
            button77.BackColor = results[28] == "True" ? Color.LightGreen : SystemColors.ControlLight;
            button78.BackColor = results[28] == "False" ? Color.Red : SystemColors.ControlLight;
            //Text
            button77.Text = results[28] == "True" ? "Bypass Activ" : "Activare";
            button78.Text = results[28] == "False" ? "Bypass Inactiv" : "Dezactivare";
        }

        public List<string> GetTagsStatus()
        {
            var results = new List<string>();
            string tagCurrentValue0 = TagList["TestLiniaA"].CurrentValue;
            string tagCurrentValue1 = TagList["Interlock"].CurrentValue;
            string tagCurrentValue2 = TagList["Anthon 2 Interlock Scanare"].CurrentValue;
            string tagCurrentValue3 = TagList["Anthon 3 Interlock Scanare"].CurrentValue;
            string tagCurrentValue4 = TagList["BedBox Interlock"].CurrentValue;
            string tagCurrentValue5 = TagList["Biesse Interlock Scanare"].CurrentValue;
            string tagCurrentValue6 = TagList["Interlock2"].CurrentValue;
            string tagCurrentValue7 = TagList["Homag 1 Interlock Scanare"].CurrentValue;
            string tagCurrentValue8 = TagList["Homag 2 Interlock Scanare Infeed 2"].CurrentValue;
            string tagCurrentValue9 = TagList["Homag 2 Interlock Scanare Infeed 1"].CurrentValue;
            string tagCurrentValue10 = TagList["Homag 3 Interlock Scanare Infeed 2"].CurrentValue;
            string tagCurrentValue11 = TagList["Homag 3 Interlock Scanare Infeed 1"].CurrentValue;
            string tagCurrentValue12 = TagList["Homag 4 Interlock Scanare"].CurrentValue;
            string tagCurrentValue13 = TagList["Homag 5 Interlock Scanare"].CurrentValue;
            string tagCurrentValue14 = TagList["Homag 6 Interlock Scanare Infeed 2"].CurrentValue;
            string tagCurrentValue15 = TagList["Homag 6 Interlock Scanare Infeed 1"].CurrentValue;
            string tagCurrentValue16 = TagList["Homag 7 Interlock Scanare"].CurrentValue;
            string tagCurrentValue17 = TagList["Koch 1 Interlock Scanare"].CurrentValue;
            string tagCurrentValue18 = TagList["Koch 2 Interlock Scanare"].CurrentValue;
            string tagCurrentValue19 = TagList["Output Interlock"].CurrentValue;
            string tagCurrentValue20 = TagList["L2 Interlock Scanare"].CurrentValue;
            string tagCurrentValue21 = TagList["L3 Interlock Scanare"].CurrentValue;
            string tagCurrentValue22 = TagList["L4_Interlock_scanare"].CurrentValue;
            string tagCurrentValue23 = TagList["L5 Caserat Interlock Scanare"].CurrentValue;
            string tagCurrentValue24 = TagList["Output interlock 2 dreapta"].CurrentValue;
            string tagCurrentValue25 = TagList["Output interlock 1 stanga"].CurrentValue;
            string tagCurrentValue26 = TagList["Output interlock"].CurrentValue;
            string tagCurrentValue27 = TagList["Tivox interlock scanare"].CurrentValue;
            string tagCurrentValue28 = TagList["Weeke 5 Interlock Scanare"].CurrentValue;

            results.Add(tagCurrentValue0);
            results.Add(tagCurrentValue1);
            results.Add(tagCurrentValue2);
            results.Add(tagCurrentValue3);
            results.Add(tagCurrentValue4);
            results.Add(tagCurrentValue5);
            results.Add(tagCurrentValue6);
            results.Add(tagCurrentValue7);
            results.Add(tagCurrentValue8);
            results.Add(tagCurrentValue9);
            results.Add(tagCurrentValue10);
            results.Add(tagCurrentValue11);
            results.Add(tagCurrentValue12);
            results.Add(tagCurrentValue13); 
            results.Add(tagCurrentValue14);
            results.Add(tagCurrentValue15);
            results.Add(tagCurrentValue16);
            results.Add(tagCurrentValue17);
            results.Add(tagCurrentValue18);
            results.Add(tagCurrentValue19);
            results.Add(tagCurrentValue20);
            results.Add(tagCurrentValue21);
            results.Add(tagCurrentValue22);
            results.Add(tagCurrentValue23);
            results.Add(tagCurrentValue24);
            results.Add(tagCurrentValue25);
            results.Add(tagCurrentValue26);
            results.Add(tagCurrentValue27);
            results.Add(tagCurrentValue28);
            return results;
        }
        private void WelcomePage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
            Environment.Exit(0);
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

        private void AddLogItemToDatabase(ReasonModal reasonModal, Label label, string status, Label aditionalLabel = null)
        {
            DateTime dateTime = DateTime.Now;
            string fullName = LoginForm.loggedUserFullName;
            string? aditionalInfo = aditionalLabel != null ? $"({aditionalLabel.Text.ToUpper()})" : null;

            string action = $"({label.Text.ToUpper()}){aditionalInfo} BYPASS {status}.";
            var reason = reasonModal.Reason;

            using (var ctx = new DatabaseContext())
            {
                var logItem = new LogItem()
                {
                    ActionExecutionTime = dateTime,
                    Action = action,
                    Reason = reason,
                    User = LoginForm.selectedUser,
                };

                ctx.Users.Attach(LoginForm.selectedUser);
                ctx.LogItems.Add(logItem);
                ctx.SaveChanges();
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
                    myOPCUAServer.WriteNode("ns=2;s=Ambalaj Linia A.Linia A PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelLiniaA, "ACTIV", interlock);
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
                    myOPCUAServer.WriteNode("ns=2;s=Ambalaj Linia A.Linia A PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelLiniaA, "INACTIV", interlock);
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
                    myOPCUAServer.WriteNode("ns=2;s=Anthon 2.Anthon 2 Alim PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelAnthon2, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Anthon 2.Anthon 2 Alim PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelAnthon2, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Anthon 3.Anthon 3 Alim PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelAnthon3, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Anthon 3.Anthon 3 Alim PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelAnthon3, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=BedBox.BedBoxPLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelBedBox, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=BedBox.BedBoxPLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelBedBox, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Biesse.Biesse PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelBiesse, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Biesse.Biesse PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelBiesse, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Debitare_folie.PLC_debitare_folie.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelDebitareFolie, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Debitare_folie.PLC_debitare_folie.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelDebitareFolie, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 1.Homag 1 PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag1, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 1.Homag 1 PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag1, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 2.Homag 2 Infeed PLC.Dsh bypass 2", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag2, "ACTIV", interlockScanareInfeed2);
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 2.Homag 2 Infeed PLC.Dsh bypass 2", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag2, "INACTIV", interlockScanareInfeed2);
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 2.Homag 2 Infeed PLC.Dsh bypass 1", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag2, "ACTIV", interlockScanareInfeed1);
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 2.Homag 2 Infeed PLC.Dsh bypass 1", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag2, "INACTIV", interlockScanareInfeed1);
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 3.Homag 3 Infeed PLC.Dsh bypass 2", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag3, "ACTIV", interlockScanareInfeed2h3);
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 3.Homag 3 Infeed PLC.Dsh bypass 2", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag3, "INACTIV", interlockScanareInfeed2h3);
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 3.Homag 3 Infeed PLC.Dsh bypass 1", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag3, "ACTIV", interlockScanareInfeed1h3);
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 3.Homag 3 Infeed PLC.Dsh bypass 1", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag3, "INACTIV", interlockScanareInfeed1h3);
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 4.Homag 4 Infeed PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag4, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 4.Homag 4 Infeed PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag4, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 5.Homag 5 Infeed PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag5, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 5.Homag 5 Infeed PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag5, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 6.Homag 6 Infeed PLC.Dsh bypass 2", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag6, "ACTIV", interlockScanareInfeed2h6);
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 6.Homag 6 Infeed PLC.Dsh bypass 2", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag6, "INACTIV", interlockScanareInfeed2h6);
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 6.Homag 6 Infeed PLC.Dsh bypass 1", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag6, "ACTIV", interlockScanareInfeed1h6);
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 6.Homag 6 Infeed PLC.Dsh bypass 1", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag6, "INACTIV", interlockScanareInfeed1h6);
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 7.Homag 7 Infeed PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelHomag7, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Homag 7.Homag 7 Infeed PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelHomag7, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Koch 1.Koch 1 PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelKoch1, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Koch 1.Koch 1 PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelKoch1, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Koch 2.Koch 2 PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelKoch2, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Koch 2.Koch 2 PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelKoch2, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Koch 3.Interlock Koch 3.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelKoch3, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Koch 3.Interlock Koch 3.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelKoch3, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=L2_Caserat.L2 PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelL2Caserat, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=L2_Caserat.L2 PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelL2Caserat, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=L3_Caserat.L3 PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelL3Caserat, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=L3_Caserat.L3 PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelL3Caserat, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=L4_Caserat.L4 PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelL4Caserat, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=L4_Caserat.L4 PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelL4Caserat, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=L5 Caserat.L5 Caserat PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelL5Caserat, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=L5 Caserat.L5 Caserat PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelL5Caserat, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Linia 1 caserat.Adam L Caserat 1.Dsh bypass 2", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelLinia1Caserat, "ACTIV", outputInterlock2Dreapta);
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
                    myOPCUAServer.WriteNode("ns=2;s=Linia 1 caserat.Adam L Caserat 1.Dsh bypass 2", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelLinia1Caserat, "INACTIV", outputInterlock2Dreapta);
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
                    myOPCUAServer.WriteNode("ns=2;s=Linia 1 caserat.Adam L Caserat 1.Dsh bypass 1", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelLinia1Caserat, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Linia 1 caserat.Adam L Caserat 1.Dsh bypass 1", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelLinia1Caserat, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Process 2.Interlock Process 2.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelProcess2, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Process 2.Interlock Process 2.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelProcess2, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Tivox.Tivox PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelTivox, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Tivox.Tivox PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelTivox, "INACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Weeke 5.Weeke 5 PLC.Dsh bypass", (bool)true);
                    AddLogItemToDatabase(reasonModal, labelWeeke, "ACTIV");
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
                    myOPCUAServer.WriteNode("ns=2;s=Weeke 5.Weeke 5 PLC.Dsh bypass", (bool)false);
                    AddLogItemToDatabase(reasonModal, labelWeeke, "INACTIV");
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

        private void label56_Click(object sender, EventArgs e)
        {

        }
    }
}
