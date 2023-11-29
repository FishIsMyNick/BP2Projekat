using BP2ProjekatCornerLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Autor
    {
        [Key]
        public int IDAutor { get; set; }
        [Required]
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatRodj { get; set; }
        public string Biografija { get; set; }
        public string Drzava { get; set; }

        public Autor() { }

        public Autor(string ime, string prezime, DateTime datRodj, string biografija, string drzava)
        {
            Ime = ime;
            Prezime = prezime;
            DatRodj = datRodj;
            Biografija = biografija;
            Drzava = drzava;
        }
        public Autor(params object[] args)
        {
            IDAutor = (int)args[0];

            if (DBHelper.CheckDbNull(args[1]))
                Ime = (string)args[1];
            if (DBHelper.CheckDbNull(args[2]))
                Prezime= (string)args[2];
            if (DBHelper.CheckDbNull(args[3]))
                DatRodj = (DateTime)args[3];
            if (DBHelper.CheckDbNull(args[4]))
                Biografija= (string)args[4];
            if (DBHelper.CheckDbNull(args[5]))
                Drzava= (string)args[5];
        }
    }
}
