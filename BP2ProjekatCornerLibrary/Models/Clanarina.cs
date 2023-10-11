using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Clanarina
{
    public string OZNC { get; set; } = null!;

    public DateTime DatUvoda { get; set; }

    public string NazivClanarine { get; set; } = null!;

    public double Cena { get; set; }

    public bool Izbacena { get; set; }

    public virtual ICollection<IstorijaClanarina> IstorijaClanarinas { get; set; } = new List<IstorijaClanarina>();
}
