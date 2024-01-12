using BP2ProjekatCornerLibrary.Helpers;
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
    [PrimaryKey("IDSStivo", "BrIzd")]
    public class IzdanjeSStiva : _DbClass
    {
        public int IDSStivo { get; set; }
        public int BrIzd { get; set; }
        [Required]
        public DateTime DatIzd { get; set; }
        [Required]
        public decimal Cena { get; set; }
        public DateTime? DatBrisanja { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDSStivo", IDSStivo),
                new ClassPropertyValue("BrIzd", BrIzd)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDSStivo", "BrIzd", "DatIzd", "Cena", "DatBrisanja"};
        }

        public IzdanjeSStiva() : base() { }
        public IzdanjeSStiva(int iDSStivo, int brIzd, DateTime datIzd, decimal cena) : base()
        {
            IDSStivo = iDSStivo;
            BrIzd = brIzd;
            DatIzd = datIzd;
            Cena = cena;
        }
    }
}
