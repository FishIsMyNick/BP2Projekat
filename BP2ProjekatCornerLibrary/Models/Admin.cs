using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models.NonContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Admin : Radnik
    {
        [Key]
        public int IDRadnik { get; set; }
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        [Required]
        public DateTime DatRodj { get; set; }
        [Required]
        public DateTime DatZap { get; set; }
        public DateTime DatOtp { get; set; }
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public int PosBr { get; set; }
        public string OZND { get; set; }

        public Admin() : base() { }
        
    }
}
