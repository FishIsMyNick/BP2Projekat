using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("IDKnjiga", "IDBK", "DatVrIzmene")]
    public class KnjigaULokalu
    {
        public int IDKnjiga { get; set; }
        public int IDBK { get; set; }
        public DateTime DatVrIzmene { get; set; }
        public int Kolicina { get; set; }

        public KnjigaULokalu(int IDKnjiga, int IDBK, int Kolicina)
        {
            this.IDKnjiga = IDKnjiga;
            this.IDBK = IDBK;
            this.Kolicina = Kolicina;
            this.DatVrIzmene = DateTime.Now;
        }
        public KnjigaULokalu(int IDKnjiga, int IDBK, DateTime DatVrIzmene, int Kolicina)
        {
            this.IDKnjiga = IDKnjiga;
            this.IDBK = IDBK;
            this.Kolicina = Kolicina;
            this.DatVrIzmene = DatVrIzmene;
        }
    }
}
