using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Autor
{
    public Autor()
    {
        
    }
    public Autor(int autorID, string ime, string prezime, DateTime datRodj, string biografija)
    {
        Idautor = autorID;
        Ime = ime;
        Prezime = prezime;
        DatRodj = datRodj;
        Biografija = biografija;
    }
    public int Idautor { get; set; }

    public string Ime { get; set; } = null!;

    public string? Prezime { get; set; }

    public DateTime? DatRodj { get; set; }

    public string? Biografija { get; set; }

    public string? Oznd { get; set; }

    public virtual Drzava? OzndNavigation { get; set; }

    public virtual ICollection<Knjiga> Idknjigas { get; set; } = new List<Knjiga>();
}
