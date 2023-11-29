using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("IDKnjiga", "OZNZ")]
    public class PripadaZanru : _DbClass
    {
        public int IDKnjiga
        {
            get; set;
        }
        public string OZNZ
        {
            get; set;
        }

        public PripadaZanru() : base() { }
        public PripadaZanru(int IDKnjiga, string OZNZ)
        {
            this.IDKnjiga = IDKnjiga;
            this.OZNZ = OZNZ;
        }
    }
}
