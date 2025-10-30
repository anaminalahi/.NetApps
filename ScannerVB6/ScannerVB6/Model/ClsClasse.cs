using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerVB6.Model
{
    public class ClsClasse
    {
        public required string NomDeLaClsClasse { get; set; }
        public required string NomDuFichierCLS { get; set; }
        public int NombreDeUserControles { get; set; }
        public int NombreDeCreateObject { get; set; }
        public int NombreDApi32 { get; set; }
        public int NombreDeExcelApp { get; set; }
        public int NombreDeWordApp { get; set; }
        public int NombreDeOfficeouAutre { get; set; }
        public int NombreDeLignesActives { get; set; }
        public List<RegexFound>? ListofRegeX { get; set; } = new List<RegexFound>();

    }
}
