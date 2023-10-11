using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class IzmenaLokacije
{
    public int IDIL { get; set; }

    public int IDClan { get; set; }

    public string Ulica { get; set; } = null!;

    public string Broj { get; set; } = null!;

    public int PosBr { get; set; }

    public string OZND { get; set; } = null!;

    public DateTime DatVr { get; set; }

    public virtual Clan IDClanNavigation { get; set; } = null!;

    public virtual Lokacija Lokacija { get; set; } = null!;
}
