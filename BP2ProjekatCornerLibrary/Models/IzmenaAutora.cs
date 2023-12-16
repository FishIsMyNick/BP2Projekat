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
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDAutor", IDAutor),
                new ClassPropertyValue("IDBib", IDBib),
                new ClassPropertyValue("DatVr", DatVr)
            };
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
        public IzmenaAutora(Autor a, int bibID)
        {
            IDAutor = a.IDAutor;
            DatVr = DateTime.Now;
            IDBib = bibID;

            Autor postojeci = DBHelper.GetAutor(a.IDAutor);

            Ime = postojeci.Ime != a.Ime? a.Ime : null;
            Prezime = postojeci.Prezime != a.Prezime ? a.Prezime : null;
            DatRodj = postojeci.DatRodj != a.DatRodj ? a.DatRodj : null;
            Biografija = postojeci.Biografija != a.Biografija ? a.Biografija : null;
            Drzava = postojeci.Drzava != a.Drzava ? a.Drzava : null;
        }
    }
}
