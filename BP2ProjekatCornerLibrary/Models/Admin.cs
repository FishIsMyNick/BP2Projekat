using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models.NonContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Admin : Zaposleni
    {
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public int PosBr { get; set; }
        public string OZND { get; set; }

        public string GetTip => "Administrator";
        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDRadnik", "Ime", "Prezime", "DatRodj", "DatZap", "DatOtp", "Ulica", "Broj", "PosBr", "OZND" };
        }
        public Admin() : base() { }

        public Admin(int iDRadnik, string ime, string prezime, DateTime datRodj, DateTime datZap, DateTime? datOtp, string ulica, string broj , int posBr, string oznd) : base(iDRadnik, ime, prezime, datRodj, datZap, datOtp)
        {
            Ulica = ulica;
            Broj = broj;
            PosBr = posBr;
            OZND = oznd;
        }
    }
}
