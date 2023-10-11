using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Periodicnost
{
    public string PeriodIzd { get; set; } = null!;

    public virtual ICollection<SerijskoStivo> SerijskoStivos { get; set; } = new List<SerijskoStivo>();
}
