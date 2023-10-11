using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Kupovina
{
    public int Idkup { get; set; }

    public int Idsstivo { get; set; }

    public int Idclan { get; set; }

    public int Idbk { get; set; }

    public DateTime DatVr { get; set; }

    public int? PotvrdioKup { get; set; }

    public DateTime? DatVrPot { get; set; }

    public virtual Serijskostivoulokalu Id { get; set; } = null!;

    public virtual Clan IdclanNavigation { get; set; } = null!;

    public virtual Radnik? PotvrdioKupNavigation { get; set; }
}
