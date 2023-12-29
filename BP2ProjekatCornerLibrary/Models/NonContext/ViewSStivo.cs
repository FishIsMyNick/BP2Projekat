using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models.NonContext
{
    public class ViewSStivo : SerijskoStivo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private List<Jezik>? Jezici;
        private List<IzdKuca>? IzdKuce;

        public string  ListJezici {
            get
            {
                if (Jezici == null) return "";
                string ret = "";
                for (int i = 0; i < Jezici.Count; i++)
                {
                    ret += Jezici[i].NazivJezika;
                    if (i < Jezici.Count - 1) ret += ", ";
                }
                return ret;
            }
        }
        public string ListIzdKuce
        {
            get
            {
                if (IzdKuce == null) return "";
                string ret = "";
                for (int i = 0; i < IzdKuce.Count; i++)
                {
                    ret += IzdKuce[i].Naziv;
                    if (i < IzdKuce.Count - 1) ret += ", ";
                }
                return ret;
            }
        }

        public ViewSStivo(int id, string naziv, int tip, string? format, string? period, List<Jezik>? jezici, List<IzdKuca>? izdKuce) : base(id, naziv, tip, format, period)
        {
            Jezici = jezici;
            IzdKuce = izdKuce;
        }
    }

    public class ViewIzdSStivo : IzdanjeSStiva, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public SerijskoStivo SStivo;
        public IzdanjeSStiva IzdSStiva;
        public string DispBrIzd => IzdSStiva.BrIzd.ToString();
        public string DispDatIzd => IzdSStiva.DatIzd.Day + "/" + IzdSStiva.DatIzd.Month + "/" + IzdSStiva.DatIzd.Year;
        public int Tip => SStivo.TipStiva;
        public string? DispFormat => SStivo.Format;
        public string? DispPeriod => SStivo.Period;
        public string DispCena => IzdSStiva.Cena.ToString();
        private List<Jezik>? Jezici;
        private List<IzdKuca>? IzdKuce;

        public string ListJezici
        {
            get
            {
                if (Jezici == null) return "";
                string ret = "";
                for (int i = 0; i < Jezici.Count; i++)
                {
                    ret += Jezici[i].NazivJezika;
                    if (i < Jezici.Count - 1) ret += ", ";
                }
                return ret;
            }
        }
        public string ListIzdKuce
        {
            get
            {
                if (IzdKuce == null) return "";
                string ret = "";
                for (int i = 0; i < IzdKuce.Count; i++)
                {
                    ret += IzdKuce[i].Naziv;
                    if (i < IzdKuce.Count - 1) ret += ", ";
                }
                return ret;
            }
        }

        public ViewIzdSStivo(int idss, int brIzd, DateTime datIzd, decimal cena, string naziv, int tip, string? format, string? period, List<Jezik>? jezici, List<IzdKuca>? izdKuce) : base(idss, brIzd, datIzd, cena)
        {
            SStivo = new SerijskoStivo(idss, naziv, tip, format, period);
            IzdSStiva = new IzdanjeSStiva(idss, brIzd, datIzd, cena);
            Jezici = jezici;
            IzdKuce = izdKuce;
        }
    }
}
