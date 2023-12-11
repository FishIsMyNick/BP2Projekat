using BP2ProjekatCornerLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models.NonContext
{
    public class Zaposleni : _DbClass
    {
        [Key]
        public int IDRadnik { get; set; }
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        [Required]
        public DateTime DatRodj { get; set; }
        [Required]
        public DateTime DatZap { get; set; }
        public DateTime? DatOtp { get; set; }
        public override ClassPropertyValue GetKeyIdentity() => new ClassPropertyValue("IDRadnik", IDRadnik);
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDRadnik", IDRadnik)
            };
        }
        public Zaposleni()
        {
            
        }

        public Zaposleni(int iDRadnik, string ime, string prezime, DateTime datRodj, DateTime datZap, DateTime? datOtp)
        {
            IDRadnik = iDRadnik;
            Ime = ime;
            Prezime = prezime;
            DatRodj = datRodj;
            DatZap = datZap;
            DatOtp = datOtp ?? datOtp;
        }
    }
}
