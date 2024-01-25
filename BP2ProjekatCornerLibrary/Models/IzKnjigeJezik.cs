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
    [PrimaryKey("IDKnjiga", "IDBib", "DatVr", "OZNJ")]
    public class IzKnjigeJezik : _DbClass
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

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDKnjiga", "IDBib", "DatVr", "OZNJ" };
        }

        public IzKnjigeJezik() : base() { }

        public IzKnjigeJezik(IzmenaKnjige izK, string oZNJ) : base() 
        {
            IDKnjiga = izK.IDKnjiga;
            IDBib = izK.IDBib;
            OZNJ = oZNJ ?? throw new ArgumentNullException(nameof(oZNJ));
            DatVr = izK.DatVr;
        }
    }
}
