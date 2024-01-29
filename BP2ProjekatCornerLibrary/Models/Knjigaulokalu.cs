using BP2ProjekatCornerLibrary.Helpers.Classes;
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
    [PrimaryKey("IDKnjiga", "IDBK", "DatVrIzmene")]
    public class KnjigaULokalu : _DbClass
    {
        public int IDKnjiga { get; set; }
        public int IDBK { get; set; }
        public DateTime DatVrIzmene { get; set; }
        public int Kolicina { get; set; }
        public DateTime? DatBrisanja { get; set; }

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDKnjiga", IDKnjiga),
                new ClassPropertyValue("IDBK", IDBK),
                new ClassPropertyValue("DatVrIzmene", DatVrIzmene)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDKnjiga", "IDBK", "DatVrIzmene", "Kolicina", "DatBrisanja" };
        }

        public KnjigaULokalu() : base() { }
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
