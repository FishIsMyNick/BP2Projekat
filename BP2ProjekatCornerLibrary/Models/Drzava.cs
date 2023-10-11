using System;
using System.Collections.Generic;

namespace BP2ProjekatCornerLibrary.Models;

public partial class Drzava
{
    public string OZND { get; set; } = null!;

    public string NazivDrzave { get; set; } = null!;

    public virtual ICollection<Autor> Autors { get; set; } = new List<Autor>();

    public virtual ICollection<Lokacija> Lokacijas { get; set; } = new List<Lokacija>();

    public virtual ICollection<Mesto> PosBrs { get; set; } = new List<Mesto>();
}
