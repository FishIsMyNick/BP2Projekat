using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Pise
{
    public int Idknjiga { get; set; }

    public int Idautor { get; set; }

    public virtual Autor IdautorNavigation { get; set; } = null!;
}
