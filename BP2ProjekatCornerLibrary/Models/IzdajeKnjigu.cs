using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("IDKnjiga", "IDIK")]
    public  class IzdajeKnjigu
    {
        public int IDKnjiga { get; set; }
        public int IDIK { get; set; }

        public IzdajeKnjigu(int IDKnjiga, int IDIK)
        {
            this.IDKnjiga = IDKnjiga;
            this.IDIK = IDIK;
        }
    }
}
