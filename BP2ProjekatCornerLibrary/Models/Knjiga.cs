using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP2ProjekatCornerLibrary.Helpers;
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
        public string? Format { get; set; }
        public override ClassPropertyValue GetKeyIdentity() => new ClassPropertyValue("IDKnjiga", IDKnjiga);

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDKnjiga", IDKnjiga)
            };
        }
        public Knjiga() : base() { }

        public Knjiga(string naziv, int brIzd, int godIzd, string vrIzd, int brStrana, int velicinaFonta, int korice, int ograniceno, string? format)
        {
            IDKnjiga = -1;
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
        public Knjiga(int id, string naziv, int brIzd, int godIzd, string vrIzd, int brStrana, int velicinaFonta, int korice, int ograniceno, string? format)
        {
            IDKnjiga = id;
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
