using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Rezervacija
{
    public int IDRez { get; set; }

    public int IDKnjiga { get; set; }

    public int IDClan { get; set; }

    public int IDBK { get; set; }

    public DateTime DatVr { get; set; }

    public int? PotvrdioRez { get; set; }

    public DateTime? DatVrPot { get; set; }

    public int? ZakljucioRez { get; set; }

    public DateTime? DatVrZak { get; set; }

    public virtual KnjigaULokalu IDKULNavigation { get; set; } = null!;

    public virtual Clan IDClanNavigation { get; set; } = null!;

    public virtual Radnik? PotvrdioRezNavigation { get; set; }

    public virtual Radnik? ZakljucioRezNavigation { get; set; }
}
