﻿using BP2ProjekatCornerLibrary.Helpers;
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
    [PrimaryKey("IDKnjiga", "IDBib", "DatVr", "IDIK")]
    public class IzKnjigeIzdKuca : _DbClass
    {
        public int IDKnjiga { get; set; }
        public int IDBib { get; set; }
        public DateTime DatVr { get; set; }
        public int IDIK { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDKnjiga", IDKnjiga),
                new ClassPropertyValue("IDBib", IDBib),
                new ClassPropertyValue("DatVr", DatVr),
                new ClassPropertyValue("IDIK", IDIK)
            };
        }

        public IzKnjigeIzdKuca() : base() { }

        public IzKnjigeIzdKuca(int iDKnjiga, int iDBib, int iDIK, DateTime? datVr)
        {
            IDKnjiga = iDKnjiga;
            IDBib = iDBib;
            IDIK = iDIK;
            DatVr = datVr == null ? DateTime.Now : (DateTime)datVr;
        }
        public IzKnjigeIzdKuca(IzmenaKnjige izk, int idik, int idBib) 
        {
            IDKnjiga = izk.IDKnjiga;
            IDIK = idik;
            DatVr = izk.DatVr;
            IDBib = idBib;
        }
    }
}
