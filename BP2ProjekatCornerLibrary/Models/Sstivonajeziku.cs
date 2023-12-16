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
    [PrimaryKey("IDSStivo", "OZNJ")]
    public class SStivoNaJeziku : _DbClass
    {
        public int IDSStivo { get; set; }
        public string OZNJ { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDSStivo", IDSStivo),
                new ClassPropertyValue("OZNJ", OZNJ)
            };
        }

        public SStivoNaJeziku() : base() { }

        public SStivoNaJeziku(int iDSStivo, string oZNJ)
        {
            IDSStivo = iDSStivo;
            OZNJ = oZNJ ?? throw new ArgumentNullException(nameof(oZNJ));
        }
    }
}
