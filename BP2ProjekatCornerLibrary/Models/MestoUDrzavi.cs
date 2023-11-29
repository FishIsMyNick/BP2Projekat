using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("PosBr", "OZND")]
    public class MestoUDrzavi
    {
        public int PosBr { get; set; }
        public string OZND { get; set; }
    }
}
