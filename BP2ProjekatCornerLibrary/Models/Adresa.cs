using BP2ProjekatCornerLibrary.Helpers;
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

    public Adresa(params object[] args)
    {
        if (DBHelper.CheckDbNull(args[0]))
            Ulica = (string)args[0];
        if (DBHelper.CheckDbNull(args[1]))
            Broj = (string)args[1];
    }
}
