using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerVB6.Model
{
    public class RefComp
    {
         public enum LeType : byte
        {
            Ocx = 0,
            Ref = 1
        }

        public required string NomApplication { get; set; }
        public required string NomDuProjet { get; set; }
        public LeType MotClef { get; set; }
        public string? NomDuFichierRefOCxTlb { get; set; }
        public string? libelleRefOCxTlb { get; set; }
        public string? ExtraitDeLigneVBP { get; set; }

    }
}
