using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models.NonContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.ViewModel
{
    public class ViewOtpRad : Radnik
    {
        public int IDRadnik { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Tip { get; set; }
        public DateTime DatZap { get; set; }
        public DateTime DatOtp { get; set; }
        public string DatZapStr { get => DateConverter.ToString(DatZap); }
        public string DatOtpStr { get => DateConverter.ToString(DatOtp); }
        public string PeriodRada { get => DatZapStr + "-" + DatOtpStr; }

        public ViewOtpRad() : base()
        {

        }
        public ViewOtpRad(int iDRadnik, string ime, string prezime, DateTime datZap, DateTime datOtp, string korisnickoIme, string tip) : base()
        {
            IDRadnik = iDRadnik;
            Ime = ime;
            Tip = tip;
            Prezime = prezime;
            DatZap = datZap;
            DatOtp = datOtp;
            KorisnickoIme = korisnickoIme;
        }
    }
}
