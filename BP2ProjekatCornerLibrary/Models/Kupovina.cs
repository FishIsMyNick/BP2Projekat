using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Kupovina
{
    public int IDKup { get; set; }

    public int IDSStivo { get; set; }

    public int IDClan { get; set; }

    public int IDBK { get; set; }

    public DateTime DatVr { get; set; }

    public int? PotvrdioKup { get; set; }

    public DateTime? DatVrPot { get; set; }

    public virtual SerijskoStivoULokalu ISStivoULokaluNavigation { get; set; } = null!;

    public virtual Clan IDClanNavigation { get; set; } = null!;

    public virtual Radnik? PotvrdioKupNavigation { get; set; }
}
