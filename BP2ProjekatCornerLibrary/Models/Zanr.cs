﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Zanr
    {
        [Key]
        public string OZNZ { get; set; }
        [Required]
        public string NazivZanra { get; set; }

        public Zanr() { }
        public Zanr(string oznz, string naziv)
        {
            OZNZ = oznz;
            NazivZanra = naziv;
        }
    }
}
