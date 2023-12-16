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
    [PrimaryKey("PosBr", "OZND")]
    public class MestoUDrzavi : _DbClass
    {
        public int PosBr { get; set; }
        public string OZND { get; set; }

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("PosBr", PosBr),
                new ClassPropertyValue("OZND", OZND)
            };
        }
        public MestoUDrzavi() : base() { }

        public MestoUDrzavi(int posBr, string oZND)
        {
            PosBr = posBr;
            OZND = oZND ?? throw new ArgumentNullException(nameof(oZND));
        }
    }
}
