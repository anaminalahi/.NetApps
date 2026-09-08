using System;
using System.Collections.Generic;

namespace BTP.Modeles.Db;

public partial class Chantier
{
    public int IdChantier { get; set; }

    public string LibelleChantier { get; set; } = null!;

    public DateTime DateDebut { get; set; }

    public DateTime DateFin { get; set; }

    public bool Statut { get; set; }

    public int ProjetsIdProjet { get; set; }

    public virtual Projet ProjetsIdProjetNavigation { get; set; } = null!;
}
