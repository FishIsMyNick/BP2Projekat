using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Biblikutak
{
    public int Idbk { get; set; }

    public string Naziv { get; set; } = null!;

    public string Ulica { get; set; } = null!;

    public string Broj { get; set; } = null!;

    public int PosBr { get; set; }

    public string Oznd { get; set; } = null!;

    public virtual ICollection<Izmenakreditum> Izmenakredita { get; set; } = new List<Izmenakreditum>();

    public virtual ICollection<Knjigaulokalu> Knjigaulokalus { get; set; } = new List<Knjigaulokalu>();

    public virtual Lokacija Lokacija { get; set; } = null!;

    public virtual ICollection<Radnik> Radniks { get; set; } = new List<Radnik>();

    public virtual ICollection<Serijskostivoulokalu> Serijskostivoulokalus { get; set; } = new List<Serijskostivoulokalu>();

    public virtual ICollection<Zahtevzaknjigu> Zahtevzaknjigus { get; set; } = new List<Zahtevzaknjigu>();

    public virtual ICollection<Zahtevzaserijskostivo> Zahtevzaserijskostivos { get; set; } = new List<Zahtevzaserijskostivo>();
}
