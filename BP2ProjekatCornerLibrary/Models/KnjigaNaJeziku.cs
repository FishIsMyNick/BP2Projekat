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

        public static bool operator ==(KnjigaNaJeziku obj1, KnjigaNaJeziku obj2)
        {
            return (obj1.IDKnjiga == obj2.IDKnjiga
                        && obj1.OZNJ == obj2.OZNJ);
        }

        // this is second one '!='
        public static bool operator !=(KnjigaNaJeziku obj1, KnjigaNaJeziku obj2)
        {
            return (obj1.IDKnjiga != obj2.IDKnjiga
                        && obj1.OZNJ != obj2.OZNJ);
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is KnjigaNaJeziku)) return false;
            return (obj as KnjigaNaJeziku).IDKnjiga == this.IDKnjiga && (obj as KnjigaNaJeziku).OZNJ == this.OZNJ;
        }
    }
}
