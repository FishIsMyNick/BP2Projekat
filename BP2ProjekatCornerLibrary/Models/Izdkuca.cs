using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class IzdKuca
{
    public int IDIK { get; set; }

    public string Naziv { get; set; } = null!;

    public string Ulica { get; set; } = null!;

    public string Broj { get; set; } = null!;

    public int PosBr { get; set; }

    public string OZND { get; set; } = null!;

    public virtual Lokacija Lokacija { get; set; } = null!;

    public virtual ICollection<Knjiga> IDKnjigas { get; set; } = new List<Knjiga>();

    public virtual ICollection<SerijskoStivo> IDSStivos { get; set; } = new List<SerijskoStivo>();
}
