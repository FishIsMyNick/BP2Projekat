using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP2ProjekatCornerLibrary.Models.NonContext;

namespace BP2ProjekatCornerLibrary.Models
{
    public class SerijskoStivo : _DbClass
    {
        [Key] 
        public int IDSStivo { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public int TipStiva { get; set; }
        [Required]
        public string Format { get; set; }
        [Required]
        public string Period { get; set; }

        public SerijskoStivo() : base() { }
    }
}
