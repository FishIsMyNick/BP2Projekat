using BP2ProjekatCornerLibrary.Helpers.Classes;
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
        public DateTime? DatZat { get; set; }
        [Required]
        public string Ulica { get; set; }
        [Required]
        public string Broj { get; set; }
        [Required]
        public int PosBr { get; set; }
        [Required]
        public string OZND { get; set; }
        public override ClassPropertyValue GetKeyIdentity() => new ClassPropertyValue("IDBK", IDBK);

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDBK", IDBK)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDBK", "Naziv", "DatOtv", "DatZat", "Ulica", "Broj", "PosBr", "OZND" };
        }

        public Biblikutak() : base() { }

        public Biblikutak(int iDBK, string naziv, DateTime datOtv, DateTime? datZat, string ulica, string broj, int posBr, string oZND)
        {
            IDBK = iDBK;
            Naziv = naziv;
            DatOtv = datOtv;
            DatZat = datZat;
            Ulica = ulica;
            Broj = broj;
            PosBr = posBr;
            OZND = oZND;
        }
    }
}
