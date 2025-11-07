namespace ScannerVB6
{
    partial class FormGFABecubedFarPoint
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
            BtnRecuperer = new Button();
            BtnQuitter = new Button();
            BtnExporter = new Button();
            CMF = new OpenFileDialog();
            SuspendLayout();
            // 
            // BtnRecuperer
            // 
            BtnRecuperer.Location = new Point(35, 31);
            BtnRecuperer.Margin = new Padding(4, 3, 4, 3);
            BtnRecuperer.Name = "BtnRecuperer";
            BtnRecuperer.Size = new Size(161, 46);
            BtnRecuperer.TabIndex = 27;
            BtnRecuperer.Text = "Récuperer";
            BtnRecuperer.UseVisualStyleBackColor = true;
            BtnRecuperer.Click += BtnRecuperer_Click;
            // 
            // BtnQuitter
            // 
            BtnQuitter.Location = new Point(445, 31);
            BtnQuitter.Margin = new Padding(4, 3, 4, 3);
            BtnQuitter.Name = "BtnQuitter";
            BtnQuitter.Size = new Size(161, 46);
            BtnQuitter.TabIndex = 28;
            BtnQuitter.Text = "Quitter";
            BtnQuitter.UseVisualStyleBackColor = true;
            BtnQuitter.Click += BtnQuitter_Click;
            // 
            // BtnExporter
            // 
            BtnExporter.Location = new Point(239, 31);
            BtnExporter.Margin = new Padding(4, 3, 4, 3);
            BtnExporter.Name = "BtnExporter";
            BtnExporter.Size = new Size(161, 46);
            BtnExporter.TabIndex = 29;
            BtnExporter.Text = "Exporter CSV";
            BtnExporter.UseVisualStyleBackColor = true;
            // 
            // FormGFABecubedFarPoint
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(637, 176);
            Controls.Add(BtnExporter);
            Controls.Add(BtnQuitter);
            Controls.Add(BtnRecuperer);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FormGFABecubedFarPoint";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormGFABecubedFarPoint";
            ResumeLayout(false);
        }

        #endregion

        internal Button BtnRecuperer;
        internal Button BtnQuitter;
        internal Button BtnExporter;
        private OpenFileDialog CMF;
    }
}