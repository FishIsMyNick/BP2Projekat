﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Jezik
    {
        [Key]
        public string OZNJ { get; set; }
        [Required]
        public string NazivJezika { get; set; }

        public Jezik() { }
        public Jezik(string oznj, string naziv)
        {
            OZNJ = oznj;
            NazivJezika = naziv;
        }
    }
}
