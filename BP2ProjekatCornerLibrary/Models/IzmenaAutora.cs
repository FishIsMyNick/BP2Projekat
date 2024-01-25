using BP2ProjekatCornerLibrary.Helpers.Classes;
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
    [PrimaryKey("IDAutor", "IDBib", "DatVr")]
    public class IzmenaAutora : _DbClass
    {
        public int IDAutor { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        public DateTime? DatRodj { get; set; }
        public string? Biografija { get; set; }
        public string? Drzava { get; set; }
        public DateTime? DatBrisanja { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDAutor", IDAutor),
                new ClassPropertyValue("IDBib", IDBib),
                new ClassPropertyValue("DatVr", DatVr)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDAutor", "IDBib", "DatVr", "Ime", "Prezime", "DatRodj", "Biografija", "Drzava", "DatBrisanja" };
        }

        public IzmenaAutora() : base() { }

        public IzmenaAutora(int iDAutor, int iDBib, string? ime, string? prezime, DateTime? datRodj, string? biografija, string? drzava)
        {
            IDAutor = iDAutor;
            IDBib = iDBib;
            Ime = ime;
            Prezime = prezime;
            DatRodj = datRodj;
            Biografija = biografija;
            Drzava = drzava;
            DatVr = DateTime.Now;
        }
        public IzmenaAutora(Autor newAutor, int bibID)
        {
            IDAutor = newAutor.IDAutor;
            DatVr = DateTime.Now;
            IDBib = bibID;

            Ime = newAutor.Ime;
            Prezime = newAutor.Prezime;
            DatRodj = newAutor.DatRodj;
            Biografija = newAutor.Biografija;
            Drzava = newAutor.Drzava;
        }
    }
}
