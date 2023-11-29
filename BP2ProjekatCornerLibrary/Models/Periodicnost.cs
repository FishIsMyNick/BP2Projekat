using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP2ProjekatCornerLibrary.Models.NonContext;
namespace BP2ProjekatCornerLibrary.Models
{
    public class Periodicnost : _DbClass
    {
        [Key]
        public string PeriodIzd { get; set; }
        [Required]
        public int Ucestalost { get; set; }

        public Periodicnost() : base() { }
    }
}
