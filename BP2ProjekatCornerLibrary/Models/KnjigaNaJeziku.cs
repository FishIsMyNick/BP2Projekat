using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Knjiganajeziku
{
    public Knjiganajeziku()
    {
        
    }
    public Knjiganajeziku(int knjiga, string jezik)
    {
        Idknjiga = knjiga;
        Oznj = jezik;
    }
    public int Idknjiga { get; set; }

    public string Oznj { get; set; } = null!;
}
