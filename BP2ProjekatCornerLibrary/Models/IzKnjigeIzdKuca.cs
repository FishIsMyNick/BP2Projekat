﻿using BP2ProjekatCornerLibrary.Helpers.Classes;
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

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDKnjiga", "IDBib", "DatVr", "IDIK" };
        }

        public IzKnjigeIzdKuca() : base() { }

        public IzKnjigeIzdKuca(IzmenaKnjige izK, int iDIK) : base() 
        {
            IDKnjiga = izK.IDKnjiga;
            IDBib = izK.IDBib;
            IDIK = iDIK;
            DatVr = izK.DatVr;
        }
    }
}
