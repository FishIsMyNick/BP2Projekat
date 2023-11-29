using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("IDSStivo", "BrIzd")]
    public class IzdanjeSStiva : _DbClass
    {
        public int IDSStivo { get; set; }
        public int BrIzd { get; set; }
        [Required]
        public DataType DatIzd { get; set; }
        [Required]
        public decimal Cena { get; set; }

        public IzdanjeSStiva() : base() { }
}
}
