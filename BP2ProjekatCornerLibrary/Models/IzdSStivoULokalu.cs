using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("IDSStivo", "BrIzd", "IDBK")]
    public class IzdSStivoULokalu
    {
        public int IDSStivo { get; set; }
        public int BrIzd { get; set; }
        public int IDBK { get; set; }
        [Required]
        public DateTime DatVrIzmene { get; set; }
        [Required]
        public int Kolicina { get; set; }
    }
}
