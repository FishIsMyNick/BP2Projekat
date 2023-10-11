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

    public string OZND { get; set; } = null!;

    public virtual ICollection<IspunjenZahtevKnjiga> IspunjenZahtevKnjigas { get; set; } = new List<IspunjenZahtevKnjiga>();

    public virtual ICollection<IspunjenZahtevSerijskoStivo> IspunjenZahtevSerijskoStivos { get; set; } = new List<IspunjenZahtevSerijskoStivo>();

    public virtual ICollection<IstorijaClanarina> IstorijaClanarinas { get; set; } = new List<IstorijaClanarina>();

    public virtual ICollection<IzmenaKredita> IzmenaKredita { get; set; } = new List<IzmenaKredita>();

    public virtual ICollection<IzmenaLokacije> IzmenaLokacijes { get; set; } = new List<IzmenaLokacije>();

    public virtual ICollection<IzmenaPodataka> IzmenaPodatakas { get; set; } = new List<IzmenaPodataka>();

    public virtual ICollection<IzmenaSifre> IzmenaSifres { get; set; } = new List<IzmenaSifre>();

    public virtual ICollection<IzmenaStatusa> IzmenaStatusas { get; set; } = new List<IzmenaStatusa>();

    public virtual ICollection<Kupovina> Kupovinas { get; set; } = new List<Kupovina>();

    public virtual Lokacija Lokacija { get; set; } = null!;

    public virtual ICollection<OcenaKnjige> OcenaKnjiges { get; set; } = new List<OcenaKnjige>();

    public virtual ICollection<OcenaSStiva> OcenaSStivas { get; set; } = new List<OcenaSStiva>();

    public virtual ICollection<Rezervacija> Rezervacijas { get; set; } = new List<Rezervacija>();

    public virtual ICollection<ZahtevZaKnjigu> ZahtevZaKnjigus { get; set; } = new List<ZahtevZaKnjigu>();

    public virtual ICollection<ZahtevZaSerijskoStivo> ZahtevZaSerijskoStivos { get; set; } = new List<ZahtevZaSerijskoStivo>();
}
