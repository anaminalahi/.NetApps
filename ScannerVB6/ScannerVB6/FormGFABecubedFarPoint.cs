using Microsoft.VisualBasic;
using ScannerVB6.Model;
using System.Data;

namespace ScannerVB6
{
    public partial class FormGFABecubedFarPoint : Form
    {

        #region Proprietes

        public List<ProjetVBP>? ListeDeProject { get; set; }
        public List<string>? ListeDesApplications { get; set; }
        public List<string>? ListeDesFichiersVBP { get; set; }
        public List<string>? ListeDesFichiersNonTrouves { get; set; }
        public List<FicSource>? ListeDesFichierSource { get; set; }
        public List<RefComp>? ListeDesRefOCx { get; set; }

        public ProjetVBP? SelectedProjetVBP { get; set; }
        public FicSource? SelectedFormulaire { get; set; }
        public FicSource? SelectedModule { get; set; }
        public FicSource? SelectedClsClasse { get; set; }
        public RefComp? SelectedRefComp { get; set; }

        #endregion

        public FormGFABecubedFarPoint()
        {
            InitializeComponent();
        }

        private void BtnRecuperer_Click(object sender, EventArgs e)
        {
            AnalyseBeceubed();

            AnalyseGobale();


        }

        private void AnalyseBeceubed()
        {
            ListeDesApplications = new List<string>()
            {
                "BAC"
            };

            ListeDesFichiersVBP = new List<string>()
            {
                @"BAC|C:\VBApps\Migration\BAC\ArreteCie\AgentciePropre.vbp",
                @"BAC|C:\VBApps\Migration\BAC\Journal\JournalPropre.vbp"
            };

            ListeDeProject = null;



        }

