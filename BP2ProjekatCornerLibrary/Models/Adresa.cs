using BP2ProjekatCornerLibrary.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BP2ProjekatCornerLibrary.Models;

[PrimaryKey("Ulica", "Broj")]
public class Adresa : _DbClass
{
    public string Ulica { get; set; } = null!;
    public string Broj { get; set; } = null!;

    public Adresa() : base() { }
}
