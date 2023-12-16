using BP2ProjekatCornerLibrary.Models.NonContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Kurir : Radnik
    {
        public Kurir() : base() { }
        public Kurir(int iDRadnik, string ime, string prezime, DateTime datRodj, DateTime datZap, DateTime? datOtp, int iDAdmin, DateTime datVr, string ulica, string broj, int posBr, string oznd) : base(iDRadnik, ime, prezime, datRodj, datZap, datOtp, iDAdmin, datVr, ulica, broj, posBr, oznd) { }
    }
}
