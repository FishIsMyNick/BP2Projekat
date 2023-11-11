using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Izdajeknjigu
{
    public Izdajeknjigu()
    {
        
    }
    public Izdajeknjigu(int knjiga, int ik)
    {
        Idknjiga = knjiga;
        Idik = ik;
    }
    public int Idknjiga { get; set; }

    public int Idik { get; set; }
}
