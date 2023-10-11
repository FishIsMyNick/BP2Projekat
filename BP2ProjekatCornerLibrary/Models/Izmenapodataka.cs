using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Izmenapodataka
{
    public int Idpodatak { get; set; }

    public int Idclan { get; set; }

    public string? Ime { get; set; }

    public string? Prezime { get; set; }

    public int? BrTel { get; set; }

    public DateTime DatVr { get; set; }

    public virtual Clan IdclanNavigation { get; set; } = null!;
}
