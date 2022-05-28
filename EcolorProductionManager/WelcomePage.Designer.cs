namespace EcolorProductionManager
{
    partial class WelcomePage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loggedUsername = new System.Windows.Forms.Label();
            this.logOutLabel = new System.Windows.Forms.LinkLabel();
            this.labelRegister = new System.Windows.Forms.LinkLabel();
            this.labelConnectionLog = new System.Windows.Forms.LinkLabel();
            this.textConsole = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // loggedUsername
            // 
            this.loggedUsername.AutoSize = true;
            this.loggedUsername.Location = new System.Drawing.Point(12, 9);
            this.loggedUsername.Name = "loggedUsername";
            this.loggedUsername.Size = new System.Drawing.Size(0, 15);
            this.loggedUsername.TabIndex = 0;
            // 
            // logOutLabel
            // 
            this.logOutLabel.AutoSize = true;
            this.logOutLabel.Location = new System.Drawing.Point(728, 9);
            this.logOutLabel.Name = "logOutLabel";
            this.logOutLabel.Size = new System.Drawing.Size(47, 15);
            this.logOutLabel.TabIndex = 1;
            this.logOutLabel.TabStop = true;
            this.logOutLabel.Text = "LogOut";
            this.logOutLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.logOutLabel_LinkClicked);
            // 
            // labelRegister
            // 
            this.labelRegister.AutoSize = true;
            this.labelRegister.Location = new System.Drawing.Point(676, 9);
            this.labelRegister.Name = "labelRegister";
            this.labelRegister.Size = new System.Drawing.Size(46, 15);
            this.labelRegister.TabIndex = 2;
            this.labelRegister.TabStop = true;
            this.labelRegister.Text = "register";
            // 
            // labelConnectionLog
            // 
            this.labelConnectionLog.AutoSize = true;
            this.labelConnectionLog.Location = new System.Drawing.Point(646, 9);
            this.labelConnectionLog.Name = "labelConnectionLog";
            this.labelConnectionLog.Size = new System.Drawing.Size(24, 15);
            this.labelConnectionLog.TabIndex = 3;
            this.labelConnectionLog.TabStop = true;
            this.labelConnectionLog.Text = "log";
            // 
            // textConsole
            // 
            this.textConsole.Location = new System.Drawing.Point(42, 29);
            this.textConsole.Name = "textConsole";
            this.textConsole.Size = new System.Drawing.Size(321, 206);
            this.textConsole.TabIndex = 4;
            this.textConsole.Text = "";
            // 
            // WelcomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textConsole);
            this.Controls.Add(this.labelConnectionLog);
            this.Controls.Add(this.labelRegister);
            this.Controls.Add(this.logOutLabel);
            this.Controls.Add(this.loggedUsername);
            this.Name = "WelcomePage";
            this.Text = "WelcomePage";
            this.Load += new System.EventHandler(this.WelcomePage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label loggedUsername;
        private System.Windows.Forms.LinkLabel logOutLabel;
        private System.Windows.Forms.LinkLabel labelRegister;
        private System.Windows.Forms.LinkLabel labelConnectionLog;
        private System.Windows.Forms.RichTextBox textConsole;
    }
}