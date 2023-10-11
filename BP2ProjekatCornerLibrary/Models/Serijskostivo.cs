using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class SerijskoStivo
{
    public int IDSStivo { get; set; }

    public string Naziv { get; set; } = null!;

    public int Cena { get; set; }

    public DateTime? DatIzd { get; set; }

    public int? BrIzd { get; set; }

    public int Tip { get; set; }

    public string? PeriodIzd { get; set; }

    public virtual ICollection<OcenaSStiva> OcenaSStivas { get; set; } = new List<OcenaSStiva>();

    public virtual Periodicnost? PeriodIzdNavigation { get; set; }

    public virtual ICollection<SerijskoStivoULokalu> SerijskoStivoULokalus { get; set; } = new List<SerijskoStivoULokalu>();

    public virtual ICollection<IzdKuca> IDIKs { get; set; } = new List<IzdKuca>();

    public virtual ICollection<Jezik> OZNJs { get; set; } = new List<Jezik>();
}
