using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class SerijskoStivoULokalu
{
    public int IDSStivo { get; set; }

    public int IDBK { get; set; }

    public int Kolicina { get; set; }

    public virtual Biblikutak IDBKNavigation { get; set; } = null!;

    public virtual SerijskoStivo IDSStivoNavigation { get; set; } = null!;

    public virtual ICollection<IspunjenZahtevSerijskoStivo> IspunjenZahtevSerijskoStivos { get; set; } = new List<IspunjenZahtevSerijskoStivo>();

    public virtual ICollection<Kupovina> Kupovinas { get; set; } = new List<Kupovina>();
}
