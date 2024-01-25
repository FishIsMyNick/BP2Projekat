using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP2ProjekatCornerLibrary.Helpers.Classes;
using BP2ProjekatCornerLibrary.Models.NonContext;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Mesto : _DbClass
    {
        [Key]
        public int PosBr { get; set; }
        [Required]
        public string NazivMesta  { get; set; }

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("PosBr", PosBr)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "PosBr", "NazivMesta" };
        }

        public Mesto() : base() { }

        public Mesto(int posBr, string nazivMesta)
        {
            PosBr = posBr;
            NazivMesta = nazivMesta;
        }
    }
}
