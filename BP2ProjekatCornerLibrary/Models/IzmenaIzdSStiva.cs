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
    [PrimaryKey("IDSStivo", "BrIzd", "IDBib", "DatVr")]
    public class IzmenaIzdSStiva : _DbClass
    {
        public int IDSStivo { get; set; }
        public int BrIzd { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public DataType DatIzd { get; set; }
        public decimal Cena { get; set; }

        public IzmenaIzdSStiva() : base() { }
    }
}
