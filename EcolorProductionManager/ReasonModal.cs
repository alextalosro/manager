using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcolorProductionManager
{
    public partial class ReasonModal : Form
    {
        TaskCompletionSource<DialogResult> _tcs;
        public ReasonModal()
        {
            InitializeComponent();
        }

        public Task<DialogResult> ShowModalAsync()
        {
            _tcs = new TaskCompletionSource<DialogResult>();

            this.Visible = true;
            this.BringToFront();
            return _tcs.Task;
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            if (AreControlsValid(this.Controls))
            {
                _tcs.SetResult(DialogResult.OK);
                this.Hide();
            }
            else return;
        }

        private void buttonAbort_Click(object sender, EventArgs e)
        {
            _tcs.SetResult(DialogResult.Cancel);
            this.Hide();
        }

        public string Reason
        {
            get { return textBoxReason.Text; }
            set { textBoxReason.Text = value; }
        }

        private bool AreControlsValid(Control.ControlCollection controlCollection)
        {
            foreach (Control control in controlCollection)
            {
                if (control is TextBox)
                {
                    if (String.IsNullOrEmpty(((TextBox)control).Text))
                    {
                        errorProvider1.SetError(control, "Campul nu trebuie sa fie gol");
                        labelErrorPosition.Text = "Campul nu trebuie sa fie gol";
                        return false;
                    }
                    if (((TextBox)control).Text.Length < 10)
                    {
                        errorProvider1.SetError(control, "Introduceti minim 10 caractere!");
                        labelErrorPosition.Text = "Introduceti minim 10 caractere!";
                        return false;
                    }
                }
                labelErrorPosition.Text = "";
                errorProvider1.SetError(control, String.Empty);
            }
            return true;
        }
    }
}
