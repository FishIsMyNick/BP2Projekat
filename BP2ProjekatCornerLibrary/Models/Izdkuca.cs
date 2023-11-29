using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP2ProjekatCornerLibrary.Models.NonContext;

namespace BP2ProjekatCornerLibrary.Models
{
    public class IzdKuca : _DbClass
    {
        [Key]
        public int IDBK { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public string Ulica { get; set; }
        [Required]
        public string Broj { get; set; }
        [Required]
        public int PosBr { get; set; }
        [Required]
        public string OZND { get; set; }

        public IzdKuca() : base() { }
        public IzdKuca(string naziv)
        {
            Naziv = naziv;
        }
        public IzdKuca(string naziv, string ulica, string broj, int posBr, string oznd)
        {
            Naziv = naziv;
            Ulica = ulica;
            Broj = broj;
            PosBr = posBr;
            OZND = oznd;
        }
    }
}
