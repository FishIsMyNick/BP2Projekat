using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Autor
{
    public int IDAutor { get; set; }

    public string Ime { get; set; } = null!;

    public string? Prezime { get; set; }

    public DateTime? DatRodj { get; set; }

    public string? Biografija { get; set; }

    public string? OZND { get; set; }

    public virtual Drzava? OZNDNavigation { get; set; }

    public virtual ICollection<Knjiga> IDKnjigas { get; set; } = new List<Knjiga>();
}
