using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models.NonContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Bibliotekar : Radnik
    {
        [Key]
        public int IDRadnik { get; set; }
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        [Required]
        public DateTime DatRodj { get; set; }
        [Required]
        public DateTime DatZap { get; set; }
        public DateTime DatOtp { get; set; }
        [Required]
        public int IDAdmin { get; set; }
        [Required]
        public DateTime DatVr { get; set; }
        [Required]
        public string Ulica { get; set; }
        [Required]
        public string Broj { get; set; }
        [Required]
        public int PosBr { get; set; }
        [Required]
        public string OZND { get; set; }

        public Bibliotekar(params object[] args)
        {
            IDRadnik = (int)args[0];
            Ime = (string)args[1];
            Prezime = (string)args[2];
            DatRodj = (DateTime)args[3];
            DatZap = (DateTime)args[4];
            if (DBHelper.CheckDbNull(args[5]))
                DatOtp = (DateTime)args[5];
            if (DBHelper.CheckDbNull(args[6]))
                Ulica = (string)args[6];
            if (DBHelper.CheckDbNull(args[7]))
                Broj = (string)args[7];
            if (DBHelper.CheckDbNull(args[8]))
                PosBr = (int)args[8];
            if (DBHelper.CheckDbNull(args[9]))
                OZND = (string)args[9];

        }
    }
}
