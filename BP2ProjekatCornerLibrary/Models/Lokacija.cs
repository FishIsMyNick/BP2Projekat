using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Lokacija
{
    public string Ulica { get; set; } = null!;

    public string Broj { get; set; } = null!;

    public int PosBr { get; set; }

    public string Oznd { get; set; } = null!;

    public virtual Adresa Adresa { get; set; } = null!;

    public virtual ICollection<Biblikutak> Biblikutaks { get; set; } = new List<Biblikutak>();

    public virtual ICollection<Clan> Clans { get; set; } = new List<Clan>();

    public virtual ICollection<Izdkuca> Izdkucas { get; set; } = new List<Izdkuca>();

    public virtual ICollection<Izmenalokacije> Izmenalokacijes { get; set; } = new List<Izmenalokacije>();

    public virtual Drzava OzndNavigation { get; set; } = null!;

    public virtual Mesto PosBrNavigation { get; set; } = null!;
}
