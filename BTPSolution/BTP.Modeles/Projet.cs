namespace BTP.Modeles
{
    public class Projet
    {
        public int IdProjet { get; set; }
        public string NomProjet { get; set; }
        public string Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int IdChefProjet { get; set; }

        // Constructeur par défaut
        public Projet(int idProjet, string nomProjet, string description, DateTime dateDebut, DateTime dateFin, int idChefProjet)
        {
            IdProjet = idProjet;
            NomProjet = nomProjet;
            Description = description;
            DateDebut = dateDebut;
            DateFin = dateFin;
            IdChefProjet = idChefProjet;

        }
    }

}
