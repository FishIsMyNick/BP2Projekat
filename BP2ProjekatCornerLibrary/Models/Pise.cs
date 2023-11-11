using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Pise
{
    public Pise()
    {
    
    }
    public Pise(int knjiga, int autor)
    {
        Idknjiga = knjiga;
        Idautor = autor;
    }
    public int Idknjiga { get; set; }

    public int Idautor { get; set; }
}
