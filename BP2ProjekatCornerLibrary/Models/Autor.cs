using BP2ProjekatCornerLibrary.Helpers.Classes;
using BP2ProjekatCornerLibrary.Models.NonContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    public class Autor : _DbClass
    {
        [Key]
        public int IDAutor { get; set; }
        [Required]
        public string Ime { get; set; }
        public string? Prezime { get; set; }
        public DateTime? DatRodj { get; set; }
        public string? Biografija { get; set; }
        public string? Drzava { get; set; }
        public DateTime? DatBrisanja { get; set; }
        public override ClassPropertyValue GetKeyIdentity() => new ClassPropertyValue("IDAutor", IDAutor);
        public string GetFullName { get => Ime + " " + ((Prezime != null) ? Prezime : string.Empty); }

        public override List<ClassPropertyValue> GetKeyProperties()
        {
            return new List<ClassPropertyValue>
            {
                new ClassPropertyValue("IDAutor", IDAutor)
            };
        }
        public override List<string> GetDbPropertyNames()
        {
            return new List<string> { "IDAutor", "Ime", "Prezime", "DatRodj", "Biografija", "Drzava", "DatBrisanja" };
        }


        public Autor() : base() { }

        public Autor(string ime, string? prezime, DateTime? datRodj, string? biografija, string? drzava)
        {
            Ime = ime;
            Prezime = prezime;
            DatRodj = datRodj;
            Biografija = biografija;
            Drzava = drzava;
        }
        public Autor(int id, string ime, string? prezime, DateTime? datRodj, string? biografija, string? drzava)
        {
            IDAutor = id;
            Ime = ime;
            Prezime = prezime;
            DatRodj = datRodj;
            Biografija = biografija;
            Drzava = drzava;
        }
        public Autor(int id, string ime)
        {
            IDAutor = id;
            Ime = ime;
        }
    }
}
