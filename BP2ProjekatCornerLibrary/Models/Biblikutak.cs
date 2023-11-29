using BP2ProjekatCornerLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Biblikutak
    {
        [Key]
        public int IDBK { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public DateTime DatOtv { get; set; }
        public DateTime DatZat { get; set; }
        [Required]
        public string Ulica { get; set; }
        [Required]
        public string Broj { get; set; }
        [Required]
        public int PosBr { get; set; }
        [Required]
        public string OZND { get; set; }

        public Biblikutak(params object[] args)
        {
            IDBK = (int)args[0];
            if (DBHelper.CheckDbNull(args[1]))
                Naziv = (string)args[1];
            if (DBHelper.CheckDbNull(args[2]))
                DatOtv = (DateTime)args[2];
            if (DBHelper.CheckDbNull(args[3]))
                DatZat = (DateTime)args[3];
            if (DBHelper.CheckDbNull(args[4]))
                Ulica = (string)args[4];
            if (DBHelper.CheckDbNull(args[5]))
                Broj = (string)args[5];
            PosBr = (int)args[6];
            if (DBHelper.CheckDbNull(args[7]))
                OZND = (string)args[7];
        }
    }
}
