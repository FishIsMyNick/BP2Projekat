using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Jezik
{
    public string Oznj { get; set; } = null!;

    public string Jezik1 { get; set; } = null!;

    public virtual ICollection<Knjiga> Idknjigas { get; set; } = new List<Knjiga>();

    public virtual ICollection<Serijskostivo> Idsstivos { get; set; } = new List<Serijskostivo>();
}
