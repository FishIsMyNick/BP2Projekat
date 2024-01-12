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
    [PrimaryKey("IDKnjiga", "IDBib", "DatVr", "OZNZ")]
    public class IzKnjigeZanr : _DbClass
    {
        public int IDKnjiga { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public string OZNZ { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDKnjiga", IDKnjiga),
                new ClassPropertyValue("IDBib", IDBib),
                new ClassPropertyValue("DatVr", DatVr),
                new ClassPropertyValue("OZNZ", OZNZ)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDKnjiga", "IDBib", "DatVr", "OZNZ" };
        }

        public IzKnjigeZanr() : base() { }

        public IzKnjigeZanr(IzmenaKnjige izK, string oZNZ) :base()
        {
            IDKnjiga = izK.IDKnjiga;
            IDBib = izK.IDBib;
            OZNZ = oZNZ ?? throw new ArgumentNullException(nameof(oZNZ));
            DatVr = izK.DatVr;
        }
    }
}
