using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Jezik
{
    public Jezik()
    {
        
    }
    public Jezik(string oznj, string jezik)
    {
        Oznj = oznj;
        NazivJezika = jezik;
    }
    public string Oznj { get; set; } = null!;

    public string NazivJezika { get; set; } = null!;

    public virtual ICollection<Knjiga> Idknjigas { get; set; } = new List<Knjiga>();

    public virtual ICollection<Serijskostivo> Idsstivos { get; set; } = new List<Serijskostivo>();
}
