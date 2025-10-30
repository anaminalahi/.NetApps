namespace PicturesForMongoDB
{
    partial class FormMain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnConnexion = new System.Windows.Forms.Button();
            this.BtnQuitter = new System.Windows.Forms.Button();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtEmpId = new System.Windows.Forms.TextBox();
            this.lblEmpId = new System.Windows.Forms.Label();
            this.PicBox = new System.Windows.Forms.PictureBox();
            this.lblPicture = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnConnexion
            // 
            this.BtnConnexion.Location = new System.Drawing.Point(11, 272);
            this.BtnConnexion.Margin = new System.Windows.Forms.Padding(2);
            this.BtnConnexion.Name = "BtnConnexion";
            this.BtnConnexion.Size = new System.Drawing.Size(111, 31);
            this.BtnConnexion.TabIndex = 0;
            this.BtnConnexion.Text = "Load Data";
            this.BtnConnexion.UseVisualStyleBackColor = true;
            this.BtnConnexion.Click += new System.EventHandler(this.BtnConnexion_Click);
            // 
            // BtnQuitter
            // 
            this.BtnQuitter.Location = new System.Drawing.Point(465, 272);
            this.BtnQuitter.Margin = new System.Windows.Forms.Padding(2);
            this.BtnQuitter.Name = "BtnQuitter";
            this.BtnQuitter.Size = new System.Drawing.Size(82, 31);
            this.BtnQuitter.TabIndex = 1;
            this.BtnQuitter.Text = "Exit";
            this.BtnQuitter.UseVisualStyleBackColor = true;
            this.BtnQuitter.Click += new System.EventHandler(this.BtnQuitter_Click);
            // 
            // txtFullName
            // 
            this.txtFullName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFullName.Location = new System.Drawing.Point(231, 145);
            this.txtFullName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(316, 20);
            this.txtFullName.TabIndex = 2;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(134, 147);
            this.lblFullName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(51, 13);
            this.lblFullName.TabIndex = 3;
            this.lblFullName.Text = "FullName";
            // 
            // txtEmpId
            // 
            this.txtEmpId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmpId.Location = new System.Drawing.Point(231, 169);
            this.txtEmpId.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmpId.Name = "txtEmpId";
            this.txtEmpId.Size = new System.Drawing.Size(133, 20);
            this.txtEmpId.TabIndex = 4;
            // 
            // lblEmpId
            // 
            this.lblEmpId.AutoSize = true;
            this.lblEmpId.Location = new System.Drawing.Point(134, 171);
            this.lblEmpId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEmpId.Name = "lblEmpId";
            this.lblEmpId.Size = new System.Drawing.Size(93, 13);
            this.lblEmpId.TabIndex = 5;
            this.lblEmpId.Text = "Déactivation Date";
            // 
            // PicBox
            // 
            this.PicBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBox.Location = new System.Drawing.Point(231, 26);
            this.PicBox.Margin = new System.Windows.Forms.Padding(2);
            this.PicBox.Name = "PicBox";
            this.PicBox.Size = new System.Drawing.Size(105, 115);
            this.PicBox.TabIndex = 6;
            this.PicBox.TabStop = false;
            // 
            // lblPicture
            // 
            this.lblPicture.AutoSize = true;
            this.lblPicture.Location = new System.Drawing.Point(134, 26);
            this.lblPicture.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPicture.Name = "lblPicture";
            this.lblPicture.Size = new System.Drawing.Size(40, 13);
            this.lblPicture.TabIndex = 7;
            this.lblPicture.Text = "Picture";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(231, 272);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(84, 31);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 314);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblPicture);
            this.Controls.Add(this.PicBox);
            this.Controls.Add(this.lblEmpId);
            this.Controls.Add(this.txtEmpId);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.BtnQuitter);
            this.Controls.Add(this.BtnConnexion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lenel Employees";
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnConnexion;
        private System.Windows.Forms.Button BtnQuitter;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtEmpId;
        private System.Windows.Forms.Label lblEmpId;
        private System.Windows.Forms.PictureBox PicBox;
        private System.Windows.Forms.Label lblPicture;
        private System.Windows.Forms.Button btnAdd;
    }
}

