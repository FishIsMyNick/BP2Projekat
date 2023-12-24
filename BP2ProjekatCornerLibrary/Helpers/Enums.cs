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
        public static iTipRadnika GetTipRadnika(object obj)
        {
            Type tip = obj.GetType();
            if (tip == typeof(Admin)) return iTipRadnika.Admin;
            if (tip == typeof(Bibliotekar)) return iTipRadnika.Bibliotekar;
            if (tip == typeof(Kurir)) return iTipRadnika.Kurir;
            return iTipRadnika.Kurir;
        }
        public static iTipRadnika GetTipRadnika(string tipRadnika)
        {
            switch (tipRadnika)
            {
                case "Administrator": return iTipRadnika.Admin;
                case "Bibliotekar": return iTipRadnika.Bibliotekar;
                case "Kurir": return iTipRadnika.Kurir;
                default: return iTipRadnika.Kurir;
            }
        }
        public static iTipRadnika GetTipRadnika(int tipRadnika)
        {
            switch (tipRadnika)
            {
                case 1: return iTipRadnika.Admin;
                case 2: return iTipRadnika.Bibliotekar;
                case 3: return iTipRadnika.Kurir;
                default: return iTipRadnika.Kurir;
            }
        }
        public static string GetTipRadnikaString(iTipRadnika tipRadnika)
        {
            switch (tipRadnika)
            {
                case iTipRadnika.Admin: return "Administrator";
                case iTipRadnika.Bibliotekar: return "Bibliotekar";
                case iTipRadnika.Kurir: return "Kurir";
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

        public static string GetKorice(int? korice)
        {
            switch (korice)
            {
                case 0: return "Meke";
                case 1: return "Tvrde";
            }
            return "Meke";
        }
        public static string GetKorice(iKorice korice)
        {
            return korice.ToString();
        }
    }

    public enum iTipRadnika
    {
        Admin = 1,
        Bibliotekar,
        Kurir
    }

    public enum iKorice
    {
        Meke = 0,
        Tvrde
    }
}
