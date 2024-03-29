﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP2ProjekatCornerLibrary.Helpers.Classes;
using BP2ProjekatCornerLibrary.Models.NonContext;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Format : _DbClass
    {
        [Key]
        public string NazivFormata { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("NazivFormata", NazivFormata)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "NazivFormata" };
        }

        public Format() : base() { }

        public Format(string nazivFormata)
        {
            NazivFormata = nazivFormata ?? throw new ArgumentNullException(nameof(nazivFormata));
        }
    }
}
