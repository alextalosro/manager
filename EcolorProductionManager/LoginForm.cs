using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace EcolorProductionManager
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public static string wellcomeUser = "";
        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Query the db for username and password;
                string mainconn = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);
                string sqlquery = "SELECT * FROM [dbo].[users] WHERE username=@ParsedUserName and password=@ParsedPassword";
                sqlconn.Open();
                SqlCommand sqlCmd = new SqlCommand(sqlquery, sqlconn);
                sqlCmd.Parameters.AddWithValue("@ParsedUserName", textUsername.Text);
                sqlCmd.Parameters.AddWithValue("@ParsedPassword", textPassword.Text);
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sqlCmd.ExecuteNonQuery();

                if (dt.Rows.Count > 0)
                {
                    //set username into label
                    wellcomeUser = textUsername.Text;

                    WelcomePage wcp = new WelcomePage();
                    wcp.Show(); //Show welcome page.
                    this.Hide(); //Hide current form (login form);
                }
                else
                {
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
    }
}
