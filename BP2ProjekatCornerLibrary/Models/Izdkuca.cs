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
    public class IzdKuca : _DbClass
    {
        [Key]
        public int IDIK { get; set; }
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
        public DateTime? DatZat { get; set; }
        public override ClassPropertyValue GetKeyIdentity() => new ClassPropertyValue("IDIK", IDIK);
        public string Adresa { get => Ulica + " " + Broj; }
        public string  DispMesto { get => DBHelper.GetMesto(PosBr).NazivMesta; }
        public string  DispDrzava { get => DBHelper.GetDrzava(OZND).NazivDrzave; }

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDIK", IDIK)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDIK", "Naziv", "Ulica", "Broj", "PosBr", "OZND", "DatZat" };
        }

        public IzdKuca() : base() { }
        public IzdKuca(string naziv)
        {
            Naziv = naziv;
        }
        public IzdKuca(string naziv, string ulica, string broj, int posBr, string oznd, DateTime? datZat = null)
        {
            Naziv = naziv;
            Ulica = ulica;
            Broj = broj;
            PosBr = posBr;
            OZND = oznd;
            DatZat = datZat;
        }
        public IzdKuca(int id, string naziv, string ulica, string broj, int posBr, string oznd, DateTime? datZat = null)
        {
            IDIK = id;
            Naziv = naziv;
            Ulica = ulica;
            Broj = broj;
            PosBr = posBr;
            OZND = oznd;
            DatZat = datZat;
        }
        public IzdKuca(int id, string naziv)
        {
            IDIK = id;
            Naziv = naziv;
        }
    }
}
