using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Istorijaclanarina
{
    public int Idcl { get; set; }

    public int Idclan { get; set; }

    public string Oznc { get; set; } = null!;

    public DateTime DatUvoda { get; set; }

    public DateTime DatVrStart { get; set; }

    public DateTime DatVrUplate { get; set; }

    public virtual Clanarina Clanarina { get; set; } = null!;

    public virtual Clan IdclanNavigation { get; set; } = null!;
}
