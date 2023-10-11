using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Adresa
{
    public string Ulica { get; set; } = null!;

    public string Broj { get; set; } = null!;

    public virtual ICollection<Lokacija> Lokacijas { get; set; } = new List<Lokacija>();
}
