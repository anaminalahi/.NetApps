namespace ScannerVB6
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
            TabPage6 = new TabPage();
            DtgListeDesUserCtrl = new DataGridView();
            DtgListeDesClasses = new DataGridView();
            TabPage5 = new TabPage();
            DtgListeDesModules = new DataGridView();
            TabPage4 = new TabPage();
            DtgListeDesActiveX = new DataGridView();
            TabPage3 = new TabPage();
            DtgListeDesReferences = new DataGridView();
            DtgListeDesFormulaires = new DataGridView();
            TabPage1 = new TabPage();
            TabloDynamique = new TabControl();
            TabPage2 = new TabPage();
            CMF = new OpenFileDialog();
            BtnQuitter = new Button();
            BtnAnalyse = new Button();
            BtnExporter = new Button();
            DtgListeDesProjets = new DataGridView();
            DtgListeDesRegeX = new DataGridView();
            BtnRecuperer = new Button();
            TabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DtgListeDesUserCtrl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DtgListeDesClasses).BeginInit();
            TabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DtgListeDesModules).BeginInit();
            TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DtgListeDesActiveX).BeginInit();
            TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DtgListeDesReferences).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DtgListeDesFormulaires).BeginInit();
            TabPage1.SuspendLayout();
            TabloDynamique.SuspendLayout();
            TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DtgListeDesProjets).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DtgListeDesRegeX).BeginInit();
            SuspendLayout();
            // 
            // TabPage6
            // 
            TabPage6.Controls.Add(DtgListeDesUserCtrl);
            TabPage6.Location = new Point(4, 24);
            TabPage6.Name = "TabPage6";
            TabPage6.Size = new Size(1410, 317);
            TabPage6.TabIndex = 5;
            TabPage6.Text = "UserControls";
            TabPage6.UseVisualStyleBackColor = true;
            // 
            // DtgListeDesUserCtrl
            // 
            DtgListeDesUserCtrl.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DtgListeDesUserCtrl.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DtgListeDesUserCtrl.Location = new Point(6, 5);
            DtgListeDesUserCtrl.Margin = new Padding(3, 2, 3, 2);
            DtgListeDesUserCtrl.Name = "DtgListeDesUserCtrl";
            DtgListeDesUserCtrl.RowHeadersWidth = 51;
            DtgListeDesUserCtrl.RowTemplate.Height = 29;
            DtgListeDesUserCtrl.Size = new Size(1398, 307);
            DtgListeDesUserCtrl.TabIndex = 10;
            // 
            // DtgListeDesClasses
            // 
            DtgListeDesClasses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DtgListeDesClasses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DtgListeDesClasses.Location = new Point(6, 5);
            DtgListeDesClasses.Margin = new Padding(3, 2, 3, 2);
            DtgListeDesClasses.Name = "DtgListeDesClasses";
            DtgListeDesClasses.RowHeadersWidth = 51;
            DtgListeDesClasses.RowTemplate.Height = 29;
            DtgListeDesClasses.Size = new Size(1398, 307);
            DtgListeDesClasses.TabIndex = 9;
            DtgListeDesClasses.CellContentClick += DtgListeDesClasses_CellContentClick;
            // 
            // TabPage5
            // 
            TabPage5.Controls.Add(DtgListeDesClasses);
            TabPage5.Location = new Point(4, 24);
            TabPage5.Name = "TabPage5";
            TabPage5.Size = new Size(1410, 317);
            TabPage5.TabIndex = 4;
            TabPage5.Text = "Classes";
            TabPage5.UseVisualStyleBackColor = true;
            // 
            // DtgListeDesModules
            // 
            DtgListeDesModules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DtgListeDesModules.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DtgListeDesModules.Location = new Point(6, 5);
            DtgListeDesModules.Margin = new Padding(3, 2, 3, 2);
            DtgListeDesModules.Name = "DtgListeDesModules";
            DtgListeDesModules.RowHeadersWidth = 51;
            DtgListeDesModules.RowTemplate.Height = 29;
            DtgListeDesModules.Size = new Size(1398, 307);
            DtgListeDesModules.TabIndex = 8;
            DtgListeDesModules.CellContentClick += DtgListeDesModules_CellContentClick;
            // 
            // TabPage4
            // 
            TabPage4.Controls.Add(DtgListeDesModules);
            TabPage4.Location = new Point(4, 24);
            TabPage4.Name = "TabPage4";
            TabPage4.Padding = new Padding(3);
            TabPage4.Size = new Size(1410, 317);
            TabPage4.TabIndex = 3;
            TabPage4.Text = "Modules";
            TabPage4.UseVisualStyleBackColor = true;
            // 
            // DtgListeDesActiveX
            // 
            DtgListeDesActiveX.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DtgListeDesActiveX.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DtgListeDesActiveX.Location = new Point(6, 5);
            DtgListeDesActiveX.Margin = new Padding(3, 2, 3, 2);
            DtgListeDesActiveX.Name = "DtgListeDesActiveX";
            DtgListeDesActiveX.RowHeadersWidth = 51;
            DtgListeDesActiveX.RowTemplate.Height = 29;
            DtgListeDesActiveX.Size = new Size(1398, 307);
            DtgListeDesActiveX.TabIndex = 8;
            // 
            // TabPage3
            // 
            TabPage3.Controls.Add(DtgListeDesActiveX);
            TabPage3.Location = new Point(4, 24);
            TabPage3.Name = "TabPage3";
            TabPage3.Padding = new Padding(3);
            TabPage3.Size = new Size(1410, 317);
            TabPage3.TabIndex = 2;
            TabPage3.Text = "Active Ocx";
            TabPage3.UseVisualStyleBackColor = true;
            // 
            // DtgListeDesReferences
            // 
            DtgListeDesReferences.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DtgListeDesReferences.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DtgListeDesReferences.Location = new Point(6, 5);
            DtgListeDesReferences.Margin = new Padding(3, 2, 3, 2);
            DtgListeDesReferences.Name = "DtgListeDesReferences";
            DtgListeDesReferences.RowHeadersWidth = 51;
            DtgListeDesReferences.RowTemplate.Height = 29;
            DtgListeDesReferences.Size = new Size(1398, 307);
            DtgListeDesReferences.TabIndex = 7;
            // 
            // DtgListeDesFormulaires
            // 
            DtgListeDesFormulaires.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DtgListeDesFormulaires.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DtgListeDesFormulaires.Location = new Point(6, 5);
            DtgListeDesFormulaires.Margin = new Padding(3, 2, 3, 2);
            DtgListeDesFormulaires.Name = "DtgListeDesFormulaires";
            DtgListeDesFormulaires.RowHeadersWidth = 51;
            DtgListeDesFormulaires.RowTemplate.Height = 29;
            DtgListeDesFormulaires.Size = new Size(1398, 307);
            DtgListeDesFormulaires.TabIndex = 6;
            DtgListeDesFormulaires.CellContentClick += DtgListeDesFormulaires_CellContentClick;
            // 
            // TabPage1
            // 
            TabPage1.Controls.Add(DtgListeDesFormulaires);
            TabPage1.Location = new Point(4, 24);
            TabPage1.Name = "TabPage1";
            TabPage1.Padding = new Padding(3);
            TabPage1.Size = new Size(1410, 317);
            TabPage1.TabIndex = 0;
            TabPage1.Text = "Formulaires";
            TabPage1.UseVisualStyleBackColor = true;
            // 
            // TabloDynamique
            // 
            TabloDynamique.Controls.Add(TabPage1);
            TabloDynamique.Controls.Add(TabPage2);
            TabloDynamique.Controls.Add(TabPage3);
            TabloDynamique.Controls.Add(TabPage4);
            TabloDynamique.Controls.Add(TabPage5);
            TabloDynamique.Controls.Add(TabPage6);
            TabloDynamique.Location = new Point(13, 272);
            TabloDynamique.Name = "TabloDynamique";
            TabloDynamique.SelectedIndex = 0;
            TabloDynamique.Size = new Size(1418, 345);
            TabloDynamique.TabIndex = 24;
            // 
            // TabPage2
            // 
            TabPage2.Controls.Add(DtgListeDesReferences);
            TabPage2.Location = new Point(4, 24);
            TabPage2.Name = "TabPage2";
            TabPage2.Padding = new Padding(3);
            TabPage2.Size = new Size(1410, 317);
            TabPage2.TabIndex = 1;
            TabPage2.Text = "Références DLL";
            TabPage2.UseVisualStyleBackColor = true;
            // 
            // BtnQuitter
            // 
            BtnQuitter.Location = new Point(1314, 856);
            BtnQuitter.Margin = new Padding(3, 2, 3, 2);
            BtnQuitter.Name = "BtnQuitter";
            BtnQuitter.Size = new Size(113, 36);
            BtnQuitter.TabIndex = 22;
            BtnQuitter.Text = "Quitter";
            BtnQuitter.UseVisualStyleBackColor = true;
            BtnQuitter.Click += BtnQuitter_Click;
            // 
            // BtnAnalyse
            // 
            BtnAnalyse.Location = new Point(14, 11);
            BtnAnalyse.Margin = new Padding(3, 2, 3, 2);
            BtnAnalyse.Name = "BtnAnalyse";
            BtnAnalyse.Size = new Size(113, 40);
            BtnAnalyse.TabIndex = 20;
            BtnAnalyse.Text = "Analyser";
            BtnAnalyse.UseVisualStyleBackColor = true;
            BtnAnalyse.Click += BtnAnalyse_Click;
            // 
            // BtnExporter
            // 
            BtnExporter.Location = new Point(17, 853);
            BtnExporter.Margin = new Padding(3, 2, 3, 2);
            BtnExporter.Name = "BtnExporter";
            BtnExporter.Size = new Size(113, 40);
            BtnExporter.TabIndex = 21;
            BtnExporter.Text = "Exporter CSV";
            BtnExporter.UseVisualStyleBackColor = true;
            BtnExporter.Click += BtnExporter_Click;
            // 
            // DtgListeDesProjets
            // 
            DtgListeDesProjets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DtgListeDesProjets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DtgListeDesProjets.Location = new Point(17, 66);
            DtgListeDesProjets.Margin = new Padding(3, 2, 3, 2);
            DtgListeDesProjets.Name = "DtgListeDesProjets";
            DtgListeDesProjets.RowHeadersWidth = 51;
            DtgListeDesProjets.RowTemplate.Height = 29;
            DtgListeDesProjets.Size = new Size(1410, 188);
            DtgListeDesProjets.TabIndex = 23;
            DtgListeDesProjets.CellContentClick += DtgListeDesProjets_CellContentClick;
            // 
            // DtgListeDesRegeX
            // 
            DtgListeDesRegeX.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DtgListeDesRegeX.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DtgListeDesRegeX.Location = new Point(17, 643);
            DtgListeDesRegeX.Margin = new Padding(3, 2, 3, 2);
            DtgListeDesRegeX.Name = "DtgListeDesRegeX";
            DtgListeDesRegeX.RowHeadersWidth = 51;
            DtgListeDesRegeX.RowTemplate.Height = 29;
            DtgListeDesRegeX.Size = new Size(1410, 188);
            DtgListeDesRegeX.TabIndex = 25;
            // 
            // BtnRecuperer
            // 
            BtnRecuperer.Location = new Point(153, 11);
            BtnRecuperer.Margin = new Padding(3, 2, 3, 2);
            BtnRecuperer.Name = "BtnRecuperer";
            BtnRecuperer.Size = new Size(113, 40);
            BtnRecuperer.TabIndex = 26;
            BtnRecuperer.Text = "Récuperer";
            BtnRecuperer.UseVisualStyleBackColor = true;
            BtnRecuperer.Click += BtnRecuperer_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1445, 904);
            Controls.Add(BtnRecuperer);
            Controls.Add(DtgListeDesRegeX);
            Controls.Add(TabloDynamique);
            Controls.Add(BtnQuitter);
            Controls.Add(BtnAnalyse);
            Controls.Add(BtnExporter);
            Controls.Add(DtgListeDesProjets);
            Name = "FormMain";
            Text = "FormMain";
            TabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DtgListeDesUserCtrl).EndInit();
            ((System.ComponentModel.ISupportInitialize)DtgListeDesClasses).EndInit();
            TabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DtgListeDesModules).EndInit();
            TabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DtgListeDesActiveX).EndInit();
            TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DtgListeDesReferences).EndInit();
            ((System.ComponentModel.ISupportInitialize)DtgListeDesFormulaires).EndInit();
            TabPage1.ResumeLayout(false);
            TabloDynamique.ResumeLayout(false);
            TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DtgListeDesProjets).EndInit();
            ((System.ComponentModel.ISupportInitialize)DtgListeDesRegeX).EndInit();
            ResumeLayout(false);
        }

        #endregion

        internal TabPage TabPage6;
        internal DataGridView DtgListeDesUserCtrl;
        internal DataGridView DtgListeDesClasses;
        internal TabPage TabPage5;
        internal DataGridView DtgListeDesModules;
        internal TabPage TabPage4;
        internal DataGridView DtgListeDesActiveX;
        internal TabPage TabPage3;
        internal DataGridView DtgListeDesReferences;
        internal DataGridView DtgListeDesFormulaires;
        internal TabPage TabPage1;
        internal TabControl TabloDynamique;
        internal TabPage TabPage2;
        internal OpenFileDialog CMF;
        internal Button BtnQuitter;
        internal Button BtnAnalyse;
        internal Button BtnExporter;
        internal DataGridView DtgListeDesProjets;
        internal DataGridView DtgListeDesRegeX;
        internal Button BtnRecuperer;
    }
}