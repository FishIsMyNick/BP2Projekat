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
    [PrimaryKey("IDKnjiga", "IDIK")]
    public  class IzdajeKnjigu : _DbClass
    {
        public int IDKnjiga { get; set; }
        public int IDIK { get; set; }

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDKnjiga", IDKnjiga),
                new ClassPropertyValue("IDIK", IDIK)
            };
        }
        public IzdajeKnjigu() : base() { }
        public IzdajeKnjigu(int IDKnjiga, int IDIK)
        {
            this.IDKnjiga = IDKnjiga;
            this.IDIK = IDIK;
        }
    }
}
