using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models.NonContext;

namespace BP2ProjekatCornerLibrary.Models
{
    public class SerijskoStivo : _DbClass
    {
        [Key] 
        public int IDSStivo { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public int TipStiva { get; set; }
        [Required]
        public string? Format { get; set; }
        [Required]
        public string? Period { get; set; }
        public override ClassPropertyValue GetKeyIdentity() => new ClassPropertyValue("IDSStivo", IDSStivo);
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDSStivo", IDSStivo)
            };
        }

        public SerijskoStivo() : base() { }

        public SerijskoStivo(int iDSStivo, string naziv, int tipStiva, string? format, string? period)
        {
            IDSStivo = iDSStivo;
            Naziv = naziv ?? throw new ArgumentNullException(nameof(naziv));
            TipStiva = tipStiva;
            Format = format;
            Period = period;
        }
        public SerijskoStivo(string naziv, int tipStiva, string? format, string? period)
        {
            IDSStivo = -1;
            Naziv = naziv ?? throw new ArgumentNullException(nameof(naziv));
            TipStiva = tipStiva;
            Format = format;
            Period = period;
        }
        public SerijskoStivo (SerijskoStivo clone) : base()
        {
            IDSStivo = clone.IDSStivo;
            Naziv = clone.Naziv;
            TipStiva = clone.TipStiva;
            Format = clone.Format;
            Period = clone.Period;
        }
    }
}
