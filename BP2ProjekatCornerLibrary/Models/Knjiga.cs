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

    public virtual ICollection<Knjigaulokalu> KnjigaULokalus { get; set; } = new List<Knjigaulokalu>();

    public virtual ICollection<Ocenaknjige> OcenaKnjiges { get; set; } = new List<Ocenaknjige>();

    public virtual ICollection<Autor> IDAutors { get; set; } = new List<Autor>();

    public virtual ICollection<Izdkuca> IDIKs { get; set; } = new List<Izdkuca>();

    public virtual ICollection<Jezik> OZNJs { get; set; } = new List<Jezik>();

    public virtual ICollection<Zanr> OZNZs { get; set; } = new List<Zanr>();
}
