using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Clan
{
    public int IDClan { get; set; }

    public string KorisnickoIme { get; set; } = null!;

    public string Sifra { get; set; } = null!;

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public DateTime DatRodj { get; set; }

    public string? BrTel { get; set; }

    public int Tip { get; set; }

    public string Ulica { get; set; } = null!;

    public string Broj { get; set; } = null!;

    public int PosBr { get; set; }

    public string Oznd { get; set; } = null!;

    public virtual ICollection<Ispunjenzahtevknjiga> Ispunjenzahtevknjigas { get; set; } = new List<Ispunjenzahtevknjiga>();

    public virtual ICollection<Ispunjenzahtevserijskostivo> Ispunjenzahtevserijskostivos { get; set; } = new List<Ispunjenzahtevserijskostivo>();

    public virtual ICollection<Istorijaclanarina> Istorijaclanarinas { get; set; } = new List<Istorijaclanarina>();

    public virtual ICollection<Izmenakreditum> Izmenakredita { get; set; } = new List<Izmenakreditum>();

    public virtual ICollection<Izmenalokacije> Izmenalokacijes { get; set; } = new List<Izmenalokacije>();

    public virtual ICollection<Izmenapodataka> Izmenapodatakas { get; set; } = new List<Izmenapodataka>();

    public virtual ICollection<Izmenasifre> Izmenasifres { get; set; } = new List<Izmenasifre>();

    public virtual ICollection<Izmenastatusa> Izmenastatusas { get; set; } = new List<Izmenastatusa>();

    public virtual ICollection<Kupovina> Kupovinas { get; set; } = new List<Kupovina>();

    public virtual Lokacija Lokacija { get; set; } = null!;

    public virtual ICollection<Ocenaknjige> Ocenaknjiges { get; set; } = new List<Ocenaknjige>();

    public virtual ICollection<Ocenasstiva> Ocenasstivas { get; set; } = new List<Ocenasstiva>();

    public virtual ICollection<Rezervacija> Rezervacijas { get; set; } = new List<Rezervacija>();

    public virtual ICollection<Zahtevzaknjigu> Zahtevzaknjigus { get; set; } = new List<Zahtevzaknjigu>();

    public virtual ICollection<Zahtevzaserijskostivo> Zahtevzaserijskostivos { get; set; } = new List<Zahtevzaserijskostivo>();
}
