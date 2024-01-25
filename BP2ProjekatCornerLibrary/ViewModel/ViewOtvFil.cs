using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.ViewModel
{
    public class ViewOtvFil : Biblikutak
    {
        public string Adresa { get { return Ulica + " " + Broj; } }
        public string DatOtvStr { get { return DateConverter.ToString(base.DatOtv); } }
        public string GetMesto { get => DBHelper.GetMesto(base.PosBr).NazivMesta; }
        public string GetDrzava { get => DBHelper.GetDrzava(base.OZND).NazivDrzave; }

        public ViewOtvFil() : base()
        {

        }

        public ViewOtvFil(int iDBK, string naziv, DateTime datOtv, DateTime? datZat, string ulica, string broj, int posBr, string oZND) : base(iDBK, naziv, datOtv, datZat, ulica, broj, posBr, oZND)
        {
        }
        public ViewOtvFil(Biblikutak b)
        {
            IDBK = b.IDBK;
            Naziv = b.Naziv;
            DatOtv = b.DatOtv;
            DatZat = b.DatZat;
            Ulica = b.Ulica;
            Broj = b.Broj;
            PosBr = b.PosBr;
            OZND = b.OZND;
        }
    }
}
