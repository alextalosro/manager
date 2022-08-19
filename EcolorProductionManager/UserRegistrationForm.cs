using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using DataAccessLayer;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace EcolorProductionManager
{
    public partial class UserRegistrationForm : Form
    {
        public UserRegistrationForm()
        {
            InitializeComponent();
            //Combo box is non-editable. Only value presented are selectable.
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            //By default new user will be set to user
            comboBox1.SelectedIndex = 0;
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (!AreControlsValid(this.Controls))
                {
                    return;
                }
                else
                {

                    User selectedUser = new User();

                    using (var ctx = new DatabaseContext())
                    {
                        selectedUser = ctx.Users
                            .Where(user => user.Username == textUsername.Text)
                            .FirstOrDefault();
                    }

                    if (selectedUser != null)
                    {
                        if (selectedUser.Username == textUsername.Text)
                        {
                            MessageBox.Show("Username-ul este deja utilizat. Incercati alt username");
                            return;
                        }
                    }

                    using (var ctx = new DatabaseContext())
                    {
                        var user = new User()
                        {
                            Username = textUsername.Text,
                            Firstname = textFirstName.Text,
                            Lastname = textLastName.Text,
                            Password = hashPassword(textPassword.Text),
                            UserRole = comboBox1.GetItemText(comboBox1.SelectedItem),
                        };

                        ctx.Users.Add(user);
                        ctx.SaveChanges();
                        AddLogItemToDatabase(user, true);
                    }

                    
                    MessageBox.Show("Utilizator inregistrat cu succes. Puteti inchide panou de inregistrare !");

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        public string hashPassword(string plainPassword)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(plainPassword);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }

        private void AddLogItemToDatabase(User user, bool registerSucces = false)
        {
            DateTime dateTime = DateTime.Now;
            var reason = "";
            string action = "";

            if (registerSucces == true)
            {
                action = $"{user.Username} a fost inregistrat cu success";
            }


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
    }
}