        private void AnalyseGobale()
        {
            try
            {
                string szChemin;
                string szApplication;
                string[] szArray0;

                ListeDeProject = new List<ProjetVBP>();
                ListeDesFichierSource = new List<FicSource>();
                ListeDesRefOCx = new List<RefComp>();
                ListeDesFichiersNonTrouves = new List<string>();

                foreach (var objProj in ListeDesFichiersVBP)
                {
                    szArray0 = objProj.Split("|");
                    szApplication = szArray0[0];
                    szChemin = szArray0[1];
                    CMF.FileName = szChemin;

                    using (var monStreamReader = new System.IO.StreamReader(new System.IO.FileStream(szChemin, System.IO.FileMode.Open), System.Text.Encoding.Latin1))
                    {
                        string ligne;
                        ligne = monStreamReader.ReadLine();

                        SelectedProjetVBP = new ProjetVBP()
                        {
                            NomApplication = szApplication,
                            NomDuFichierVBP = CMF.SafeFileName,
                            Emplacement = System.IO.Path.GetFullPath(szArray0[1].Replace(CMF.SafeFileName, string.Empty)),
                            TypeDeProjet = string.Empty, // Valeur par défaut, sera mise à jour plus tard
                            NomDuProjet = string.Empty   // Valeur par défaut, sera mise à jour plus tard
                        };

                        while ((ligne != null))
                        {

                            // Test Type de Projet
                            if ((ligne.StartsWith("Type")))
                            {
                                if ((ligne.Contains("Type=Exe")))
                                    SelectedProjetVBP.TypeDeProjet = "Exe";
                                else if ((ligne.Contains("Type=OleExe")))
                                    SelectedProjetVBP.TypeDeProjet = "OleExe";
                                else if ((ligne.Contains("OleDll")))
                                    SelectedProjetVBP.TypeDeProjet = "OleDll";
                                else if ((ligne.Contains("Control")))
                                    SelectedProjetVBP.TypeDeProjet = "Control";
                            }

                            // Test NomDuProjet
                            if ((ligne.StartsWith("Name")))
                                SelectedProjetVBP.NomDuProjet = ExtraireValeurEgal(ligne);

                            // Test NomDeCompilation
                            if ((ligne.StartsWith("ExeName32")))
                                SelectedProjetVBP.NomDeCompilation = ExtraireValeurEgal(ligne);

                            // Test Startup
                            if ((ligne.StartsWith("Startup")))
                                SelectedProjetVBP.Startup = ExtraireValeurEgal(ligne);

                            // Test Reference
                            if ((ligne.StartsWith("Reference")))
                            {
                                SelectedRefComp = new RefComp
                                {
                                    NomApplication = SelectedProjetVBP.NomApplication,
                                    NomDuProjet = SelectedProjetVBP.NomDuFichierVBP,
                                    ExtraitDeLigneVBP = ligne,
                                    MotClef = (RefComp.LeType)1
                                };

                                SelectedProjetVBP.ListeDeReference.Add(ExtraireReference(ligne));
                                SelectedProjetVBP.NombreReferenceDLL += 1;

                                ListeDesRefOCx.Add(SelectedRefComp);
                            }

                            // Test Object
                            if ((ligne.StartsWith("Object")))
                            {
                                SelectedRefComp = new RefComp
                                {
                                    NomApplication = SelectedProjetVBP.NomApplication,
                                    NomDuProjet = SelectedProjetVBP.NomDuFichierVBP,
                                    ExtraitDeLigneVBP = ligne,
                                    MotClef = 0
                                };

                                SelectedProjetVBP.ListeDActiveX.Add(ExtraireObject(ligne));
                                SelectedProjetVBP.NombreDActiveX += 1;

                                ListeDesRefOCx.Add(SelectedRefComp);
                            }

                            ligne = monStreamReader.ReadLine();
                        }

                        ListeDeProject.Add(SelectedProjetVBP);
                    }
                }

                //DtgListeDesProjets.DataSource = ListeDeProject;
                SelectedProjetVBP = ListeDeProject.FirstOrDefault();

                //ActualiserEcran();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string ExtraireValeurEgal(string objString)
        {
            var szArray = objString.Split("=");
            return szArray[1].Replace("\"", string.Empty);
        }


        private ObjectActiveX ExtraireObject(string objString)
        {
            var objActX = new ObjectActiveX();
            var szArray1 = objString.Split("=");
            string[] szArray2;

            if (szArray1[1].Where(x => Equals(@"\")).Count() > 1 && !szArray1[1].Contains(";"))
            {

                // Object =*\ A..\Communs\Controles\prjControles.vbp
                szArray2 = szArray1[1].Split(@"\");
                objActX.NomObjectActiveX = szArray2[szArray2.Length - 1].Trim();
            }
            else if (szArray1[1].Where(x => Equals(@"\")).Count() == 0 && szArray1[1].Contains(";"))
            {
                // Object = {831.0FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.1#0; mscomctl.ocx
                szArray2 = objString.Split(";");
                objActX.NomObjectActiveX = szArray2[1].Trim();
            }


            SelectedRefComp.NomDuFichierRefOCxTlb = objActX.NomObjectActiveX;

            szArray1 = null;
            szArray2 = null;

            return objActX;
        }


        private ReferenceDLL ExtraireReference(string objString)
        {
            var szArray1 = objString.Split("=");
            string[] szArray2;
            string[] szArray3;

            var objRef = new ReferenceDLL();

            if (szArray1[1].Where(x => Equals(@"\")).Count() >= 1)
            {
                if (szArray1[1].Where(x => Equals("#")).Count() == 0)
                {
                    // Reference =*\ A..\Server\Server.vbp
                    szArray2 = szArray1[1].Split(@"\");
                    objRef.NomDeLaReferenceDLL = szArray2[szArray2.Length - 1].Trim();
                    objRef.NomDuFichiereDLL = szArray1[1].TrimStart();
                }
                else
                {
                    // Reference =*\ G{91147A58-DFE4-47C0-8E76-987FC1A6001B}#3.0#0#C:\Program Files\Fichiers communs\MSSoap\Binaries\MSSOAP30.dll#Microsoft Soap Type Library v3.0
                    szArray2 = szArray1[1].Split("#");
                    objRef.NomDeLaReferenceDLL = szArray2[szArray2.Length - 1].Trim();
                    objRef.NomDuFichiereDLL = szArray2[szArray2.Length - 2].Trim();
                    szArray3 = szArray2[szArray2.Length - 2].Trim().Split(@"\");
                    SelectedRefComp.NomDuFichierRefOCxTlb = szArray3[szArray3.Length - 1].Trim();
                }
            }

            SelectedRefComp.libelleRefOCxTlb = objRef.NomDeLaReferenceDLL;

            szArray1 = null;
            szArray2 = null;
            szArray3 = null;

            return objRef;
        }


        private FicSource ExtraireFichier(string objLigne, byte oTypeFichier)
        {
            string[] szArray1, szArray2, szArray3;

            szArray1 = objLigne.Split("=");

            var objFicSource = new FicSource()
            {
                NomApplication = SelectedProjetVBP.NomApplication,
                NomDuFichierVBP = SelectedProjetVBP.NomDuFichierVBP,
                TypeFicSource = (TypeSource.TypeFichier)oTypeFichier
            };

            try
            {
                switch (oTypeFichier)
                {
                    case 0:
                        {
                            // Form=Feuilles\frmAccueil.frm
                            szArray2 = szArray1[1].Split(@"\");

                            objFicSource.NomIntFicSource = szArray2[1];
                            objFicSource.NomExtFicSource = szArray2[1];

                            objFicSource.Emplacement = SelectedProjetVBP.Emplacement.ToString() + szArray1[1];
                            break;
                        }

                    case 1:
                    case 2:
                        {
                            // Module=Demarrage;Modules\Demarrage.bas
                            // Class=clIntermediaire;Classes\clIntermediaire.cls
                            szArray2 = szArray1[1].Split(";");
                            szArray3 = szArray2[1].Split(@"\");

                            objFicSource.NomIntFicSource = szArray2[0].Trim();
                            objFicSource.NomExtFicSource = szArray3[1].Trim();

                            objFicSource.Emplacement = SelectedProjetVBP.Emplacement.ToString() + Strings.Trim(szArray2[1]);
                            break;
                        }
                }

                //alyserFichier(ref objFicSource);

                szArray1 = null;
                szArray2 = null;
                szArray3 = null;
            }
            catch (Exception ex)
            {
                throw;
            }

            return objFicSource;
        }


        private UserControle ExtraireUserCtrl(string objString)
        {
            var szArray1 = objString.Split("=");
            var objUserCtrl = new UserControle();
            objUserCtrl.NomUserControle = szArray1[1];
            szArray1 = null;
            return objUserCtrl;
        }

        private void BtnQuitter_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }
    }
}
