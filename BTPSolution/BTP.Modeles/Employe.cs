using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTP.Modeles
{
    public class Employe
    {
        public int IdEmploye { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Poste { get; set; }
        public DateTime DateEmbauche { get; set; }

        public int IdChantier { get; set; }


    }
}
