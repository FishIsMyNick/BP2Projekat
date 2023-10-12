using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Autor
{
    public int Idautor { get; set; }

    public string Ime { get; set; } = null!;

    public string? Prezime { get; set; }

    public DateTime? DatRodj { get; set; }

    public string? Biografija { get; set; }

    public string? Oznd { get; set; }

    public virtual Drzava? OzndNavigation { get; set; }

    public virtual ICollection<Pise> Pises { get; set; } = new List<Pise>();
}
