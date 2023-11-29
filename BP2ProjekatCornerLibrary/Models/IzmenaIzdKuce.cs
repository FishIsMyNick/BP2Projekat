using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("IDIK", "IDBib", "DatVr")]
    public class IzmenaIzdKuce : _DbClass
    {
        public int IDIK { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public string Naziv { get; set; }
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public int PosBr { get; set; }
        public string OZND { get; set; }

        public IzmenaIzdKuce() : base() { }
    }
}
