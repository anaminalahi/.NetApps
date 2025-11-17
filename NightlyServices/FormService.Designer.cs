namespace NigthlyServices
{
    partial class FormService
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
            this.NightlyTimer = new System.Windows.Forms.Timer(this.components);
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnManualUpload = new System.Windows.Forms.Button();
            this.Libelle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NightlyTimer
            // 
            this.NightlyTimer.Enabled = true;
            this.NightlyTimer.Interval = 60000;
            this.NightlyTimer.Tick += new System.EventHandler(this.NightlyTimer_Tick);
            // 
            // BtnExit
            // 
            this.BtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExit.Location = new System.Drawing.Point(309, 43);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(53, 23);
            this.BtnExit.TabIndex = 8;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnManualUpload
            // 
            this.BtnManualUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnManualUpload.Location = new System.Drawing.Point(12, 43);
            this.BtnManualUpload.Name = "BtnManualUpload";
            this.BtnManualUpload.Size = new System.Drawing.Size(123, 23);
            this.BtnManualUpload.TabIndex = 9;
            this.BtnManualUpload.Text = "Uploader Service";
            this.BtnManualUpload.UseVisualStyleBackColor = true;
            this.BtnManualUpload.Click += new System.EventHandler(this.BtnManualUpload_Click);
            // 
            // Libelle
            // 
            this.Libelle.Location = new System.Drawing.Point(12, 11);
            this.Libelle.Name = "Libelle";
            this.Libelle.Size = new System.Drawing.Size(349, 23);
            this.Libelle.TabIndex = 10;
            this.Libelle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 78);
            this.Controls.Add(this.Libelle);
            this.Controls.Add(this.BtnManualUpload);
            this.Controls.Add(this.BtnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NightlyService Interface";
            this.Load += new System.EventHandler(this.FormService_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer NightlyTimer;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnManualUpload;
        private System.Windows.Forms.Label Libelle;
    }
}