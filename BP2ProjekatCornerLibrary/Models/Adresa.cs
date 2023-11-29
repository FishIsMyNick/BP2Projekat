using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BP2ProjekatCornerLibrary.Models;

[PrimaryKey("Ulica", "Broj")]
public class Adresa
{
    public string Ulica { get; set; } = null!;
    public string Broj { get; set; } = null!;
}
