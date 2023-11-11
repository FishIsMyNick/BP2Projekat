using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Zanr
{
    public Zanr()
    {
        
    }
    public Zanr(string oznz, string zanr)
    {
        Oznz = oznz;
        NazivZanra = zanr; 
    }
    public string Oznz { get; set; } = null!;

    public string NazivZanra { get; set; } = null!;

    public virtual ICollection<Knjiga> Idknjigas { get; set; } = new List<Knjiga>();
}
