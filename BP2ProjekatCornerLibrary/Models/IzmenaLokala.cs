using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("IDLokal", "IDAdmin", "DatVrIL")]
    public class IzmenaLokala : _DbClass
    {
        public int IDLokal { get; set; }
        public int IDAdmin { get; set; }
        public DateTime DatVrIL { get; set; }
        public string Naziv { get; set; }
        public DateTime DatOtv { get; set; }
        public DateTime DatZat { get; set; }

        public IzmenaLokala() : base() { }
    }
}
