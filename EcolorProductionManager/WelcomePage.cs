using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EcolorProductionManager.OPCUAClass;

namespace EcolorProductionManager
{
    public partial class WelcomePage : Form
    {
        //creating a object that encapsulates the netire OPC UA Server related work
        OPCUAClass myOPCUAServer;

        //creating a dictionary of Tags that would be captured from the OPC UA Server
        Dictionary<String, OPCUAClass.TagClass> TagList = new Dictionary<String, OPCUAClass.TagClass>();

        public WelcomePage()
        {
            InitializeComponent();

            TagList.Add("TestLiniaA", new OPCUAClass.TagClass("TestLiniaA", "Ambalaj Linia A.Linia A PLC.TestLiniaA"));

            myOPCUAServer = new OPCUAClass("127.0.0.1", "49320", TagList, true, 1, "2");

            myOPCUAServer.WriteNode("Ambalaj Linia A.Linia A PLC.TestLiniaA", (bool)false);

            var tagCurrentValue = TagList["TestLiniaA"].CurrentValue;
            var tagLastGoodValue = TagList["TestLiniaA"].LastGoodValue;
            var lastTimeTagupdated = TagList["TestLiniaA"].LastUpdatedTime;
        }

        private void WelcomePage_Load(object sender, EventArgs e)
        {
            loggedUsername.Text = LoginForm.wellcomeUser;
            this.Text = "Welcome " + LoginForm.wellcomeUser;
        }

        private void logOutLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }
    }
}
