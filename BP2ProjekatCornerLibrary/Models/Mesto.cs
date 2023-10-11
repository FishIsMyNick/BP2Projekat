using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Mesto
{
    public int PosBr { get; set; }

    public string Mesto1 { get; set; } = null!;

    public virtual ICollection<Lokacija> Lokacijas { get; set; } = new List<Lokacija>();

    public virtual ICollection<Drzava> Oznds { get; set; } = new List<Drzava>();
}
