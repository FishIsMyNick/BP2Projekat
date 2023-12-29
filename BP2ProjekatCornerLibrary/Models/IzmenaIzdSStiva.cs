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
    [PrimaryKey("IDSStivo", "BrIzd", "IDBib", "DatVr")]
    public class IzmenaIzdSStiva : _DbClass
    {
        public int IDSStivo { get; set; }
        public int BrIzd { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public DateTime? DatIzd { get; set; }
        public decimal? Cena { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDSStivo", IDSStivo),
                new ClassPropertyValue("BrIzd", BrIzd),
                new ClassPropertyValue("IDBib", IDBib),
                new ClassPropertyValue("DatVr", DatVr)
            };
        }

        public IzmenaIzdSStiva() : base() { }
        public IzmenaIzdSStiva(IzdanjeSStiva iss, int idBib) : base()
        {
            IDSStivo = iss.IDSStivo;
            BrIzd = iss.BrIzd;
            IDBib = idBib;
            DatVr = DateTime.Now;

            IzdanjeSStiva postojece = DBHelper.GetIzdanjeSStiva(IDSStivo, BrIzd);

            DatIzd = postojece.DatIzd != iss.DatIzd ? iss.DatIzd : null;
            Cena = postojece.Cena != iss.Cena ? iss.Cena : null;
        }
    }
}
