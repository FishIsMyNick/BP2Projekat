using BP2ProjekatCornerLibrary.Models.NonContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("IDKnjiga", "IDAutor")]
    public class Pise : _DbClass
    {
        public int IDKnjiga { get; set; }
        public int IDAutor { get; set; }


        public Pise() : base() { }
        public Pise(int IDKnjiga, int IDAutor)
        {
            this.IDKnjiga = IDKnjiga;
            this.IDAutor = IDAutor;
        }
    }
}
