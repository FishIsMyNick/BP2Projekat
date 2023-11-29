using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("IDSStivo", "IDBib", "DatVr")]
    public class IzmenaSStiva : _DbClass
    {
        public int IDSStivo { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public string Naziv { get; set; }
        public int TipStiva { get; set; }
        public string Format { get; set; }
        public string Period { get; set; }

        public IzmenaSStiva() : base() { }
    }
}
