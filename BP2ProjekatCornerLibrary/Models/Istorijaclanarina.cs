using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class IstorijaClanarina
{
    public int IDCL { get; set; }

    public int IDClan { get; set; }

    public string OZNC { get; set; } = null!;

    public DateTime DatUvoda { get; set; }

    public DateTime DatVrStart { get; set; }

    public DateTime DatVrUplate { get; set; }

    public virtual Clanarina Clanarina { get; set; } = null!;

    public virtual Clan IDClanNavigation { get; set; } = null!;
}
