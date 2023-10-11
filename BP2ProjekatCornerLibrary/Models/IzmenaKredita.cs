using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class IzmenaKredita
{
    public int IDKredit { get; set; }

    public int IDClan { get; set; }

    public int IDBK { get; set; }

    public int Kolicina { get; set; }

    public DateTime DatVr { get; set; }

    public virtual Biblikutak IDBKNavigation { get; set; } = null!;

    public virtual Clan IDClanNavigation { get; set; } = null!;
}
