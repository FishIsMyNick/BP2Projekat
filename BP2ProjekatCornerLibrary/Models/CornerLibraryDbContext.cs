using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Models
{
    public class CornerLibraryDbContext : DbContext
    {
        public CornerLibraryDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<AdminKoristiNalog> AdminKoristiNalog { get; set; }
        public DbSet<Adresa> Adresa { get; set; }
        public DbSet<Autor> Autor { get; set; }
        public DbSet<Biblikutak> Biblikutak { get; set; }
        public DbSet<Bibliotekar> Bibliotekar { get; set; }
        public DbSet<BibliotekarKoristiNalog> BibliotekarKoristiNalog { get; set; }
        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<Format> Format { get; set; }
        public DbSet<IzdajeKnjigu> IzdajeKnjigu { get; set; }
        public DbSet<IzdajeSStivo> IzdajeSStivo { get; set; }
        public DbSet<IzdanjeSStiva> IzdanjeSStiva { get; set; }
        public DbSet<IzdKuca> IzdKuca { get; set; }
        public DbSet<IzdSStivoULokalu> IzdSStivoULokalu { get; set; }
        public DbSet<IzKnjigeAutor> IzKnjigeAutor { get; set; }
        public DbSet<IzKnjigeIzdKuca> IzKnjigeIzdKuca { get; set; }
        public DbSet<IzKnjigeJezik> IzKnjigeJezik { get; set; }
        public DbSet<IzKnjigeZanr> IzKnjigeZanr { get; set; }
        public DbSet<IzmenaAutora> IzmenaAutora { get; set; }
        public DbSet<IzmenaIzdKuce> IzmenaIzdKuce { get; set; }
        public DbSet<IzmenaSStiva> IzmenaSStiva { get; set; }
        public DbSet<IzmenaIzdSStiva> IzmenaIzdSStiva { get; set; }
        public DbSet<IzmenaKnjige> IzmenaKnjige { get; set; }
        public DbSet<IzmenaLokala> IzmenaLokala { get; set; }
        public DbSet<IzSStivaIzdKuca> IzSStivaIzdKuca { get; set; }
        public DbSet<IzSStivaJezik> IzSStivaJezik { get; set; }
        public DbSet<Jezik> Jezik { get; set; }
        public DbSet<Knjiga> Knjiga { get; set; }
        public DbSet<KnjigaNaJeziku> KnjigaNaJeziku { get; set; }
        public DbSet<KnjigaULokalu> KnjigaUlokalu { get; set; }
        public DbSet<KorisnickiNalog> KorisnickiNalog { get; set; }
        public DbSet<Kurir> Kurir { get; set; }
        public DbSet<KurirKoristiNalog> KurirKoristiNalog { get; set; }
        public DbSet<Lokacija> Lokacija { get; set; }
        public DbSet<Mesto> Mesto { get; set; }
        public DbSet<MestoUDrzavi> MestoUDrzavi { get; set; }
        public DbSet<Periodicnost> Periodicnost { get; set; }
        public DbSet<Pise> Pise { get; set; }
        public DbSet<PripadaZanru> PripadaZanru { get; set; }
        public DbSet<RasporedjenBibliotekar> RasporedjenBibliotekar { get; set; }
        public DbSet<SerijskoStivo> SerijskoStivo { get; set; }
        public DbSet<SStivoNaJeziku> SStivoNaJeziku { get; set; }
        public DbSet<Zanr> Zanr { get; set; }

    }
}
