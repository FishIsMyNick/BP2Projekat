﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Helpers
{
    public static class DateConverter
    {
        public static string format = "d.m.yyyy.";
        public static CultureInfo provider = CultureInfo.InvariantCulture;
        public static DateTime ToDateTime(string date)
        {
            return DateTime.ParseExact(date.Normalize(), format, provider);
        }
        public static DateTime? TryToDateTime(string date)
        {
            DateTime? ret = null;
            try
            {
                ret = DateTime.ParseExact(date.Normalize(), format, provider);
            }
            catch (Exception e) { }
            return ret;
        }
        public static DateTime ToDateTime(int day, int month, int year)
        {
            string dt = $"{day}.{month}.{year}.";
            return DateTime.ParseExact(dt.Normalize(), format, provider);
        }
        public static DateTime ToDateTime(string day, string month, string year)
        {
            string dt = $"{day}.{month}.{year}.";
            return DateTime.ParseExact(dt.Normalize(), format, provider);
        }
        public static string ToString(DateTime date)
        {
            return $"{date.Day}.{date.Month}.{date.Year}.";
        }
    }
}
