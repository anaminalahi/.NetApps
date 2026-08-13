using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTP.Modeles
{
    public class Chantier
    {
        public int IdChantier { get; set; }
        public required string LibelleChantier { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public required string Statut { get; set; }

        public int IdProjet { get; set; }
    }
}
