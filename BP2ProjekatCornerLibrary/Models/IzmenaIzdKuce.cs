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
    [PrimaryKey("IDIK", "IDBib", "DatVr")]
    public class IzmenaIzdKuce : _DbClass
    {
        public int IDIK { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public string? Naziv { get; set; }
        public string? Ulica { get; set; }
        public string? Broj { get; set; }
        public int? PosBr { get; set; }
        public string? OZND { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDIK", IDIK),
                new ClassPropertyValue("IDBib", IDBib),
                new ClassPropertyValue("DatVr", DatVr)
            };
        }

        public IzmenaIzdKuce() : base() { }

        public IzmenaIzdKuce(int iDIK, int iDBib, string? naziv, string? ulica, string? broj, int? posBr, string? oZND)
        {
            IDIK = iDIK;
            IDBib = iDBib;
            Naziv = naziv;
            Ulica = ulica;
            Broj = broj;
            PosBr = posBr;
            OZND = oZND;
            DatVr = DateTime.Now;
        }
        public IzmenaIzdKuce(IzdKuca ik, int IdBib)
        {
            IDIK = ik.IDIK;
            IDBib = IdBib;
            DatVr = DateTime.Now;

            IzdKuca postojeca = DBHelper.GetIzdKuca(IDIK);

            Naziv = postojeca.Naziv != ik.Naziv ? ik.Naziv : null;
            Ulica = postojeca.Ulica != ik.Ulica ? ik.Ulica : null;
            Broj = postojeca.Broj != ik.Broj ? ik.Broj : null;
            PosBr = postojeca.PosBr != ik.PosBr ? ik.PosBr : null;
            OZND = postojeca.OZND != ik.OZND ? ik.OZND : null;
        }
    }
}
