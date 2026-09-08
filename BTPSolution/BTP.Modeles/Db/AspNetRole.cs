using System;
using System.Collections.Generic;

namespace BTP.Modeles.Db;

public partial class AspNetRole
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();
}
