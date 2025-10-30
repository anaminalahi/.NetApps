using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScannerVB6.Model
{
    public class ProjetVBP
    {
        public required string NomApplication { get; set; }
        public required string NomDuFichierVBP { get; set; }
        public required string TypeDeProjet { get; set; }
        public required string NomDuProjet { get; set; }
        public string? NomDeCompilation { get; set; }
        public string? Startup { get; set; }
        public string? Emplacement { get; set; }


        public int NombreReferenceDLL { get; set; } = 0;
        public int NombreDeFormulaire { get; set; } = 0;
        public int NombreDActiveX { get; set; } = 0;
        public int NombreDeModuleBas { get; set; } = 0;
        public int NombreDeClasse { get; set; } = 0;
        public int NombreDeCreateObject { get; set; } = 0;
        public int NombreDApi32 { get; set; } = 0;
        public int NombreDeExcelApp { get; set; } = 0;
        public int NombreDeWordApp { get; set; } = 0;
        public int NombreDeOfficeouAutre { get; set; } = 0;
        public int NombreDeUserControles { get; set; } = 0;
        public int NombreDeLignesActives { get; set; } = 0;


        public List<ReferenceDLL> ListeDeReference { get; set; } = new List<ReferenceDLL>();
        public List<ObjectActiveX> ListeDActiveX { get; set; } = new List<ObjectActiveX>();
        public List<UserControle> ListeUserControles { get; set; } = new List<UserControle>();
        public List<FicSource> ListeFormulaire { get; set; } = new List<FicSource>();
        public List<FicSource> ListeDeModule { get; set; } = new List<FicSource>();
        public List<FicSource> ListeClasse { get; set; } = new List<FicSource>();

    }
}
