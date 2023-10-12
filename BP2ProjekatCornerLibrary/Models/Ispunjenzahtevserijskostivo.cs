using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Ispunjenzahtevserijskostivo
{
    public int Idclan { get; set; }

    public int Idsstivo { get; set; }

    public int Idbk { get; set; }

    public DateTime DatVrIsp { get; set; }

    public bool Procitano { get; set; }

    public virtual Serijskostivoulokalu Id { get; set; } = null!;

    public virtual Clan IdclanNavigation { get; set; } = null!;
}
