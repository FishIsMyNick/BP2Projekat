using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("IDSStivo", "IDBib", "DatVr", "IDIK")]
    public class IzSStivaIzdKuca : _DbClass
    {
        public int IDSStivo { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public int IDIK { get; set; }

        public IzSStivaIzdKuca() : base() { }
    }
}
