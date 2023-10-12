using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Ispunjenzahtevknjiga
{
    public int Idclan { get; set; }

    public int Idknjiga { get; set; }

    public int Idbk { get; set; }

    public DateTime DatVrIsp { get; set; }

    public bool Procitano { get; set; }

    public virtual Knjigaulokalu Id { get; set; } = null!;

    public virtual Clan IdclanNavigation { get; set; } = null!;
}
