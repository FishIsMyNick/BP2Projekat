using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Drzava
    {
        [Key]
        public string OZND { get; set; }
        [Required]
        public string NazivDrzave { get; set; }
    }
}
