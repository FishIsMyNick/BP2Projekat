using BP2ProjekatCornerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BP2ProjekatCornerLibrary.Helpers
{
    public static class Validator
    {
        public static bool Number(string value)
        {
            return int.TryParse(value, out _);
        }
        public static bool PozNumber(string value)
        {
            int i = -1;
            if (!int.TryParse(value, out i))
            {
                MessageBox.Show("Unet broj nije u dobrom formatu!");
                return false;
            }
            else if (i > 0)
            {
                return true;
            }
            MessageBox.Show("Unet broj mora biti pozitivan i veći od 0!");
            return false;
        }
        public static bool PozNumberNoCom(string value)
        {
            int i = -1;
            if (!int.TryParse(value, out i))
            {
                return false;
            }
            else if (i > 0)
            {
                return true;
            }
            return false;
        }
        public static bool PozDecimal(string value)
        {
            decimal i = -1;
            if(!decimal.TryParse(value, out i))
            {
                MessageBox.Show("Unet broj nije u dobrom formatu!");
                return false;
            }
            else if (i > 0)
            {
                return true;
            }
            MessageBox.Show("Unet broj mora biti pozitivan i veći od 0!");
            return false;
        }
        public static bool Ucestalost(string value)
        {
            value = value.Trim();
            if (value.Length == 0)
            {
                MessageBox.Show("Polje za učestalost mora biti popunjeno!");
                return false;
            }
            int i = -1;
            if (!int.TryParse(value, out i))
            {
                MessageBox.Show("Polje za učestalost mora biti pozitivan ceo broj!");
                return false;
            }
            if (i <= 0)
            {
                MessageBox.Show("Polje za učestalost mora biti pozitivan ceo broj!");
                return false;
            }
            return true;
        }
        public static bool Naziv(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Polje naziva mora biti popunjeno!");
                return false;
            }
            if (input.Any(char.IsDigit))
            {
                MessageBox.Show("Naziv mora sadržati samo slova i razmake!");
                return false;
            }
            if (input.Contains(' '))
            {
                string[] strings = input.Split(' ');
                string tocheck = "";
                foreach (string s in strings)
                {
                    if (s != "")
                        tocheck += s;
                }
                if (!tocheck.All(char.IsLetter))
                {
                    MessageBox.Show("Naziv mora sadržati samo slova i razmake!");
                    return false;
                }
            }
            else
            {
                if (!input.All(char.IsLetter))
                {
                    MessageBox.Show("Naziv mora sadržati samo slova i razmake!");
                    return false;
                }
            }
            return true;
        }
        public static bool Oznaka(string input)
        {
            if (input == null) return false;
            if (input.Length == 0)
            {
                MessageBox.Show("Polje oznake mora biti popunjeno!");
                return false;
            }
            if (input.Length > 4)
            {
                MessageBox.Show("Oznaka može imati najviše 4 karaktera!");
                return false;
            }
            if (!input.All(char.IsLetter))
            {
                MessageBox.Show("Oznaka mora sadržati samo velika slova!");
                return false;
            }
            if (!(input.ToUpper() == input))
            {
                MessageBox.Show("Oznaka mora sadržati samo velika slova!");
                return false;
            }
            return true;
        }
        public static bool Name(string input)
        {
            if (input.Length <= 0)
            {
                MessageBox.Show("Polje za ime mora biti popunjeno!");
                return false;
            }
            if (input.Contains(' '))
            {
                string[] s = input.Split(' ');
                if (!string.Concat(s).All(char.IsLetter))
                {
                    MessageBox.Show("Ime ne sme sadržati specijalne karaktere ni cifre!");
                    return false;
                }
            }
            else if (!input.All(char.IsLetter))
            {
                MessageBox.Show("Ime ne sme sadržati specijalne karaktere ni cifre!");
                return false;
            }
            return true;
        }
        public static bool LastName(string input)
        {
            if (input.Length <= 0)
            {
                MessageBox.Show("Polje za prezime mora biti popunjeno!");
                return false;
            }
            if (input.Contains(' '))
            {
                string[] s = input.Split(' ');
                if (!string.Concat(s).All(char.IsLetter))
                {
                    MessageBox.Show("Prezime ne sme sadržati specijalne karaktere ni cifre!");
                    return false;
                }
            }
            else if (!input.All(char.IsLetter))
            {
                MessageBox.Show("Prezime ne sme sadržati specijalne karaktere ni cifre!");
                return false;
            }
            return true;
        }
        public static bool Day(string input)
        {
            int d = -1;
            if (!int.TryParse(input, out d))
            {
                MessageBox.Show("Dan je unet u pogrešnom formatu!");
                return false;
            }
            if (d < 1 || d > 31)
            {
                MessageBox.Show("Dan je unet u pogrešnom formatu!");
                return false;
            }
            return true;
        }
        public static bool Month(string input)
        {
            int d = -1;
            if (!int.TryParse(input, out d))
            {
                MessageBox.Show("Mesec je unet u pogrešnom formatu!");
                return false;
            }
            if (d < 1 || d > 12)
            {
                MessageBox.Show("Mesec je unet u pogrešnom formatu!");
                return false;
            }
            return true;
        }
        public static bool Year(string input)
        {
            int d = -1;
            if (!int.TryParse(input, out d))
            {
                MessageBox.Show("Godina je uneta u pogrešnom formatu!");
                return false;
            }
            if (d < 1 || d > DateTime.Now.Year)
            {
                MessageBox.Show("Godina je uneta u pogrešnom formatu!");
                return false;
            }
            return true;
        }
        public static bool YearInfinite(string input)
        {
            int d = -1;
            if (!int.TryParse(input, out d))
            {
                MessageBox.Show("Godina je uneta u pogrešnom formatu!");
                return false;
            }
            if (d < 1)
            {
                MessageBox.Show("Godina je uneta u pogrešnom formatu!");
                return false;
            }
            return true;
        }
        public static bool Date(string day, string month, string year)
        {
            if(day == "" || month == "" || year == "")
            {
                MessageBox.Show("Polja za datum moraju biti popunjena!");
                return false;
            }
            if (!DateTime.TryParse($"{int.Parse(year)}/{int.Parse(month)}/{int.Parse(day)}", out _))
            {
                MessageBox.Show("Unet datum ne postoji!");
                return false;
            }
            return true;
        }
        public static bool Password(string input)
        {
            if (input.Length <= 0)
            {
                MessageBox.Show("Polje za šifru mora biti popunjeno!");
                return false;
            }
            return true;
        }
        public static bool VrIzd(string input)
        {
            if (input.Length <= 0)
            {
                MessageBox.Show("Polje za vreme izdavanja mora biti popunjeno!");
                return false;
            }
            if (input.Contains(' '))
            {
                string[] s = input.Split(' ');
                if (!string.Concat(s).All(char.IsLetterOrDigit))
                {
                    MessageBox.Show("Vreme izdavanja ne sme sadržati specijalne karaktere!");
                    return false;
                }
            }
            else
            {
                if(input.EndsWith('.')) input = input.Substring(0, input.Length - 1);
                if (!input.All(char.IsLetterOrDigit))
                {
                    MessageBox.Show("Vreme izdavanja ne sme sadržati specijalne karaktere!");
                    return false;
                }
            }
            return true;
        }
        public static bool StreetName(string input)
        {
            if (input.Length <= 0)
            {
                MessageBox.Show("Polje za ulicu mora biti popunjeno!");
                return false;
            }
            if (input.Contains(' '))
            {
                string[] s = input.Split(' ');
                if (!string.Concat(s).All(char.IsLetterOrDigit))
                {
                    MessageBox.Show("Naziv ulice ne sme sadržati specijalne karaktere!");
                    return false;
                }
            }
            else if (!input.All(char.IsLetterOrDigit))
            {
                MessageBox.Show("Naziv ulice ne sme sadržati specijalne karaktere!");
                return false;
            }
            return true;
        }
        public static bool StreetNumber(string input)
        {
            if (input.Length <= 0)
            {
                MessageBox.Show("Polje za broj mora biti popunjeno!");
                return false;
            }
            if (input.Contains(' '))
            {
                MessageBox.Show("Polje za broj ne sme sadržati razmake!");
                return false;
            }
            if (!input.All(char.IsLetterOrDigit))
            {
                MessageBox.Show("Polje za broj ne sme sadržati specijalne karaktere!");
                return false;
            }
            if (!input[0].ToString().All(char.IsNumber))
            {
                MessageBox.Show("Polje za broj mora počinjati sa cifrom!");
                return false;
            }
            int sIndex = input.Length;
            for (int i = 0; i < input.Length; i++)
            {
                if (!input[i].ToString().All(char.IsNumber))
                {
                    sIndex = i;
                    break;
                }
            }
            for (int i = sIndex + 1; i < input.Length; i++)
            {
                if (!input[i].ToString().All(char.IsLetter))
                {
                    MessageBox.Show("Polje za broj je popunjeno u pogrešnom formatu!");
                    return false;
                }
            }
            int broj = 0;
            if (!int.TryParse(input.Substring(0, sIndex), out broj))
            {
                MessageBox.Show("Polje za broj je popunjeno u pogrešnom formatu!");
                return false;
            }
            if (broj <= 0)
            {
                MessageBox.Show("Polje za broj mora imati pozitivan broj!");
                return false;
            }
            return true;
        }
        public static bool City(Mesto m)
        {
            return City(m.PosBr);
        }
        public static bool City(int posBr)
        {
            if (posBr <= 0)
            {
                MessageBox.Show("Mesto mora biti izabrano!");
                return false;
            }
            return true;
        }
        public static bool Country(Drzava d)
        {
            return Country(d.OZND);
        }
        public static bool Country(string oznd)
        {
            if (oznd == "0000" || oznd.Length <= 0)
            {
                MessageBox.Show("Drzava mora biti izabrana!");
                return false;
            }
            return true;
        }
    }
}
