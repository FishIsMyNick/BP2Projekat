using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Zanr
{
    public string Oznz { get; set; } = null!;

    public string Zanr1 { get; set; } = null!;

    public virtual ICollection<Knjiga> Idknjigas { get; set; } = new List<Knjiga>();
}
