using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Knjiga
{
    public int Idknjiga { get; set; }

    public string Naziv { get; set; } = null!;

    public DateTime? GodIzd { get; set; }

    public int? BrIzd { get; set; }

    public bool? Ograniceno { get; set; }

    public virtual ICollection<Knjigaulokalu> Knjigaulokalus { get; set; } = new List<Knjigaulokalu>();

    public virtual ICollection<Ocenaknjige> Ocenaknjiges { get; set; } = new List<Ocenaknjige>();

    public virtual ICollection<Autor> Idautors { get; set; } = new List<Autor>();

    public virtual ICollection<Izdkuca> Idiks { get; set; } = new List<Izdkuca>();

    public virtual ICollection<Jezik> Oznjs { get; set; } = new List<Jezik>();

    public virtual ICollection<Zanr> Oznzs { get; set; } = new List<Zanr>();
}
