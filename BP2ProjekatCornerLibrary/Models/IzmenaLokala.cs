using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models.NonContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("IDLokal", "IDAdmin", "DatVrIL")]
    public class IzmenaLokala : _DbClass
    {
        public int IDLokal { get; set; }
        public int IDAdmin { get; set; }
        public DateTime DatVrIL { get; set; }
        public string? Naziv { get; set; }
        public DateTime? DatOtv { get; set; }
        public DateTime? DatZat { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDLokal", IDLokal),
                new ClassPropertyValue("IDAdmin", IDAdmin),
                new ClassPropertyValue("DatVrIL", DatVrIL)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDLokal", "IDAdmin", "DatVrIL", "Naziv", "DatOtv", "DatZat" };
        }

        public IzmenaLokala() : base() { }

        public IzmenaLokala(int iDLokal, int iDAdmin, string? naziv = null, DateTime? datOtv = null, DateTime? datZat = null) : base()
        {
            IDLokal = iDLokal;
            IDAdmin = iDAdmin;
            Naziv = naziv;
            DatOtv = datOtv;
            DatZat = datZat;
            DatVrIL = DateTime.Now;
        }
        public IzmenaLokala(Biblikutak newBK, int idAdmin) : base() 
        {
            IDLokal = newBK.IDBK;
            IDAdmin = idAdmin;
            DatVrIL = DateTime.Now;

            Naziv = newBK.Naziv;
            DatOtv = newBK.DatOtv;
            DatZat = newBK.DatZat;
        }
    }
}
