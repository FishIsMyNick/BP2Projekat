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
    [PrimaryKey("IDKnjiga", "IDAutor")]
    public class Pise : _DbClass
    {
        public int IDKnjiga { get; set; }
        public int IDAutor { get; set; }


        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDKnjiga", IDKnjiga),
                new ClassPropertyValue("IDAutor", IDAutor)
            };
        }
        public Pise() : base() { }
        public Pise(int IDKnjiga, int IDAutor)
        {
            this.IDKnjiga = IDKnjiga;
            this.IDAutor = IDAutor;
        }

        public override bool Equals(object? obj)
        {
            if(!(obj is Pise)) return false;
            return (obj as Pise).IDKnjiga == IDKnjiga && (obj as Pise).IDAutor == IDAutor;
        }

        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDKnjiga", "IDAutor" };
        }
    }
}
