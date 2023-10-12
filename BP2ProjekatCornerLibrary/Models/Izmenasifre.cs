using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Izmenasifre
{
    public int Idsifra { get; set; }

    public int Idclan { get; set; }

    public string Sifra { get; set; } = null!;

    public DateTime DatVr { get; set; }

    public virtual Clan IdclanNavigation { get; set; } = null!;
}
