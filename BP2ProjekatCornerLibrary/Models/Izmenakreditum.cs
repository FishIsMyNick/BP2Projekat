using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Izmenakreditum
{
    public int Idkredit { get; set; }

    public int Idclan { get; set; }

    public int Idbk { get; set; }

    public int Kolicina { get; set; }

    public DateTime DatVr { get; set; }

    public virtual Biblikutak IdbkNavigation { get; set; } = null!;

    public virtual Clan IdclanNavigation { get; set; } = null!;
}
