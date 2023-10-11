using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class IzmenaSifre
{
    public int IDSifra { get; set; }

    public int IDClan { get; set; }

    public string Sifra { get; set; } = null!;

    public DateTime DatVr { get; set; }

    public virtual Clan IDClanNavigation { get; set; } = null!;
}
