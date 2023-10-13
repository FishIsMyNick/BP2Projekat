using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Izdkuca
{
    public int Idik { get; set; }

    public string Naziv { get; set; } = null!;

    public string Ulica { get; set; } = null!;

    public string Broj { get; set; } = null!;

    public int PosBr { get; set; }

    public string Oznd { get; set; } = null!;

    public virtual Lokacija Lokacija { get; set; } = null!;

    public virtual ICollection<Knjiga> Idknjigas { get; set; } = new List<Knjiga>();

    public virtual ICollection<Serijskostivo> Idsstivos { get; set; } = new List<Serijskostivo>();
}
