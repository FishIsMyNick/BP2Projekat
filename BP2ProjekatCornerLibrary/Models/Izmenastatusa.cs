using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class IzmenaStatusa
{
    public int IDStatus { get; set; }

    public int IDClan { get; set; }

    public int? Tip { get; set; }

    public double? Prosek { get; set; }

    public bool? Budzet { get; set; }

    public bool? Zaposlen { get; set; }

    public DateTime DatVr { get; set; }

    public virtual Clan IDClanNavigation { get; set; } = null!;
}
