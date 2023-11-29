using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models.NonContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Biblikutak : _DbClass
    {
        [Key]
        public int IDBK { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public DateTime DatOtv { get; set; }
        public DateTime DatZat { get; set; }
        [Required]
        public string Ulica { get; set; }
        [Required]
        public string Broj { get; set; }
        [Required]
        public int PosBr { get; set; }
        [Required]
        public string OZND { get; set; }

        public Biblikutak() : base() { }
    }
}
