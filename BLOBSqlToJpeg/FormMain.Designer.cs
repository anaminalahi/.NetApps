namespace BLOBSqlToJpeg
{
    partial class FormMain
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
            this.BtnSelectFolder = new System.Windows.Forms.Button();
            this.LblFolderImages = new System.Windows.Forms.Label();
            this.TxtImagesFolder = new System.Windows.Forms.TextBox();
            this.PicBox = new System.Windows.Forms.PictureBox();
            this.BtnSaveCurrent = new System.Windows.Forms.Button();
            this.BtnSaveAll = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.CMDFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.DBGridPersonel = new System.Windows.Forms.DataGridView();
            this.NightlyTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBGridPersonel)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnSelectFolder
            // 
            this.BtnSelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSelectFolder.Location = new System.Drawing.Point(632, 8);
            this.BtnSelectFolder.Name = "BtnSelectFolder";
            this.BtnSelectFolder.Size = new System.Drawing.Size(23, 15);
            this.BtnSelectFolder.TabIndex = 0;
            this.BtnSelectFolder.Text = "...";
            this.BtnSelectFolder.UseVisualStyleBackColor = true;
            this.BtnSelectFolder.Click += new System.EventHandler(this.BtnSelectFolder_Click);
            // 
            // LblFolderImages
            // 
            this.LblFolderImages.AutoSize = true;
            this.LblFolderImages.Location = new System.Drawing.Point(353, 10);
            this.LblFolderImages.Name = "LblFolderImages";
            this.LblFolderImages.Size = new System.Drawing.Size(106, 13);
            this.LblFolderImages.TabIndex = 1;
            this.LblFolderImages.Text = "Images Export Folder";
            // 
            // TxtImagesFolder
            // 
            this.TxtImagesFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtImagesFolder.Location = new System.Drawing.Point(353, 25);
            this.TxtImagesFolder.Name = "TxtImagesFolder";
            this.TxtImagesFolder.Size = new System.Drawing.Size(302, 20);
            this.TxtImagesFolder.TabIndex = 2;
            // 
            // PicBox
            // 
            this.PicBox.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.PicBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PicBox.Location = new System.Drawing.Point(353, 52);
            this.PicBox.Name = "PicBox";
            this.PicBox.Size = new System.Drawing.Size(302, 352);
            this.PicBox.TabIndex = 3;
            this.PicBox.TabStop = false;
            // 
            // BtnSaveCurrent
            // 
            this.BtnSaveCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnSaveCurrent.Location = new System.Drawing.Point(353, 410);
            this.BtnSaveCurrent.Name = "BtnSaveCurrent";
            this.BtnSaveCurrent.Size = new System.Drawing.Size(80, 23);
            this.BtnSaveCurrent.TabIndex = 5;
            this.BtnSaveCurrent.Text = "Save Current Picture";
            this.BtnSaveCurrent.UseVisualStyleBackColor = true;
            this.BtnSaveCurrent.Click += new System.EventHandler(this.BtnSaveCurrent_Click);
            // 
            // BtnSaveAll
            // 
            this.BtnSaveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnSaveAll.Location = new System.Drawing.Point(439, 410);
            this.BtnSaveAll.Name = "BtnSaveAll";
            this.BtnSaveAll.Size = new System.Drawing.Size(63, 23);
            this.BtnSaveAll.TabIndex = 6;
            this.BtnSaveAll.Text = "Save All Pictures";
            this.BtnSaveAll.UseVisualStyleBackColor = true;
            this.BtnSaveAll.Click += new System.EventHandler(this.BtnSaveAll_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExit.Location = new System.Drawing.Point(602, 410);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(53, 23);
            this.BtnExit.TabIndex = 7;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // DBGridPersonel
            // 
            this.DBGridPersonel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DBGridPersonel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DBGridPersonel.Location = new System.Drawing.Point(8, 8);
            this.DBGridPersonel.Name = "DBGridPersonel";
            this.DBGridPersonel.RowHeadersWidth = 62;
            this.DBGridPersonel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DBGridPersonel.Size = new System.Drawing.Size(333, 425);
            this.DBGridPersonel.TabIndex = 8;
            this.DBGridPersonel.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DBGridPersonel_CellClick);
            // 
            // NightlyTimer
            // 
            this.NightlyTimer.Enabled = true;
            this.NightlyTimer.Interval = 60000;
            this.NightlyTimer.Tick += new System.EventHandler(this.NightlyTimer_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 458);
            this.Controls.Add(this.DBGridPersonel);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnSaveAll);
            this.Controls.Add(this.BtnSaveCurrent);
            this.Controls.Add(this.PicBox);
            this.Controls.Add(this.TxtImagesFolder);
            this.Controls.Add(this.LblFolderImages);
            this.Controls.Add(this.BtnSelectFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
        private System.Windows.Forms.Timer NightlyTimer;
    }
}