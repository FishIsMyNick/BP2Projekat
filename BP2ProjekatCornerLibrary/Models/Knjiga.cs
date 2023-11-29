using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP2ProjekatCornerLibrary.Models.NonContext;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Knjiga : _DbClass
    {
        [Key]
        public int IDKnjiga { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public int BrIzd { get; set; }
        public int GodIzd { get; set; }
        public string  VrIzd { get; set; }
        public int BrStrana { get; set; }
        public int VelicinaFonta { get; set; }
        public int Korice { get; set; }
        public int Ograniceno { get; set; }
        [Required]
        public string Format { get; set; }

        public Knjiga() : base() { }

        public Knjiga(string naziv, int brIzd, int godIzd, string vrIzd, int brStrana, int velicinaFonta, int korice, int ograniceno, string format)
        {
            Naziv = naziv;
            BrIzd = brIzd;
            GodIzd = godIzd;
            VrIzd = vrIzd;
            BrStrana = brStrana;
            VelicinaFonta = velicinaFonta;
            Korice = korice;
            Ograniceno = ograniceno;
            Format = format;
        }
    }
}
