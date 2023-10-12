using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Serijskostivoulokalu
{
    public int Idsstivo { get; set; }

    public int Idbk { get; set; }

    public int Kolicina { get; set; }

    public virtual Biblikutak IdbkNavigation { get; set; } = null!;

    public virtual Serijskostivo IdsstivoNavigation { get; set; } = null!;

    public virtual ICollection<Ispunjenzahtevserijskostivo> Ispunjenzahtevserijskostivos { get; set; } = new List<Ispunjenzahtevserijskostivo>();

    public virtual ICollection<Kupovina> Kupovinas { get; set; } = new List<Kupovina>();
}
