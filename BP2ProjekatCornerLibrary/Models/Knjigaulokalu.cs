using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class KnjigaULokalu
{
    public int IDKnjiga { get; set; }

    public int IDBK { get; set; }

    public int Kolicina { get; set; }

    public virtual Biblikutak IDBKNavigation { get; set; } = null!;

	public virtual Knjiga IDKnjigaNavigation { get; set; } = null!;

    public virtual ICollection<IspunjenZahtevKnjiga> IspunjenZahtevKnjigas { get; set; } = new List<IspunjenZahtevKnjiga>();

    public virtual ICollection<Rezervacija> Rezervacijas { get; set; } = new List<Rezervacija>();
}
