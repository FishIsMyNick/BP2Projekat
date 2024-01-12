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
    public class Jezik : _DbClass
    {
        [Key]
        public string OZNJ { get; set; }
        [Required]
        public string NazivJezika { get; set; }

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("OZNJ", OZNJ)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "OZNJ", "NazivJezika" };
        }

        public Jezik() : base() { } 
        public Jezik(string oznj, string naziv)
        {
            OZNJ = oznj;
            NazivJezika = naziv;
        }
    }
}
