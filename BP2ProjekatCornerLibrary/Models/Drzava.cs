using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP2ProjekatCornerLibrary.Models.NonContext;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Drzava : _DbClass
    {
        [Key]
        public string OZND { get; set; }
        [Required]
        public string NazivDrzave { get; set; }
        public Drzava() : base() { }
    }
}
