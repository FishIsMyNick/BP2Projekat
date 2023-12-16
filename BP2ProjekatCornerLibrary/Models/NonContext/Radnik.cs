using BP2ProjekatCornerLibrary.Helpers;
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
        public Radnik() : base() { }
        
        public Radnik(int iDRadnik, string ime, string prezime, DateTime datRodj, DateTime datZap, DateTime? datOtp, int iDAdmin, DateTime datVr, string ulica, string broj, int posBr, string oznd) : base(iDRadnik, ime, prezime, datRodj, datZap, datOtp)
        {
            IDAdmin = iDAdmin;
            DatVr = datVr;
            Ulica = ulica;
            Broj = broj;
            PosBr = posBr;
            OZND = oznd;
        }

    }
}
