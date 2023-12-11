using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models.NonContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    public class KorisnickiNalog : _DbClass
    {
        [Key]
        public string KorisnickoIme { get; set; }
        [Required]
        public string Sifra { get; set; }
        [Required]
        public DateTime DatKreiranja { get; set; }
        public DateTime? DatZatvaranja { get; set; }
        [Required]
        public int TipNaloga { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("KorisnickoIme", KorisnickoIme)
            };
        }

        public KorisnickiNalog() : base() { }

        public KorisnickiNalog(string korisnickoIme, string sifra, DateTime datKreiranja, int tipNaloga)
        {
            KorisnickoIme = korisnickoIme ?? throw new ArgumentNullException(nameof(korisnickoIme));
            Sifra = sifra ?? throw new ArgumentNullException(nameof(sifra));
            DatKreiranja = datKreiranja;
            DatZatvaranja = null;
            TipNaloga = tipNaloga;
        }


        //public KorisnickiNalog(params ClassPropertyValue[] args) : base(args)
        //{
        //}
    }
}
