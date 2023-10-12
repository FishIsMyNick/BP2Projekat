using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Izmenastatusa
{
    public int Idstatus { get; set; }

    public int Idclan { get; set; }

    public int? Tip { get; set; }

    public double? Prosek { get; set; }

    public bool? Budzet { get; set; }

    public bool? Zaposlen { get; set; }

    public DateTime DatVr { get; set; }

    public virtual Clan IdclanNavigation { get; set; } = null!;
}
