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
    public class Periodicnost : _DbClass
    {
        [Key]
        public string PeriodIzd { get; set; }
        [Required]
        public int Ucestalost { get; set; }

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("PeriodIzd", PeriodIzd)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "PeriodIzd", "Ucestalost" };
        }

        public Periodicnost() : base() { }

        public Periodicnost(string periodIzd, int ucestalost)
        {
            PeriodIzd = periodIzd ?? throw new ArgumentNullException(nameof(periodIzd));
            Ucestalost = ucestalost;
        }
    }
}
