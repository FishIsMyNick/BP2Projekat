using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Jezik
{
    public string Oznj { get; set; } = null!;

    public string NazivJezika { get; set; } = null!;

    public virtual ICollection<Knjiganajeziku> Knjiganajezikus { get; set; } = new List<Knjiganajeziku>();

    public virtual ICollection<Serijskostivo> Idsstivos { get; set; } = new List<Serijskostivo>();
}
