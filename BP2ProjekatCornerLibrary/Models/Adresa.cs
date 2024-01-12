using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models.NonContext;
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

    public override List<ClassPropertyValue> GetKeyProperties()
    {
        return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("Ulica", Ulica),
                new ClassPropertyValue("Broj", Broj)
            };
    }

    public override List<string> GetDbPropertyNames()
    {
        return new List<string> { "Ulica", "Broj" };
    }

    public Adresa() : base() { }

    public Adresa(string ulica, string broj)
    {
        Ulica = ulica ?? throw new ArgumentNullException(nameof(ulica));
        Broj = broj ?? throw new ArgumentNullException(nameof(broj));
    }
}
