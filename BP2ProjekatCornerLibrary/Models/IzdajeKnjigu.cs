using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Izdajeknjigu
{
    public int Idknjiga { get; set; }

    public int Idik { get; set; }

    public virtual Izdkuca IdikNavigation { get; set; } = null!;
}
