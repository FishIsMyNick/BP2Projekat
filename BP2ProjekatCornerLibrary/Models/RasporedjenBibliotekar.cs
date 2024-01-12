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
    [PrimaryKey("IDBib", "IDBK", "DatVr")]
    public class RasporedjenBibliotekar : _DbClass
    {
        public int IDBib { get; set; }
        public int IDBK { get; set; }
        public DateTime DatVr { get; set; }
        public DateTime DatOd { get; set; }
        public DateTime DatDo { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDBib", IDBib),
                new ClassPropertyValue("IDBK", IDBK),
                new ClassPropertyValue("DatVr", DatVr)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDBib", "IDBK", "DatVr", "DatOd", "DatDo" };
        }

        public RasporedjenBibliotekar() : base() { }
    }
}
