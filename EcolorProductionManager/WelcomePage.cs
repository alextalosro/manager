﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

            //TagList.Add("TestLiniaA", new OPCUAClass.TagClass("TestLiniaA", "Ambalaj Linia A.Linia A PLC.TestLiniaA"));
            //TagList.Add("Anthon 2 Interlock Scanare", new OPCUAClass.TagClass("Anthon 2", "Anthon 2.Anthon 2 Alim PLC.Anthon 2 Interlock Scanare"));

            //myOPCUAServer = new OPCUAClass("127.0.0.1", "49320", TagList, true, 1, "2");

            ////var tagCurrentValue = TagList["TestLiniaA"].CurrentValue;
            ////var tagLastGoodValue = TagList["TestLiniaA"].LastGoodValue;
            ////var lastTimeTagupdated = TagList["TestLiniaA"].LastUpdatedTime;

            ////var tagCurrentValue = TagList["Anthon 2"].CurrentValue;
            ////var tagLastGoodValue = TagList["Anthon 2"].LastGoodValue;
            ////var lastTimeTagupdated = TagList["Anthon 2"].LastUpdatedTime;
        }

        private void WelcomePage_Load(object sender, EventArgs e)
        {
            loggedUsername.Text = LoginForm.wellcomeUser;
            //this.Text = "Welcome " + LoginForm.wellcomeUser;
            this.Text = "DASHBOARD BYPASS INTERLOCK";
        }

        private void logOutLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private async void buttonLock_Click(object sender, EventArgs e)
        {
            try
            {
                ReasonModal reasonModal = new ReasonModal();
                reasonModal.Show();
                this.Enabled = false;

                if (await reasonModal.ShowModalAsync() == DialogResult.OK)
                {
                        DateTime dateTime = DateTime.Now;
                        string fullName = LoginForm.wellcomeUser;
                        string action = $"Statusul liniei ({label4.Text.ToUpper()}) a fost schimbat in {this.buttonLock.Text.ToUpper()}.";
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

                    this.Enabled = true;
                    //myOPCUAServer.WriteNode("ns=2;s=Ambalaj Linia A.Linia A PLC.TestLiniaA", (bool)true);
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

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            try
            {
                myOPCUAServer.WriteNode("ns=2;s=Ambalaj Linia A.Linia A PLC.TestLiniaA", (bool)false);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void button2Lock_Click(object sender, EventArgs e)
        {
            try
            {
                myOPCUAServer.WriteNode("ns=2;s=Anthon 2.Anthon 2 Alim PLC.Anthon 2 Interlock Scanare", (bool)true);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void button2Unlock_Click(object sender, EventArgs e)
        {
            try
            {
                myOPCUAServer.WriteNode("ns=2;s=Anthon 2.Anthon 2 Alim PLC.Anthon 2 Interlock Scanare", (bool)false);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
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
    }
}
