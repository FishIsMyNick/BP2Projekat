using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class OcenaKnjige
{
    public int IDOcenaK { get; set; }

    public int IDClan { get; set; }

    public int IDKnjiga { get; set; }

    public DateTime DatVr { get; set; }

    public int Ocena { get; set; }

    public string? Komentar { get; set; }

    public virtual Clan IDClanNavigation { get; set; } = null!;

    public virtual Knjiga IDKnjigaNavigation { get; set; } = null!;
}
