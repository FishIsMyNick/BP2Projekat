using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Knjiga
{
    public int IDKnjiga { get; set; }

    public string Naziv { get; set; } = null!;

    public DateTime? GodIzd { get; set; }

    public int? BrIzd { get; set; }

    public bool? Ograniceno { get; set; }

    public virtual ICollection<KnjigaULokalu> KnjigaULokalus { get; set; } = new List<KnjigaULokalu>();

    public virtual ICollection<OcenaKnjige> OcenaKnjiges { get; set; } = new List<OcenaKnjige>();

    public virtual ICollection<Autor> IDAutors { get; set; } = new List<Autor>();

    public virtual ICollection<IzdKuca> IDIKs { get; set; } = new List<IzdKuca>();

    public virtual ICollection<Jezik> OZNJs { get; set; } = new List<Jezik>();

    public virtual ICollection<Zanr> OZNZs { get; set; } = new List<Zanr>();
}
