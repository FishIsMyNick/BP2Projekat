using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models.NonContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("Ulica", "Broj", "PosBr", "OZND")]
    public class Lokacija : _DbClass
    {
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public int PosBr { get; set; }
        public string OZND { get; set; }

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("Ulica", Ulica),
                new ClassPropertyValue("Broj", Broj),
                new ClassPropertyValue("PosBr", PosBr),
                new ClassPropertyValue("OZND", OZND)
            };
        }
        public Lokacija() : base() { }

        public Lokacija(string ulica, string broj, int posBr, string oZND)
        {
            Ulica = ulica ?? throw new ArgumentNullException(nameof(ulica));
            Broj = broj ?? throw new ArgumentNullException(nameof(broj));
            PosBr = posBr;
            OZND = oZND ?? throw new ArgumentNullException(nameof(oZND));
        }
    }
}
