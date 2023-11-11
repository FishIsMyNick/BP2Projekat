using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Pripadazanru
{
    public Pripadazanru()
    {
        
    }
    public Pripadazanru(int knjiga, string zanr)
    {
        Idknjiga = knjiga;
        Oznz = zanr;
    }
    public int Idknjiga { get; set; }

    public string Oznz { get; set; } = null!;
}
