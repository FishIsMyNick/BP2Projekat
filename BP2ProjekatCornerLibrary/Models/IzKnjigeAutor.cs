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
    [PrimaryKey("IDKnjiga", "IDBib", "DatVr", "IDAutor")]
    public class IzKnjigeAutor : _DbClass
    {
        public int IDKnjiga { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public int IDAutor { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDKnjiga", IDKnjiga),
                new ClassPropertyValue("IDBib", IDBib),
                new ClassPropertyValue("DatVr", DatVr),
                new ClassPropertyValue("IDAutor", IDAutor)
            };
        }
        public IzKnjigeAutor() : base() { }

        public IzKnjigeAutor(int iDKnjiga, int iDBib, int iDAutor)
        {
            IDKnjiga = iDKnjiga;
            IDBib = iDBib;
            IDAutor = iDAutor;
            DatVr = DateTime.Now;
        }
        public IzKnjigeAutor(Pise p, int idBib)
                    {
            IDKnjiga = p.IDKnjiga;
            IDAutor = p.IDAutor;
            IDBib = idBib;
            DatVr = DateTime.Now;
        }
    }
}
