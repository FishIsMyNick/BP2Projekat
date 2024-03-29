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
    [PrimaryKey("IDSStivo", "BrIzd", "IDBK", "DatVrIzmene")]
    public class IzdSStivoULokalu : _DbClass
    {
        public int IDSStivo { get; set; }
        public int BrIzd { get; set; }
        public int IDBK { get; set; }
        [Required]
        public DateTime DatVrIzmene { get; set; }
        [Required]
        public int Kolicina { get; set; }

        public DateTime? DatBrisanja { get; set; }
        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDSStivo", IDSStivo),
                new ClassPropertyValue("BrIzd", BrIzd),
                new ClassPropertyValue("IDBK", IDBK),
                new ClassPropertyValue("DatVrIzmene", DatVrIzmene)
            };
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDSStivo", "BrIzd", "IDBK", "DatVrIzmene", "Kolicina", "DatBrisanja" };
        }

        public IzdSStivoULokalu() : base() { }

        public IzdSStivoULokalu(int iDSStivo, int brIzd, int iDBK, int kolicina)
        {
            IDSStivo = iDSStivo;
            BrIzd = brIzd;
            IDBK = iDBK;
            Kolicina = kolicina;
            DatVrIzmene = DateTime.Now;
        }
    }
}
