using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class OcenaSStiva
{
    public int IDOcenaS { get; set; }

    public int IDClan { get; set; }

    public int IDSStivo { get; set; }

    public DateTime DatVr { get; set; }

    public int Ocena { get; set; }

    public string? Komentar { get; set; }

    public virtual Clan IDClanNavigation { get; set; } = null!;

    public virtual SerijskoStivo IDSStivoNavigation { get; set; } = null!;
}
