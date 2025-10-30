using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerVB6.Model
{
    public class FicSource
    {
        public string? NomIntFicSource { get; set; }
        public string? NomExtFicSource { get; set; }
        public string? Emplacement { get; set; }
        public string? NomApplication { get; set; }
        public string? NomDuFichierVBP { get; set; }

        public TypeSource.TypeFichier TypeFicSource { get; set; }

        public int NombreDeCreateObject { get; set; } = 0;
        public int NombreDApi32 { get; set; } = 0;
        public int NombreDeExcelApp { get; set; } = 0;
        public int NombreDeWordApp { get; set; } = 0;
        public int NombreDeOfficeouAutre { get; set; } = 0;
        public int NombreDeUserControles { get; set; } = 0;
        public int NombreDeLignesActives { get; set; } = 0;

        public List<RegexFound> ListofRegeX { get; set; } = new List<RegexFound>();



    }
}
