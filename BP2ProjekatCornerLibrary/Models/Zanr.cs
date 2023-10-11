using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Zanr
{
    public string OZNZ { get; set; } = null!;

    public string NazivZanra { get; set; } = null!;

    public virtual ICollection<Knjiga> IDKnjigas { get; set; } = new List<Knjiga>();
}
