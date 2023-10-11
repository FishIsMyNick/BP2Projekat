using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Zahtevzaserijskostivo
{
    public string Naziv { get; set; } = null!;

    public string Jezik { get; set; } = null!;

    public int Tip { get; set; }

    public int Idclan { get; set; }

    public int Idbk { get; set; }

    public DateTime DatVr { get; set; }

    public bool Ispunjen { get; set; }

    public virtual Biblikutak IdbkNavigation { get; set; } = null!;

    public virtual Clan IdclanNavigation { get; set; } = null!;
}
