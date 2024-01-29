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
    [PrimaryKey("IDSStivo", "IDIK")]
    public class IzdajeSStivo : _DbClass
    {
        public int IDSStivo { get; set; }
        public int IDIK { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDSStivo", IDSStivo),
                new ClassPropertyValue("IDIK", IDIK)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDSStivo", "IDIK" };
        }

        public IzdajeSStivo() : base() { }

        public IzdajeSStivo(int idSStivo, int idik)
        {
            IDSStivo = idSStivo;
            IDIK = idik;
        }
    }
}
