using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class IzmenaPodataka
{
    public int IDPodatak { get; set; }

    public int IDClan { get; set; }

    public string? Ime { get; set; }

    public string? Prezime { get; set; }

    public int? BrTel { get; set; }

    public DateTime DatVr { get; set; }

    public virtual Clan IDClanNavigation { get; set; } = null!;
}
