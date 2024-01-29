using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Helpers.Classes;
using BP2ProjekatCornerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.ViewModel
{
    public class ViewRadnik
    {
        public int IDRadnik { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DatRodj { get; set; }
        public string DatRodjStr { get => DateConverter.ToString(DatRodj); }
        public DateTime DatZap { get; set; }
        public string DatZapStr { get => DateConverter.ToString(DatZap); }
        public int Tip { get; set; }
        public string TipStr { get => EnumsHelper.GetTipRadnikaString(Tip); }
        public Lokacija Lok { get; set; }
        public string GetUlica => Lok.Ulica;
        public string GetBroj => Lok.Broj;
        public string GetMesto => DBHelper.GetMesto(Lok.PosBr).NazivMesta;
        public string GetDrzava => DBHelper.GetDrzava(Lok.OZND).NazivDrzave;
        public string GetTip
        {
            get
            {
                switch (Tip)
                {
                    case 1: return "Administrator";
                    case 2: return "Bibliotekar";
                    case 3: return "Kurir";
                    default: return "";
                }
            }
        }

        public ViewRadnik(int id, string ime, string prezime, string username, DateTime datRodj, DateTime datZap, int tip, string ulica, string broj, int posBr, string oznd)
        {
            IDRadnik = id;
            Ime = ime;
            Prezime = prezime;
            Username = username;
            DatRodj = datRodj;
            DatZap = datZap;
            Tip = tip;
            Lok = new Lokacija(ulica, broj, posBr, oznd);
        }
    }
}
