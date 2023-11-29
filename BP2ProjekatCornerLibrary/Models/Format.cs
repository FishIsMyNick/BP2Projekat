using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Format : _DbClass
    {
        [Key]
        public string NazivFormata { get; set; }
        public Format() : base() { }
    }
}
