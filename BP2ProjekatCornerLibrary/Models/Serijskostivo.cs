using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Serijskostivo
{
    public int Idsstivo { get; set; }

    public string Naziv { get; set; } = null!;

    public int Cena { get; set; }

    public DateTime? DatIzd { get; set; }

    public int? BrIzd { get; set; }

    public int Tip { get; set; }

    public string? PeriodIzd { get; set; }

    public virtual ICollection<Ocenasstiva> Ocenasstivas { get; set; } = new List<Ocenasstiva>();

    public virtual Periodicnost? PeriodIzdNavigation { get; set; }

    public virtual ICollection<Serijskostivoulokalu> Serijskostivoulokalus { get; set; } = new List<Serijskostivoulokalu>();

    public virtual ICollection<Izdkuca> Idiks { get; set; } = new List<Izdkuca>();

    public virtual ICollection<Jezik> Oznjs { get; set; } = new List<Jezik>();
}
