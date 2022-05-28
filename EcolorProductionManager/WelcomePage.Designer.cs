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
            this.buttonLock = new System.Windows.Forms.Button();
            this.buttonUnlock = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2Unlock = new System.Windows.Forms.Button();
            this.button2Lock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loggedUsername
            // 
            this.loggedUsername.AutoSize = true;
            this.loggedUsername.Location = new System.Drawing.Point(10, 8);
            this.loggedUsername.Name = "loggedUsername";
            this.loggedUsername.Size = new System.Drawing.Size(0, 13);
            this.loggedUsername.TabIndex = 0;
            // 
            // logOutLabel
            // 
            this.logOutLabel.AutoSize = true;
            this.logOutLabel.Location = new System.Drawing.Point(624, 8);
            this.logOutLabel.Name = "logOutLabel";
            this.logOutLabel.Size = new System.Drawing.Size(42, 13);
            this.logOutLabel.TabIndex = 1;
            this.logOutLabel.TabStop = true;
            this.logOutLabel.Text = "LogOut";
            this.logOutLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.logOutLabel_LinkClicked);
            // 
            // labelRegister
            // 
            this.labelRegister.AutoSize = true;
            this.labelRegister.Location = new System.Drawing.Point(579, 8);
            this.labelRegister.Name = "labelRegister";
            this.labelRegister.Size = new System.Drawing.Size(41, 13);
            this.labelRegister.TabIndex = 2;
            this.labelRegister.TabStop = true;
            this.labelRegister.Text = "register";
            // 
            // labelConnectionLog
            // 
            this.labelConnectionLog.AutoSize = true;
            this.labelConnectionLog.Location = new System.Drawing.Point(554, 8);
            this.labelConnectionLog.Name = "labelConnectionLog";
            this.labelConnectionLog.Size = new System.Drawing.Size(21, 13);
            this.labelConnectionLog.TabIndex = 3;
            this.labelConnectionLog.TabStop = true;
            this.labelConnectionLog.Text = "log";
            // 
            // buttonLock
            // 
            this.buttonLock.Location = new System.Drawing.Point(186, 47);
            this.buttonLock.Name = "buttonLock";
            this.buttonLock.Size = new System.Drawing.Size(75, 23);
            this.buttonLock.TabIndex = 4;
            this.buttonLock.Text = "Lock";
            this.buttonLock.UseVisualStyleBackColor = true;
            this.buttonLock.Click += new System.EventHandler(this.buttonLock_Click);
            // 
            // buttonUnlock
            // 
            this.buttonUnlock.Location = new System.Drawing.Point(267, 47);
            this.buttonUnlock.Name = "buttonUnlock";
            this.buttonUnlock.Size = new System.Drawing.Size(75, 23);
            this.buttonUnlock.TabIndex = 5;
            this.buttonUnlock.Text = "Unlock";
            this.buttonUnlock.UseVisualStyleBackColor = true;
            this.buttonUnlock.Click += new System.EventHandler(this.buttonUnlock_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "TestLiniaA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Anthon 2 Interlock Scanare";
            // 
            // button2Unlock
            // 
            this.button2Unlock.Location = new System.Drawing.Point(267, 83);
            this.button2Unlock.Name = "button2Unlock";
            this.button2Unlock.Size = new System.Drawing.Size(75, 23);
            this.button2Unlock.TabIndex = 8;
            this.button2Unlock.Text = "Lock";
            this.button2Unlock.UseVisualStyleBackColor = true;
            this.button2Unlock.Click += new System.EventHandler(this.button2Unlock_Click);
            // 
            // button2Lock
            // 
            this.button2Lock.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button2Lock.Location = new System.Drawing.Point(186, 83);
            this.button2Lock.Name = "button2Lock";
            this.button2Lock.Size = new System.Drawing.Size(75, 23);
            this.button2Lock.TabIndex = 9;
            this.button2Lock.Text = "Lock";
            this.button2Lock.UseVisualStyleBackColor = false;
            this.button2Lock.Click += new System.EventHandler(this.button2Lock_Click);
            // 
            // WelcomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 390);
            this.Controls.Add(this.button2Lock);
            this.Controls.Add(this.button2Unlock);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonUnlock);
            this.Controls.Add(this.buttonLock);
            this.Controls.Add(this.labelConnectionLog);
            this.Controls.Add(this.labelRegister);
            this.Controls.Add(this.logOutLabel);
            this.Controls.Add(this.loggedUsername);
            this.Name = "WelcomePage";
            this.Text = "WelcomePage";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WelcomePage_FormClosed);
            this.Load += new System.EventHandler(this.WelcomePage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label loggedUsername;
        private System.Windows.Forms.LinkLabel logOutLabel;
        private System.Windows.Forms.LinkLabel labelRegister;
        private System.Windows.Forms.LinkLabel labelConnectionLog;
        private System.Windows.Forms.Button buttonLock;
        private System.Windows.Forms.Button buttonUnlock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2Unlock;
        private System.Windows.Forms.Button button2Lock;
    }
}