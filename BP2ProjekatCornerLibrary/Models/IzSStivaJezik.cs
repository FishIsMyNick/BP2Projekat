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
    [PrimaryKey("IDSStivo", "IDBib", "DatVr", "OZNJ")]
    public class IzSStivaJezik : _DbClass
    {
        public int IDSStivo { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public string OZNJ { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDSStivo", IDSStivo),
                new ClassPropertyValue("IDBib", IDBib),
                new ClassPropertyValue("DatVr", DatVr),
                new ClassPropertyValue("OZNJ", OZNJ)
            };
        }

        public IzSStivaJezik() : base() { }

        public IzSStivaJezik(int iDSStivo, string oZNJ, int iDBib)
        {
            IDSStivo = iDSStivo;
            IDBib = iDBib;
            OZNJ = oZNJ ?? throw new ArgumentNullException(nameof(oZNJ));
            DatVr = DateTime.Now;
        }
    }
}
