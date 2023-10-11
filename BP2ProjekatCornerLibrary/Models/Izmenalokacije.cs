using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Izmenalokacije
{
    public int Idil { get; set; }

    public int Idclan { get; set; }

    public string Ulica { get; set; } = null!;

    public string Broj { get; set; } = null!;

    public int PosBr { get; set; }

    public string Oznd { get; set; } = null!;

    public DateTime DatVr { get; set; }

    public virtual Clan IdclanNavigation { get; set; } = null!;

    public virtual Lokacija Lokacija { get; set; } = null!;
}
