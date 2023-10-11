using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Jezik
{
    public string OZNJ { get; set; } = null!;

    public string NazivJezika { get; set; } = null!;

    public virtual ICollection<Knjiga> IDKnjigas { get; set; } = new List<Knjiga>();

    public virtual ICollection<SerijskoStivo> IDSStivos { get; set; } = new List<SerijskoStivo>();
}
