using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models.NonContext;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Zanr : _DbClass
    {
        [Key]
        public string OZNZ { get; set; }
        [Required]
        public string NazivZanra { get; set; }

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("OZNZ", OZNZ)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "OZNZ", "NazivZanra" };
        }

        public Zanr() : base() { }
        public Zanr(string oznz, string naziv)
        {
            OZNZ = oznz;
            NazivZanra = naziv;
        }
    }
}
