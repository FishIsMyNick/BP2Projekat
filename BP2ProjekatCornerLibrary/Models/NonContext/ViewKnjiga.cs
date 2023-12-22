using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BP2ProjekatCornerLibrary.Models.NonContext
{
    public class ViewKnjiga : Knjiga, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private List<Autor>? Autori;
        private List<Jezik>? Jezici;
        private List<Zanr>? Zanrovi;
        private List<IzdKuca>? IzdKuce;


        public string ListAutori
        {
            get
            {
                if (Autori == null) return "";
                string ret = "";
                for (int i = 0; i < Autori.Count; i++)
                {
                    ret += Autori[i].GetFullName;
                    if (i < Autori.Count - 1) ret += ", ";
                }
                return ret;
            }
        }
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
        public string ListZanrovi
        {
            get
            {
                if (Zanrovi == null) return "";
                string ret = "";
                for (int i = 0; i < Zanrovi.Count; i++)
                {
                    ret += Zanrovi[i].NazivZanra;
                    if (i < Zanrovi.Count - 1) ret += ", ";
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
        public bool OgranicenoBool { get => Ograniceno != 0; set { try { _ = value; } catch (Exception e) { OgranicenoBool = OgranicenoBool; } } }
        public string DispVrIzd
        {
            get
            {
                if (GodIzd == null)
                {
                    if (VrIzd == null) return "Nepoznato";
                    else return VrIzd;
                }
                else return GodIzd.ToString() + ".";
            }
        }
        public string DispKorice
        {
            get
            {
                if (Korice == 0) return "Meke";
                else if (Korice == 1) return "Tvrde";
                else return "";
            }
        }
        public ViewKnjiga(int id, string naziv, int brIzd, List<Autor>? autori, List<Jezik>? jezici, List<Zanr>? zanrovi, List<IzdKuca>? izdkuce, int? godIzd, string? vrIzd, int? brStrana, int? velicinaFonta, int? korice, int? ograniceno, string? format) : base(id, naziv, brIzd, godIzd, vrIzd, brStrana, velicinaFonta, korice, ograniceno, format)
        {
            Autori = autori;
            Jezici = jezici;
            Zanrovi = zanrovi;
            IzdKuce = izdkuce;
        }
    }
}
