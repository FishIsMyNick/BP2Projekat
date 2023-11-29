using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("IDKnjiga", "OZNJ")]
    public class KnjigaNaJeziku
    {
        public int IDKnjiga { get; set; }
        public string OZNJ { get; set; }

        public KnjigaNaJeziku(int IDKnjiga, string OZNJ)
        {
            this.IDKnjiga = IDKnjiga;
            this.OZNJ = OZNJ;
        }
    }
}
