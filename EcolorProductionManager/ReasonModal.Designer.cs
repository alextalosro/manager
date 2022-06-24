namespace EcolorProductionManager
{
    partial class ReasonModal
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
            this.components = new System.ComponentModel.Container();
            this.buttonValidate = new System.Windows.Forms.Button();
            this.buttonAbort = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxReason = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.labelErrorPosition = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonValidate
            // 
            this.buttonValidate.Location = new System.Drawing.Point(12, 95);
            this.buttonValidate.Name = "buttonValidate";
            this.buttonValidate.Size = new System.Drawing.Size(75, 23);
            this.buttonValidate.TabIndex = 0;
            this.buttonValidate.Text = "Valideaza";
            this.buttonValidate.UseVisualStyleBackColor = true;
            this.buttonValidate.Click += new System.EventHandler(this.buttonValidate_Click);
            // 
            // buttonAbort
            // 
            this.buttonAbort.Location = new System.Drawing.Point(93, 95);
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.Size = new System.Drawing.Size(75, 23);
            this.buttonAbort.TabIndex = 1;
            this.buttonAbort.Text = "Renunta";
            this.buttonAbort.UseVisualStyleBackColor = true;
            this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Introduceti motivul pentru aceasta modificare:";
            // 
            // textBoxReason
            // 
            this.textBoxReason.Location = new System.Drawing.Point(12, 50);
            this.textBoxReason.MaxLength = 255;
            this.textBoxReason.Name = "textBoxReason";
            this.textBoxReason.Size = new System.Drawing.Size(333, 20);
            this.textBoxReason.TabIndex = 3;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // labelErrorPosition
            // 
            this.labelErrorPosition.AutoSize = true;
            this.labelErrorPosition.BackColor = System.Drawing.SystemColors.Control;
            this.labelErrorPosition.ForeColor = System.Drawing.Color.Red;
            this.labelErrorPosition.Location = new System.Drawing.Point(12, 73);
            this.labelErrorPosition.Name = "labelErrorPosition";
            this.labelErrorPosition.Size = new System.Drawing.Size(0, 13);
            this.labelErrorPosition.TabIndex = 4;
            // 
            // ReasonModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.labelErrorPosition);
            this.Controls.Add(this.textBoxReason);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAbort);
            this.Controls.Add(this.buttonValidate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReasonModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReasonModal";
            this.Load += new System.EventHandler(this.ReasonModal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonValidate;
        private System.Windows.Forms.Button buttonAbort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxReason;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label labelErrorPosition;
    }
}