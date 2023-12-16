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
    [PrimaryKey("IDKnjiga", "OZNJ")]
    public class KnjigaNaJeziku : _DbClass
    {
        public int IDKnjiga { get; set; }
        public string OZNJ { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDKnjiga", IDKnjiga),
                new ClassPropertyValue("OZNJ", OZNJ)
            };
        }


        public KnjigaNaJeziku() : base() { }
        public KnjigaNaJeziku(int IDKnjiga, string OZNJ)
        {
            this.IDKnjiga = IDKnjiga;
            this.OZNJ = OZNJ;
        }
    }
}
