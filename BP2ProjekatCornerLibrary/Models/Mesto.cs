using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP2ProjekatCornerLibrary.Models.NonContext;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Mesto : _DbClass
    {
        [Key]
        public int PosBr { get; set; }
        [Required]
        public string NazivMesta  { get; set; }

        public Mesto() : base() { }
    }
}
