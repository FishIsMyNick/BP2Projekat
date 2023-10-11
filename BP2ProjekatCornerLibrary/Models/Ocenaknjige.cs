using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Ocenaknjige
{
    public int IdocenaK { get; set; }

    public int Idclan { get; set; }

    public int Idknjiga { get; set; }

    public DateTime DatVr { get; set; }

    public int Ocena { get; set; }

    public string? Komentar { get; set; }

    public virtual Clan IdclanNavigation { get; set; } = null!;

    public virtual Knjiga IdknjigaNavigation { get; set; } = null!;
}
