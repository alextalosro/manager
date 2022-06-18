using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace EcolorProductionManager
{
    public partial class UserRegistrationForm : Form
    {
        public UserRegistrationForm()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (!AreControlsValid(this.Controls))
            {
                return;
            }
            else
            {
                //Query the db for username and password;
                string mainconn = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);
                string sqlquery = "INSERT INTO [dbo].[users] VALUES (@username, @password, @firstname, @lastname)";
                sqlconn.Open();
                SqlCommand sqlCmd = new SqlCommand(sqlquery, sqlconn);
                sqlCmd.Parameters.AddWithValue("@username", textUsername.Text);
                sqlCmd.Parameters.AddWithValue("@password", textPassword.Text);
                sqlCmd.Parameters.AddWithValue("@firstname", textFirstName.Text);
                sqlCmd.Parameters.AddWithValue("@lastname", textLastName.Text);
                sqlCmd.ExecuteNonQuery();
                labelStatusMessage.Text = "User " + textUsername.Text + " was registered successfully!";
                sqlconn.Close();
            }
        }

        private void linkLabelBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabelLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
            this.Dispose();
        }

        private bool AreControlsValid(Control.ControlCollection controlCollection)
        {
            foreach (Control control in controlCollection)
            {
                if (control is TextBox)
                {
                    if (String.IsNullOrEmpty(((TextBox)control).Text))
                    {
                        errorProvider1.SetError(control, "Field must not be empty!");
                        return false;
                    }
                    if(control.Name == "textRepeatedPassword")
                    {
                        if (control.Text != textPassword.Text)
                        {
                            errorProvider1.SetError(control, String.Empty);
                            errorProvider1.SetError(control, "Password must match!");
                            return false;
                        }
                    }
                }
                errorProvider1.SetError(control, String.Empty);
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
