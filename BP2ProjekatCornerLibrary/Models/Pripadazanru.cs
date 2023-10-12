using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Pripadazanru
{
    public int Idknjiga { get; set; }

    public string Oznz { get; set; } = null!;

    public virtual Zanr OznzNavigation { get; set; } = null!;
}
