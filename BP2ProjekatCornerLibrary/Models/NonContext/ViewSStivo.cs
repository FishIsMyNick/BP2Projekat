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
}
