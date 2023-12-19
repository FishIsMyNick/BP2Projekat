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
        public int? GodIzd { get; set; }
        public string?  VrIzd { get; set; }
        public int? BrStrana { get; set; }
        public int? VelicinaFonta { get; set; }
        public int? Korice { get; set; }
        public int? Ograniceno { get; set; }
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

        public Knjiga(string naziv, int brIzd, int? godIzd, string? vrIzd, int? brStrana, int? velicinaFonta, int? korice, int? ograniceno, string? format)
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
        public Knjiga(int id, string naziv, int brIzd, int? godIzd, string? vrIzd, int? brStrana, int? velicinaFonta, int? korice, int? ograniceno, string? format)
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
        public Knjiga(string naziv, string brIzd, string? vrIzd, string? brStrana, string? velicinaFonta, string? korice, bool? ograniceno, string? format)
        {
            Naziv = naziv;
            BrIzd = int.Parse(brIzd);
            if(vrIzd == "")
            { GodIzd = null; VrIzd = null; }
            else
            {
                int god = 0;
                if(int.TryParse(vrIzd, out god))
                {
                    GodIzd = god;
                    VrIzd = null;
                }
                else
                {
                    GodIzd = null;
                    VrIzd = vrIzd;
                }
            }
            BrStrana = brStrana == "" ? null : int.Parse(brStrana);
            VelicinaFonta = velicinaFonta == "" ? null : int.Parse(velicinaFonta); 
            Korice = korice == "Meke" ? 0 : 1;
            Ograniceno = ograniceno == true ? 1 : 0;
            Format = format == "" ? null : format;
        }
    }
}
