using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Radnik
{
    public int IDRadnik { get; set; }

    public string KorisnickoIme { get; set; } = null!;

    public string Sifra { get; set; } = null!;

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public DateTime DatRodj { get; set; }

    public DateTime DatZap { get; set; }

    public int Tip { get; set; }

    public int? IDBK { get; set; }

    public virtual Biblikutak? IDBKNavigation { get; set; }

    public virtual ICollection<Kupovina> Kupovinas { get; set; } = new List<Kupovina>();

    public virtual ICollection<Rezervacija> RezervacijaPotvrdioRezNavigations { get; set; } = new List<Rezervacija>();

    public virtual ICollection<Rezervacija> RezervacijaZakljucioRezNavigations { get; set; } = new List<Rezervacija>();
}
