using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerVB6.Model
{
    public class RegexFound
    {
        public enum LeMotClef : byte
        {
            Api32 = 0,
            CreateObject = 1,
            ExcelApp = 2,
            WordApp = 3,
            UserCtrl = 4
        }

        public LeMotClef MotClef { get; set; }
        public int Position { get; set; } = 0;
        public string? ExtraitDeLigne { get; set; }

    }
}
