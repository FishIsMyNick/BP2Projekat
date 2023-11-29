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
    [PrimaryKey("IDAutor", "IDBib", "DatVr")]
    public class IzmenaAutora : _DbClass
    {
        public int IDAutor { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatRodj { get; set; }
        public string Biografija { get; set; }
        public string Drzava { get; set; }

        public IzmenaAutora() : base() { }
    }
}
