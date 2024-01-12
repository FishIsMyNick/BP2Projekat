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
    [PrimaryKey("ID", "KorisnickoIme")]
    public class KurirKoristiNalog : _DbClass
    {
        public int ID { get; set; }
        public string KorisnickoIme { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("ID", ID),
                new ClassPropertyValue("KorisnickoIme", KorisnickoIme)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "ID", "KorisnickoIme" };
        }

        public KurirKoristiNalog() : base() { }

        public KurirKoristiNalog(int iD, string korisnickoIme)
        {
            ID = iD;
            KorisnickoIme = korisnickoIme ?? throw new ArgumentNullException(nameof(korisnickoIme));
        }
    }
}
