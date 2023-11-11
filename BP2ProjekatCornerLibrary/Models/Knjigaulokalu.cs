using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Knjigaulokalu
{
    public Knjigaulokalu()
    {
    
    }
    public Knjigaulokalu(int IDKnjiga, int IDBK, int kolicina)
    {
        Idknjiga = IDKnjiga;
        Idbk = IDBK;
        Kolicina = kolicina;
    }
    public int Idknjiga { get; set; }

    public int Idbk { get; set; }

    public int Kolicina { get; set; }

    public virtual Biblikutak IdbkNavigation { get; set; } = null!;

    public virtual Knjiga IdknjigaNavigation { get; set; } = null!;

    public virtual ICollection<Ispunjenzahtevknjiga> Ispunjenzahtevknjigas { get; set; } = new List<Ispunjenzahtevknjiga>();

    public virtual ICollection<Rezervacija> Rezervacijas { get; set; } = new List<Rezervacija>();
}
