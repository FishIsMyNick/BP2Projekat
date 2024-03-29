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
    [PrimaryKey("IDKnjiga", "OZNZ")]
    public class PripadaZanru : _DbClass
    {
        public int IDKnjiga
        {
            get; set;
        }
        public string OZNZ
        {
            get; set;
        }

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDKnjiga", IDKnjiga),
                new ClassPropertyValue("OZNZ", OZNZ)
            };
        }
        public PripadaZanru() : base() { }
        public PripadaZanru(int IDKnjiga, string OZNZ)
        {
            this.IDKnjiga = IDKnjiga;
            this.OZNZ = OZNZ;
        }

        public override bool Equals(object? obj)
        {
            if(!(obj is PripadaZanru)) return false;
            return (obj as PripadaZanru).IDKnjiga == this.IDKnjiga && (obj as PripadaZanru).OZNZ == this.OZNZ;
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDKnjiga", "OZNZ" };
        }
    }
}
