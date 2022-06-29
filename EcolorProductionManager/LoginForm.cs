using System;
using System.Data;
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

        public static string loggedUserFullName = "";
        public static bool isCurrentUserAdmin;
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
                    DataRow[] dr = dt.Select();

                    //Set is curent logged user ? admin : user
                    isCurrentUserAdmin = dr[0].ItemArray[5].ToString() == "admin" ? true : false;
                    //Set FullName of logged user into label
                    loggedUserFullName = dr[0].ItemArray[3].ToString() + " " + dr[0].ItemArray[4].ToString();

                    WelcomePage wcp = new WelcomePage();

                    if (WelcomePage.isClientConnected)
                    {
                        wcp.Show(); //Show welcome page.
                        this.Hide(); //Hide current form (login form);
                    }
                    else
                    {
                        wcp.Dispose();
                    }
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

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.Text = "DASHBOARD BYPASS INTERLOCK";
        }
    }
}
