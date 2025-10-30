namespace ScannerVB6.Model
{
    public class ModuleBas
    {
        public required string NomDuModule { get; set; }
        public required string NomDuFichierBAS { get; set; }
        public int NombreDeUserControles { get; set; } = 0;
        public int NombreDeCreateObject { get; set; } = 0;
        public int NombreDApi32 { get; set; } = 0;
        public int NombreDeExcelApp { get; set; } = 0;
        public int NombreDeWordApp { get; set; } = 0;
        public int NombreDeOfficeouAutre { get; set; } = 0;
        public int NombreDeLignesActives { get; set; } = 0;

        public List<RegexFound> ListofRegeX { get; set; } = new List<RegexFound>();

    }
}
