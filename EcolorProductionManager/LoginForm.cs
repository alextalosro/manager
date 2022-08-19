﻿using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using DataAccessLayer;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EcolorProductionManager
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public static User selectedUser = new User();
        public static string loggedUserFullName = "";
        public static bool isCurrentUserAdmin;
        private void loginButton_Click(object sender, EventArgs e)
        {
            string hashedPassword = hashPassword(textPassword.Text);

            try
            {

                using (var ctx = new DatabaseContext())
                {
                    selectedUser = ctx.Users
                        .Where(user => user.Username == textUsername.Text && user.Password == hashedPassword)
                        .FirstOrDefault();
                }

                if (selectedUser != null)
                {
                    //Set is curent logged user ? admin : user
                    isCurrentUserAdmin = selectedUser.UserRole == "admin" ? true : false;
                    //Set FullName of logged user into label
                    loggedUserFullName = selectedUser.Firstname + " " + selectedUser.Lastname;

                    WelcomePage wcp = new WelcomePage();

                    if (WelcomePage.isClientConnected)
                    {
                        wcp.Show(); //Show welcome page.
                        this.Hide(); //Hide current form (login form);
                        AddLogItemToDatabase(loggedUserFullName, true);
                    }
                    else
                    {
                        wcp.Dispose();
                    }

                }
                else
                {
                    AddLogItemToDatabase(textUsername.Text, false);
                    MessageBox.Show("Invalid credentials! Contact network administrator!");
                    textUsername.Text = "";
                    textPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void textUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginButton.PerformClick();
            }
        }

        private void textPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginButton.PerformClick();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.Text = "DASHBOARD BYPASS INTERLOCK";
        }

        public string hashPassword(string plainPassword)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(plainPassword);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }

        private void AddLogItemToDatabase(string username, bool loginSuccess = false)
        {
            DateTime dateTime = DateTime.Now;
            var reason = "";
            string fullName = username;
            string action = "";

            if (loginSuccess == true)
            {
                action = $"{fullName} s-a autentificat in aplicatie.";
            }
            else
            {
                action = $"{fullName} a esuat autentificarea.";
            }
            
            try
            {
                using (var ctx = new DatabaseContext())
                {
                    var logItem = new LogItem()
                    {
                        ActionExecutionTime = dateTime,
                        Action = action,
                        Reason = reason,
                        User = loginSuccess ? selectedUser : null,
                    };

                    if (loginSuccess)
                    {
                        ctx.Users.Attach(LoginForm.selectedUser);
                    }
                    ctx.LogItems.Add(logItem);
                    ctx.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
