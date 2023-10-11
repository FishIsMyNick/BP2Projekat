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

    public virtual ICollection<IzmenaKredita> IzmenaKredita { get; set; } = new List<IzmenaKredita>();

    public virtual ICollection<KnjigaULokalu> KnjigaULokalus { get; set; } = new List<KnjigaULokalu>();

    public virtual Lokacija Lokacija { get; set; } = null!;

    public virtual ICollection<Radnik> Radniks { get; set; } = new List<Radnik>();

    public virtual ICollection<SerijskoStivoULokalu> SerijskoStivoULokalus { get; set; } = new List<SerijskoStivoULokalu>();

    public virtual ICollection<ZahtevZaKnjigu> ZahtevZaKnjigus { get; set; } = new List<ZahtevZaKnjigu>();

    public virtual ICollection<ZahtevZaSerijskoStivo> ZahtevZaSerijskoStivos { get; set; } = new List<ZahtevZaSerijskoStivo>();
}
