using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Clanarina
{
    public string Oznc { get; set; } = null!;

    public DateTime DatUvoda { get; set; }

    public string Clanarina1 { get; set; } = null!;

    public double Cena { get; set; }

    public bool Izbacena { get; set; }

    public virtual ICollection<Istorijaclanarina> Istorijaclanarinas { get; set; } = new List<Istorijaclanarina>();
}
