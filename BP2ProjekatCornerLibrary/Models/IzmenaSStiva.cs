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
    [PrimaryKey("IDSStivo", "IDBib", "DatVr")]
    public class IzmenaSStiva : _DbClass
    {
        public int IDSStivo { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public string? Naziv { get; set; }
        public int? TipStiva { get; set; }
        public string? Format { get; set; }
        public string? Period { get; set; }
        public DateTime? DatBrisanja { get; set; }

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDSStivo", IDSStivo),
                new ClassPropertyValue("IDBib", IDBib),
                new ClassPropertyValue("DatVr", DatVr)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDSStivo", "IDBib", "DatVr", "Naziv", "TipStiva", "Format", "Period", "DatBrisanja" };
        }

        public IzmenaSStiva() : base() { }
        public IzmenaSStiva(SerijskoStivo newSS, int idBib) : base()
        {
            IDSStivo = newSS.IDSStivo;
            IDBib = idBib;
            DatVr = DateTime.Now;

            Naziv = newSS.Naziv;
            TipStiva = newSS.TipStiva;
            Format = newSS.Format;
            Period = newSS.Period;
            DatBrisanja = newSS.DatBrisanja;
        }
    }
}
