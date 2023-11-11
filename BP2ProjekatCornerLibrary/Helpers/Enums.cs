﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Helpers
{
    public static class EnumsHelper
    {

        public static  string GetTipRadnika(TipRadnika tipRadnika)
        {
            switch (tipRadnika)
            {
                case TipRadnika.Admin: return "Administrator";
                case TipRadnika.Bibliotekar: return "Bibliotekar";
                case TipRadnika.Kurir: return "Kurir";
                default: return null;
            }
        }
    }

    public enum TipRadnika
    {
        Admin,
        Bibliotekar,
        Kurir
    }
}
