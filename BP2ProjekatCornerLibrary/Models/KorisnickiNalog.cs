using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    public class KorisnickiNalog
    {
        [Key]
        public string KorisnickoIme { get; set; }
        [Required]
        public string Sifra { get; set; }
        [Required]
        public DateTime DatKreiranja { get; set; }
        public DateTime DatZatvaranja { get; set; }
        [Required]
        public int TipNaloga { get; set; }

        public KorisnickiNalog() { }
        public KorisnickiNalog(params object[] args)
        {
            KorisnickoIme = (string)args[0];
            Sifra = (string)args[1];
            DatKreiranja = (DateTime)args[2];
            if (args[3].GetType() != typeof(DBNull))
                DatZatvaranja = (DateTime)args[3];
            TipNaloga = (int)args[4];
        }
    }
}
