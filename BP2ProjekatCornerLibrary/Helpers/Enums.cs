using BP2ProjekatCornerLibrary.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Helpers
{
    public static class EnumsHelper
    {
        public static TipRadnika GetTipRadnika(object obj)
        {
            Type tip = obj.GetType();
            if (tip == typeof(Admin)) return TipRadnika.Admin;
            if (tip == typeof(Bibliotekar)) return TipRadnika.Bibliotekar;
            if (tip == typeof(Kurir)) return TipRadnika.Kurir;
            return TipRadnika.Kurir;
        }
        public static TipRadnika GetTipRadnika(string tipRadnika)
        {
            switch (tipRadnika)
            {
                case "Administrator": return TipRadnika.Admin;
                case "Bibliotekar": return TipRadnika.Bibliotekar;
                case "Kurir": return TipRadnika.Kurir;
                default: return TipRadnika.Kurir;
            }
        }
        public static TipRadnika GetTipRadnika(int tipRadnika)
        {
            switch (tipRadnika)
            {
                case 1: return TipRadnika.Admin;
                case 2: return TipRadnika.Bibliotekar;
                case 3: return TipRadnika.Kurir;
                default: return TipRadnika.Kurir;
            }
        }
        public static string GetTipRadnikaString(TipRadnika tipRadnika)
        {
            switch (tipRadnika)
            {
                case TipRadnika.Admin: return "Administrator";
                case TipRadnika.Bibliotekar: return "Bibliotekar";
                case TipRadnika.Kurir: return "Kurir";
                default: return null;
            }
        }
        public static string GetTipRadnikaString(int tipRadnika)
        {
            switch (tipRadnika)
            {
                case 1: return "Administrator";
                case 2: return "Bibliotekar";
                case 3: return "Kurir";
                default: return null;
            }
        }


    }

    public enum TipRadnika
    {
        Admin = 1,
        Bibliotekar,
        Kurir
    }
}
