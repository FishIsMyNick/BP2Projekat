using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Biblikutak
{
    public int IDBK { get; set; }

    public string Naziv { get; set; } = null!;

    public string Ulica { get; set; } = null!;

    public string Broj { get; set; } = null!;

    public int PosBr { get; set; }

    public string OZND { get; set; } = null!;

    public virtual ICollection<Izmenakreditum> IzmenaKredita { get; set; } = new List<Izmenakreditum>();

    public virtual ICollection<Knjigaulokalu> KnjigaULokalus { get; set; } = new List<Knjigaulokalu>();

    public virtual Lokacija Lokacija { get; set; } = null!;

    public virtual ICollection<Radnik> Radniks { get; set; } = new List<Radnik>();

    public virtual ICollection<Serijskostivoulokalu> SerijskoStivoULokalus { get; set; } = new List<Serijskostivoulokalu>();

    public virtual ICollection<Zahtevzaknjigu> ZahtevZaKnjigus { get; set; } = new List<Zahtevzaknjigu>();

    public virtual ICollection<Zahtevzaserijskostivo> ZahtevZaSerijskoStivos { get; set; } = new List<Zahtevzaserijskostivo>();
}
