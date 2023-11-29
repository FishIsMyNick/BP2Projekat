using BP2ProjekatCornerLibrary.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("ID", "KorisnickoIme")]
    public class AdminKoristiNalog
    {
        public int ID { get; set; }
        public string KorisnickoIme { get; set; }

        public AdminKoristiNalog() { }
        public AdminKoristiNalog(int ID, string KorisnickoIme)
        {
            this.ID = ID;
            this.KorisnickoIme = KorisnickoIme;
        }
        public AdminKoristiNalog(params object[] args)
        {
            ID = (int)args[0];
            if (DBHelper.CheckDbNull(args[1]))
                KorisnickoIme = (string)args[1];
        }
    }
}
