namespace BLOBSqlToJpeg
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
            this.BtnSelectFolder = new System.Windows.Forms.Button();
            this.LblFolderImages = new System.Windows.Forms.Label();
            this.TxtImagesFolder = new System.Windows.Forms.TextBox();
            this.PicBox = new System.Windows.Forms.PictureBox();
            this.BtnSaveCurrent = new System.Windows.Forms.Button();
            this.BtnSaveAll = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.CMDFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.DBGridPersonel = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBGridPersonel)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnSelectFolder
            // 
            this.BtnSelectFolder.Location = new System.Drawing.Point(1191, 48);
            this.BtnSelectFolder.Name = "BtnSelectFolder";
            this.BtnSelectFolder.Size = new System.Drawing.Size(34, 23);
            this.BtnSelectFolder.TabIndex = 0;
            this.BtnSelectFolder.Text = "...";
            this.BtnSelectFolder.UseVisualStyleBackColor = true;
            // 
            // LblFolderImages
            // 
            this.LblFolderImages.AutoSize = true;
            this.LblFolderImages.Location = new System.Drawing.Point(828, 30);
            this.LblFolderImages.Name = "LblFolderImages";
            this.LblFolderImages.Size = new System.Drawing.Size(106, 13);
            this.LblFolderImages.TabIndex = 1;
            this.LblFolderImages.Text = "Images Export Folder";
            // 
            // TxtImagesFolder
            // 
            this.TxtImagesFolder.Location = new System.Drawing.Point(831, 50);
            this.TxtImagesFolder.Name = "TxtImagesFolder";
            this.TxtImagesFolder.Size = new System.Drawing.Size(354, 20);
            this.TxtImagesFolder.TabIndex = 2;
            // 
            // PicBox
            // 
            this.PicBox.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.PicBox.Location = new System.Drawing.Point(831, 99);
            this.PicBox.Name = "PicBox";
            this.PicBox.Size = new System.Drawing.Size(393, 393);
            this.PicBox.TabIndex = 3;
            this.PicBox.TabStop = false;
            // 
            // BtnSaveCurrent
            // 
            this.BtnSaveCurrent.Location = new System.Drawing.Point(831, 527);
            this.BtnSaveCurrent.Name = "BtnSaveCurrent";
            this.BtnSaveCurrent.Size = new System.Drawing.Size(122, 32);
            this.BtnSaveCurrent.TabIndex = 5;
            this.BtnSaveCurrent.Text = "Save Current Picture";
            this.BtnSaveCurrent.UseVisualStyleBackColor = true;
            this.BtnSaveCurrent.Click += new System.EventHandler(this.BtnSaveCurrent_Click);
            // 
            // BtnSaveAll
            // 
            this.BtnSaveAll.Location = new System.Drawing.Point(959, 527);
            this.BtnSaveAll.Name = "BtnSaveAll";
            this.BtnSaveAll.Size = new System.Drawing.Size(101, 32);
            this.BtnSaveAll.TabIndex = 6;
            this.BtnSaveAll.Text = "Save All Pictures";
            this.BtnSaveAll.UseVisualStyleBackColor = true;
            this.BtnSaveAll.Click += new System.EventHandler(this.BtnSaveAll_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(1164, 527);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(60, 32);
            this.BtnExit.TabIndex = 7;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // DBGridPersonel
            // 
            this.DBGridPersonel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DBGridPersonel.Location = new System.Drawing.Point(25, 30);
            this.DBGridPersonel.Name = "DBGridPersonel";
            this.DBGridPersonel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DBGridPersonel.Size = new System.Drawing.Size(783, 529);
            this.DBGridPersonel.TabIndex = 8;
            this.DBGridPersonel.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DBGridPersonel_CellClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 577);
            this.Controls.Add(this.DBGridPersonel);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnSaveAll);
            this.Controls.Add(this.BtnSaveCurrent);
            this.Controls.Add(this.PicBox);
            this.Controls.Add(this.TxtImagesFolder);
            this.Controls.Add(this.LblFolderImages);
            this.Controls.Add(this.BtnSelectFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blob Manager Tool";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBGridPersonel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSelectFolder;
        private System.Windows.Forms.Label LblFolderImages;
        private System.Windows.Forms.TextBox TxtImagesFolder;
        private System.Windows.Forms.PictureBox PicBox;
        private System.Windows.Forms.Button BtnSaveCurrent;
        private System.Windows.Forms.Button BtnSaveAll;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.FolderBrowserDialog CMDFolder;
        private System.Windows.Forms.DataGridView DBGridPersonel;
        
    }
}

