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
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDKnjiga", IDKnjiga),
                new ClassPropertyValue("IDBib", IDBib),
                new ClassPropertyValue("DatVr", DatVr)
            };
        }
        public IzmenaKnjige() : base() { }

        public IzmenaKnjige(Knjiga k, int IdBib) : base()
        {
            IDKnjiga = k.IDKnjiga;
            IDBib = IdBib;
            DatVr = DateTime.Now;

            Knjiga postojeca = DBHelper.GetBook(IDKnjiga);

            Naziv = postojeca.Naziv != k.Naziv ? k.Naziv : null;
            BrIzd = postojeca.BrIzd != k.BrIzd ? k.BrIzd : null;
            GodIzd = postojeca.GodIzd != k.GodIzd ? k.GodIzd : null;
            VrIzd = postojeca.VrIzd != k.VrIzd ? k.VrIzd : null;
            BrStrana = postojeca.BrStrana != k.BrStrana ? k.BrStrana : null;
            VelicinaFonta = postojeca.VelicinaFonta != k.VelicinaFonta ? k.VelicinaFonta : null;
            Korice = postojeca.Korice != k.Korice ? k.Korice : null;
            Ograniceno = postojeca.Ograniceno != k.Ograniceno ? k.Ograniceno : null;
            Format = postojeca.Format != k.Format ? k.Format : null;
        }
    }
}
