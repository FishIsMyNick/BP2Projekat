using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Knjiganajeziku
{
    public int Idknjiga { get; set; }

    public string Oznj { get; set; } = null!;

    public virtual Jezik OznjNavigation { get; set; } = null!;
}
