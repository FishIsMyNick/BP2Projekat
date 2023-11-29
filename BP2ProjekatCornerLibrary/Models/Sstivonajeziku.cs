using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("IDSStivo", "OZNJ")]
    public class SStivoNaJeziku
    {
        public int IDSStivo { get; set; }
        public string OZNJ { get; set; }
    }
}
