using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Mesto
{
    public int PosBr { get; set; }

    public string NazivMesta { get; set; } = null!;

    public virtual ICollection<Lokacija> Lokacijas { get; set; } = new List<Lokacija>();

    public virtual ICollection<Drzava> OZNDs { get; set; } = new List<Drzava>();
}
