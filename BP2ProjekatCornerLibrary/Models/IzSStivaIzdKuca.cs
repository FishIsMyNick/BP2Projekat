using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models.NonContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BP2ProjekatCornerLibrary.Models
{
    [PrimaryKey("IDSStivo", "IDBib", "DatVr", "IDIK")]
    public class IzSStivaIzdKuca : _DbClass
    {
        public int IDSStivo { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public int IDIK { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDSStivo", IDSStivo),
                new ClassPropertyValue("IDBib", IDBib),
                new ClassPropertyValue("DatVr", DatVr),
                new ClassPropertyValue("IDIK", IDIK)
            };
        }

        public IzSStivaIzdKuca() : base() { }

        public IzSStivaIzdKuca(int iDSStivo, int iDIK, int iDBib)
        {
            IDSStivo = iDSStivo;
            IDBib = iDBib;
            IDIK = iDIK;
            DatVr = DateTime.Now;
        }
        public IzSStivaIzdKuca(IzdajeSStivo i, int idBib)
        {
            IDSStivo = i.IDSStivo;
            IDBib = idBib;
            IDIK = i.IDIK;
            DatVr = DateTime.Now;
        }
    }
}
