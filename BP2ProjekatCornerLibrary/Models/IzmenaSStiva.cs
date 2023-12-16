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

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDSStivo", IDSStivo),
                new ClassPropertyValue("IDBib", IDBib),
                new ClassPropertyValue("DatVr", DatVr)
            };
        }

        public IzmenaSStiva() : base() { }
        public IzmenaSStiva(SerijskoStivo ss, int idBib) : base()
        {
            IDSStivo = ss.IDSStivo;
            IDBib = idBib;
            DatVr = DateTime.Now;

            SerijskoStivo postojece = DBHelper.GetSerijskoStivo(IDSStivo);

            Naziv = postojece.Naziv != ss.Naziv ? ss.Naziv : null;
            TipStiva = postojece.TipStiva != ss.TipStiva ? ss.TipStiva : null;
            Format = postojece.Format != ss.Format ? ss.Format : null;
            Period = postojece.Period != ss.Period ? ss.Period : null;
        }
    }
}
