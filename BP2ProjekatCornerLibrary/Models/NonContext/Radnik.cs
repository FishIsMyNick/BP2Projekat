using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models.NonContext
{
    public class Radnik : Zaposleni
    {
        [Required]
        public int IDAdmin { get; set; }
        [Required]
        public DateTime DatVr { get; set; }
        [Required]
        public string Ulica { get; set; }
        [Required]
        public string Broj { get; set; }
        [Required]
        public int PosBr { get; set; }
        [Required]
        public string OZND { get; set; }
    }
}
