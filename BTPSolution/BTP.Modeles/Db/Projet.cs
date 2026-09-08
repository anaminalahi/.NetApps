using System;
using System.Collections.Generic;

namespace BTP.Modeles.Db;

public partial class Projet
{
    public int IdProjet { get; set; }

    public string NomProjet { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime DateDebut { get; set; }

    public DateTime DateFin { get; set; }

    public string AspNetUsersId { get; set; } = null!;

    public virtual AspNetUser AspNetUsers { get; set; } = null!;

    public virtual ICollection<Chantier> Chantiers { get; set; } = new List<Chantier>();
}
