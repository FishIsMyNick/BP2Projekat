using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Lokacija
{
    public string Ulica { get; set; } = null!;

    public string Broj { get; set; } = null!;

    public int PosBr { get; set; }

    public string OZND { get; set; } = null!;

    public virtual Adresa Adresa { get; set; } = null!;

    public virtual ICollection<Biblikutak> Biblikutaks { get; set; } = new List<Biblikutak>();

    public virtual ICollection<Clan> Clans { get; set; } = new List<Clan>();

    public virtual ICollection<IzdKuca> IzdKucas { get; set; } = new List<IzdKuca>();

    public virtual ICollection<IzmenaLokacije> IzmenaLokacijes { get; set; } = new List<IzmenaLokacije>();

    public virtual Drzava OZNDNavigation { get; set; } = null!;

    public virtual Mesto PosBrNavigation { get; set; } = null!;
}
