using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Zahtevzaknjigu
{
    public string Knjiga { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public string Jezik { get; set; } = null!;

    public int Idclan { get; set; }

    public int Idbk { get; set; }

    public DateTime DatVr { get; set; }

    public bool Ispunjen { get; set; }

    public virtual Biblikutak IdbkNavigation { get; set; } = null!;

    public virtual Clan IdclanNavigation { get; set; } = null!;
}
