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
    public class Drzava : _DbClass
    {
        [Key]
        public string OZND { get; set; }
        [Required]
        public string NazivDrzave { get; set; }

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("OZND", OZND)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "OZND", "NazivDrzave" };
        }

        public Drzava() : base() { }
        public Drzava(string oznd, string naziv)
        {
            OZND = oznd;
            NazivDrzave = naziv;
        }
    }
}
