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
    [PrimaryKey("IDKnjiga", "IDBib", "DatVr", "OZNJ")]
    public class IzKnjigeJezik :_DbClass
    {
        public int IDKnjiga { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public string OZNJ { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDKnjiga", IDKnjiga),
                new ClassPropertyValue("IDBib", IDBib),
                new ClassPropertyValue("DatVr", DatVr),
                new ClassPropertyValue("OZNJ", OZNJ)
            };
        }

        public IzKnjigeJezik() : base() { }

        public IzKnjigeJezik(int iDKnjiga, string oZNJ, int iDBib, DateTime? datVr = null)
        {
            IDKnjiga = iDKnjiga;
            IDBib = iDBib;
            OZNJ = oZNJ ?? throw new ArgumentNullException(nameof(oZNJ));
            DatVr = datVr == null ? DateTime.Now : (DateTime)datVr;
        }
    }
}
