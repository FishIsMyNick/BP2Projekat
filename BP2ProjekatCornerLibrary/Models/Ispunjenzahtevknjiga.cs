using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class IspunjenZahtevKnjiga
{
    public int IDClan { get; set; }

    public int IDKnjiga { get; set; }

    public int IDBK { get; set; }

    public DateTime DatVrIsp { get; set; }

    public bool Procitano { get; set; }

    public virtual KnjigaULokalu IDKULNavigation { get; set; } = null!;

    public virtual Clan IDClanNavigation { get; set; } = null!;
}
