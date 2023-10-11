using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class ZahtevZaSerijskoStivo
{
    public string Naziv { get; set; } = null!;

    public string Jezik { get; set; } = null!;

    public int Tip { get; set; }

    public int IDClan { get; set; }

    public int IDBK { get; set; }

    public DateTime DatVr { get; set; }

    public bool Ispunjen { get; set; }

    public virtual Biblikutak IDBKNavigation { get; set; } = null!;

    public virtual Clan IDClanNavigation { get; set; } = null!;
}
