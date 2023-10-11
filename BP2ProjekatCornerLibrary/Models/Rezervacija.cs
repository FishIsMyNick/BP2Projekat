using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Rezervacija
{
    public int Idrez { get; set; }

    public int Idknjiga { get; set; }

    public int Idclan { get; set; }

    public int Idbk { get; set; }

    public DateTime DatVr { get; set; }

    public int? PotvrdioRez { get; set; }

    public DateTime? DatVrPot { get; set; }

    public int? ZakljucioRez { get; set; }

    public DateTime? DatVrZak { get; set; }

    public virtual Knjigaulokalu Id { get; set; } = null!;

    public virtual Clan IdclanNavigation { get; set; } = null!;

    public virtual Radnik? PotvrdioRezNavigation { get; set; }

    public virtual Radnik? ZakljucioRezNavigation { get; set; }
}
