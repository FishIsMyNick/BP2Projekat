using BP2ProjekatCornerLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models.NonContext
{
    public class ViewAutor : Autor, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string DispDrzava => Drzava != null ? DBHelper.GetDrzava(Drzava).NazivDrzave : "Nepoznato";
        public string DispDatRodj => DatRodj != null ? ((DateTime)DatRodj).Day + "." + ((DateTime)DatRodj).Month + "." + ((DateTime)DatRodj).Year + "." : "Nepoznato";
        public string GetPrezime => Prezime != null ? Prezime : "";

        public ViewAutor() : base() { }

        public ViewAutor(int id, string ime, string? prezime, DateTime? datRodj, string? biografija, string? drzava) : base(id, ime, prezime, datRodj, biografija, drzava)
        {
        }
        public ViewAutor(Autor a) : base(a.IDAutor, a.Ime, a.Prezime, a.DatRodj, a.Biografija, a.Drzava)
        {

        }
    }
}
