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
    [PrimaryKey("IDKnjiga", "IDBib", "DatVr")]
    public class IzmenaKnjige : _DbClass
    {
        public int IDKnjiga { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public string Naziv { get; set; }
        public int BrIzd { get; set; }
        public int GodIzd { get; set; }
        public string VrIzd { get; set; }
        public int BrStrana { get; set; }
        public int VelicinaFonta { get; set; }
        public int Korice { get; set; }
        public int Ograniceno { get; set; }
        public string Format { get; set; }
        public IzmenaKnjige() : base() { }
    }
}
