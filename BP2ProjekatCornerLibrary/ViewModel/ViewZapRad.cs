using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models.NonContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.ViewModel
{
    public class ViewZapRad : Radnik
    {
        public int IDRadnik { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Tip { get; set; }
        public DateTime DatZap { get; set; }
        public string DatZapStr { get => DateConverter.ToString(DatZap); }

        public ViewZapRad() : base()
        {

        }
        public ViewZapRad(int iDRadnik, string ime, string prezime, DateTime datZap, string korisnickoIme, string tip) : base()
        {
            IDRadnik = iDRadnik;
            Ime = ime;
            Tip = tip;
            Prezime = prezime;
            DatZap = datZap;
            KorisnickoIme = korisnickoIme;
        }
    }
}
