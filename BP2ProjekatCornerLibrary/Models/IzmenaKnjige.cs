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
    [PrimaryKey("IDKnjiga", "IDBib", "DatVr")]
    public class IzmenaKnjige : _DbClass
    {
        public int IDKnjiga { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public string? Naziv { get; set; }
        public int? BrIzd { get; set; }
        public int? GodIzd { get; set; }
        public string? VrIzd { get; set; }
        public int? BrStrana { get; set; }
        public int? VelicinaFonta { get; set; }
        public int? Korice { get; set; }
        public int? Ograniceno { get; set; }
        public string? Format { get; set; }
        public DateTime? DatBrisanja { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDKnjiga", IDKnjiga),
                new ClassPropertyValue("IDBib", IDBib),
                new ClassPropertyValue("DatVr", DatVr)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDKnjiga", "IDBib", "DatVr", "Naziv", "BrIzd", "GodIzd", "VrIzd", "BrStrana", "VelicinaFonta", "Korice", "Ograniceno", "Format", "DatBrisanja" };
        }

        public IzmenaKnjige() : base() { }

        public IzmenaKnjige(Knjiga newKnjiga, int IdBib) : base()
        {
            IDKnjiga = newKnjiga.IDKnjiga;
            IDBib = IdBib;
            DatVr = DateTime.Now;

            Naziv = newKnjiga.Naziv;
            BrIzd = newKnjiga.BrIzd;
            GodIzd = newKnjiga.GodIzd;
            VrIzd = newKnjiga.VrIzd;
            BrStrana = newKnjiga.BrStrana;
            VelicinaFonta = newKnjiga.VelicinaFonta;
            Korice = newKnjiga.Korice;
            Ograniceno = newKnjiga.Ograniceno;
            Format = newKnjiga.Format;
            DatBrisanja = newKnjiga.DatBrisanja;
        }
    }
}
