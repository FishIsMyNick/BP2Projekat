using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BP2ProjekatCornerLibrary.Models;

public partial class CornerLibraryDbContext : DbContext
{
    public CornerLibraryDbContext()
    {
    }

    public CornerLibraryDbContext(DbContextOptions<CornerLibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adresa> Adresas { get; set; }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Biblikutak> Biblikutaks { get; set; }

    public virtual DbSet<Clan> Clans { get; set; }

    public virtual DbSet<Clanarina> Clanarinas { get; set; }

    public virtual DbSet<Drzava> Drzavas { get; set; }

    public virtual DbSet<IspunjenZahtevKnjiga> Ispunjenzahtevknjigas { get; set; }

    public virtual DbSet<IspunjenZahtevSerijskoStivo> Ispunjenzahtevserijskostivos { get; set; }

    public virtual DbSet<IstorijaClanarina> Istorijaclanarinas { get; set; }

    public virtual DbSet<IzdKuca> Izdkucas { get; set; }

    public virtual DbSet<IzmenaKredita> Izmenakredita { get; set; }

    public virtual DbSet<IzmenaLokacije> Izmenalokacijes { get; set; }

    public virtual DbSet<IzmenaPodataka> Izmenapodatakas { get; set; }

    public virtual DbSet<IzmenaSifre> Izmenasifres { get; set; }

    public virtual DbSet<IzmenaStatusa> Izmenastatusas { get; set; }

    public virtual DbSet<Jezik> Jeziks { get; set; }

    public virtual DbSet<Knjiga> Knjigas { get; set; }

    public virtual DbSet<KnjigaULokalu> Knjigaulokalus { get; set; }

    public virtual DbSet<Kupovina> Kupovinas { get; set; }

    public virtual DbSet<Lokacija> Lokacijas { get; set; }

    public virtual DbSet<Mesto> Mestos { get; set; }

    public virtual DbSet<OcenaKnjige> Ocenaknjiges { get; set; }

    public virtual DbSet<OcenaSStiva> Ocenasstivas { get; set; }

    public virtual DbSet<Periodicnost> Periodicnosts { get; set; }

    public virtual DbSet<Radnik> Radniks { get; set; }

    public virtual DbSet<Rezervacija> Rezervacijas { get; set; }

    public virtual DbSet<SerijskoStivo> Serijskostivos { get; set; }

    public virtual DbSet<SerijskoStivoULokalu> Serijskostivoulokalus { get; set; }

    public virtual DbSet<ZahtevZaKnjigu> Zahtevzaknjigus { get; set; }

    public virtual DbSet<ZahtevZaSerijskoStivo> Zahtevzaserijskostivos { get; set; }

    public virtual DbSet<Zanr> Zanrs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\CORNERLIBRARYDB;Database=CornerLibraryDB;User Id=sa;Password=bp2sifra;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adresa>(entity =>
        {
            entity.HasKey(e => new { e.Ulica, e.Broj }).HasName("ADRESA_PK");

            entity.ToTable("ADRESA");

            entity.Property(e => e.Ulica)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Broj)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.IDAutor).HasName("AUT_PK");

            entity.ToTable("AUTOR");

            entity.Property(e => e.IDAutor)
                .ValueGeneratedNever()
                .HasColumnName("IDAutor");
            entity.Property(e => e.Biografija).IsUnicode(false);
            entity.Property(e => e.DatRodj).HasColumnType("date");
            entity.Property(e => e.Ime)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OZND)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZND");
            entity.Property(e => e.Prezime)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.OZNDNavigation).WithMany(p => p.Autors)
                .HasForeignKey(d => d.OZND)
                .HasConstraintName("AUT_FK");
        });

        modelBuilder.Entity<Biblikutak>(entity =>
        {
            entity.HasKey(e => e.IDBK).HasName("BIB_PK");

            entity.ToTable("BIBLIKUTAK");

            entity.Property(e => e.IDBK)
                .ValueGeneratedNever()
                .HasColumnName("IDBK");
            entity.Property(e => e.Broj)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OZND)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZND");
            entity.Property(e => e.Ulica)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Lokacija).WithMany(p => p.Biblikutaks)
                .HasForeignKey(d => new { d.Ulica, d.Broj, d.PosBr, d.OZND })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BIB_FK");
        });

        modelBuilder.Entity<Clan>(entity =>
        {
            entity.HasKey(e => e.IDClan).HasName("CL_PK");

            entity.ToTable("CLAN");

            entity.Property(e => e.IDClan)
                .ValueGeneratedNever()
                .HasColumnName("IDClan");
            entity.Property(e => e.BrTel)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Broj)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DatRodj).HasColumnType("date");
            entity.Property(e => e.Ime)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.KorisnickoIme)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.OZND)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZND");
            entity.Property(e => e.Prezime)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Sifra).IsUnicode(false);
            entity.Property(e => e.Ulica)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Lokacija).WithMany(p => p.Clans)
                .HasForeignKey(d => new { d.Ulica, d.Broj, d.PosBr, d.OZND })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CL_FK");
        });

        modelBuilder.Entity<Clanarina>(entity =>
        {
            entity.HasKey(e => new { e.OZNC, e.DatUvoda }).HasName("CLA_PK");

            entity.ToTable("CLANARINA");

            entity.Property(e => e.OZNC)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZNC");
            entity.Property(e => e.DatUvoda).HasColumnType("date");
            entity.Property(e => e.NazivClanarine)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Clanarina");
        });

        modelBuilder.Entity<Drzava>(entity =>
        {
            entity.HasKey(e => e.OZND).HasName("OZND_PK");

            entity.ToTable("DRZAVA");

            entity.Property(e => e.OZND)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZND");
            entity.Property(e => e.NazivDrzave)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Drzava");
        });

        modelBuilder.Entity<IspunjenZahtevKnjiga>(entity =>
        {
            entity.HasKey(e => new { e.IDClan, e.IDKnjiga, e.IDBK }).HasName("IZK_PK");

            entity.ToTable("ISPUNJENZAHTEVKNJIGA");

            entity.Property(e => e.IDClan).HasColumnName("IDClan");
            entity.Property(e => e.IDKnjiga).HasColumnName("IDKnjiga");
            entity.Property(e => e.IDBK).HasColumnName("IDBK");
            entity.Property(e => e.DatVrIsp).HasColumnType("datetime");

            entity.HasOne(d => d.IDClanNavigation).WithMany(p => p.IspunjenZahtevKnjigas)
                .HasForeignKey(d => d.IDClan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZK_FK1");

            entity.HasOne(d => d.IDKULNavigation).WithMany(p => p.IspunjenZahtevKnjigas)
                .HasForeignKey(d => new { d.IDKnjiga, d.IDBK })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZK_FK2");
        });

        modelBuilder.Entity<IspunjenZahtevSerijskoStivo>(entity =>
        {
            entity.HasKey(e => new { e.IDClan, e.IDSStivo, e.IDBK }).HasName("IZS_PK");

            entity.ToTable("ISPUNJENZAHTEVSERIJSKOSTIVO");

            entity.Property(e => e.IDClan).HasColumnName("IDClan");
            entity.Property(e => e.IDSStivo).HasColumnName("IDSStivo");
            entity.Property(e => e.IDBK).HasColumnName("IDBK");
            entity.Property(e => e.DatVrIsp).HasColumnType("datetime");

            entity.HasOne(d => d.IDClanNavigation).WithMany(p => p.IspunjenZahtevSerijskoStivos)
                .HasForeignKey(d => d.IDClan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZS_FK1");

            entity.HasOne(d => d.IDSStivoULokaluNavigation).WithMany(p => p.IspunjenZahtevSerijskoStivos)
                .HasForeignKey(d => new { d.IDSStivo, d.IDBK })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZS_FK2");
        });

        modelBuilder.Entity<IstorijaClanarina>(entity =>
        {
            entity.HasKey(e => new { e.IDCL, e.IDClan, e.OZNC, e.DatUvoda }).HasName("ICL_PK");

            entity.ToTable("ISTORIJACLANARINA");

            entity.Property(e => e.IDCL).HasColumnName("IDCL");
            entity.Property(e => e.IDClan).HasColumnName("IDClan");
            entity.Property(e => e.OZNC)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZNC");
            entity.Property(e => e.DatUvoda).HasColumnType("date");
            entity.Property(e => e.DatVrStart).HasColumnType("datetime");
            entity.Property(e => e.DatVrUplate).HasColumnType("datetime");

            entity.HasOne(d => d.IDClanNavigation).WithMany(p => p.IstorijaClanarinas)
                .HasForeignKey(d => d.IDClan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ICL_FK1");

            entity.HasOne(d => d.Clanarina).WithMany(p => p.IstorijaClanarinas)
                .HasForeignKey(d => new { d.OZNC, d.DatUvoda })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ICL_FK2");
        });

        modelBuilder.Entity<IzdKuca>(entity =>
        {
            entity.HasKey(e => e.IDIK).HasName("IK_PK");

            entity.ToTable("IZDKUCA");

            entity.Property(e => e.IDIK)
                .ValueGeneratedNever()
                .HasColumnName("IDIK");
            entity.Property(e => e.Broj)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OZND)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZND");
            entity.Property(e => e.Ulica)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Lokacija).WithMany(p => p.IzdKucas)
                .HasForeignKey(d => new { d.Ulica, d.Broj, d.PosBr, d.OZND })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IK_FK");
        });

        modelBuilder.Entity<IzmenaKredita>(entity =>
        {
            entity.HasKey(e => new { e.IDKredit, e.IDClan, e.IDBK }).HasName("IKR_PK");

            entity.ToTable("IZMENAKREDITA");

            entity.Property(e => e.IDKredit).HasColumnName("IDKredit");
            entity.Property(e => e.IDClan).HasColumnName("IDClan");
            entity.Property(e => e.IDBK).HasColumnName("IDBK");
            entity.Property(e => e.DatVr).HasColumnType("datetime");

            entity.HasOne(d => d.IDBKNavigation).WithMany(p => p.IzmenaKredita)
                .HasForeignKey(d => d.IDBK)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IKR_FK2");

            entity.HasOne(d => d.IDClanNavigation).WithMany(p => p.IzmenaKredita)
                .HasForeignKey(d => d.IDClan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IKR_FK1");
        });

        modelBuilder.Entity<IzmenaLokacije>(entity =>
        {
            entity.HasKey(e => new { e.IDIL, e.IDClan, e.Ulica, e.Broj, e.PosBr, e.OZND }).HasName("IZL_PK");

            entity.ToTable("IZMENALOKACIJE");

            entity.Property(e => e.IDIL).HasColumnName("IDIL");
            entity.Property(e => e.IDClan).HasColumnName("IDClan");
            entity.Property(e => e.Ulica)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Broj)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OZND)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZND");
            entity.Property(e => e.DatVr).HasColumnType("datetime");

            entity.HasOne(d => d.IDClanNavigation).WithMany(p => p.IzmenaLokacijes)
                .HasForeignKey(d => d.IDClan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZL_FK1");

            entity.HasOne(d => d.Lokacija).WithMany(p => p.IzmenaLokacijes)
                .HasForeignKey(d => new { d.Ulica, d.Broj, d.PosBr, d.OZND })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZL_FK2");
        });

        modelBuilder.Entity<IzmenaPodataka>(entity =>
        {
            entity.HasKey(e => new { e.IDPodatak, e.IDClan }).HasName("IZP_PK");

            entity.ToTable("IZMENAPODATAKA");

            entity.Property(e => e.IDPodatak).HasColumnName("IDPodatak");
            entity.Property(e => e.IDClan).HasColumnName("IDClan");
            entity.Property(e => e.DatVr).HasColumnType("datetime");
            entity.Property(e => e.Ime)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Prezime)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IDClanNavigation).WithMany(p => p.IzmenaPodatakas)
                .HasForeignKey(d => d.IDClan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZP_FK");
        });

        modelBuilder.Entity<IzmenaSifre>(entity =>
        {
            entity.HasKey(e => new { e.IDSifra, e.IDClan }).HasName("IZSI_PK");

            entity.ToTable("IZMENASIFRE");

            entity.Property(e => e.IDSifra).HasColumnName("IDSifra");
            entity.Property(e => e.IDClan).HasColumnName("IDClan");
            entity.Property(e => e.DatVr).HasColumnType("datetime");
            entity.Property(e => e.Sifra).IsUnicode(false);

            entity.HasOne(d => d.IDClanNavigation).WithMany(p => p.IzmenaSifres)
                .HasForeignKey(d => d.IDClan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZSI_FK");
        });

        modelBuilder.Entity<IzmenaStatusa>(entity =>
        {
            entity.HasKey(e => new { e.IDStatus, e.IDClan }).HasName("IZST_PK");

            entity.ToTable("IZMENASTATUSA");

            entity.Property(e => e.IDStatus).HasColumnName("IDStatus");
            entity.Property(e => e.IDClan).HasColumnName("IDClan");
            entity.Property(e => e.DatVr).HasColumnType("datetime");

            entity.HasOne(d => d.IDClanNavigation).WithMany(p => p.IzmenaStatusas)
                .HasForeignKey(d => d.IDClan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZST_FK");
        });

        modelBuilder.Entity<Jezik>(entity =>
        {
            entity.HasKey(e => e.OZNJ).HasName("JEZ_PK");

            entity.ToTable("JEZIK");

            entity.Property(e => e.OZNJ)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("OZNJ");
            entity.Property(e => e.NazivJezika)
                .IsUnicode(false)
                .HasColumnName("Jezik");
        });

        modelBuilder.Entity<Knjiga>(entity =>
        {
            entity.HasKey(e => e.IDKnjiga).HasName("KNJ_PK");

            entity.ToTable("KNJIGA");

            entity.Property(e => e.IDKnjiga)
                .ValueGeneratedNever()
                .HasColumnName("IDKnjiga");
            entity.Property(e => e.GodIzd).HasColumnType("date");
            entity.Property(e => e.Naziv).IsUnicode(false);

            entity.HasMany(d => d.IDAutors).WithMany(p => p.IDKnjigas)
                .UsingEntity<Dictionary<string, object>>(
                    "Pise",
                    r => r.HasOne<Autor>().WithMany()
                        .HasForeignKey("Idautor")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("PIS_FK2"),
                    l => l.HasOne<Knjiga>().WithMany()
                        .HasForeignKey("Idknjiga")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("PIS_FK1"),
                    j =>
                    {
                        j.HasKey("Idknjiga", "Idautor").HasName("PIS_PK");
                        j.ToTable("PISE");
                        j.IndexerProperty<int>("Idknjiga").HasColumnName("IDKnjiga");
                        j.IndexerProperty<int>("Idautor").HasColumnName("IDAutor");
                    });

            entity.HasMany(d => d.IDIKs).WithMany(p => p.IDKnjigas)
                .UsingEntity<Dictionary<string, object>>(
                    "Izdajeknjigu",
                    r => r.HasOne<IzdKuca>().WithMany()
                        .HasForeignKey("Idik")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("IKK_FK2"),
                    l => l.HasOne<Knjiga>().WithMany()
                        .HasForeignKey("Idknjiga")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("IKK_FK1"),
                    j =>
                    {
                        j.HasKey("Idknjiga", "Idik").HasName("IKK_PK");
                        j.ToTable("IZDAJEKNJIGU");
                        j.IndexerProperty<int>("Idknjiga").HasColumnName("IDKnjiga");
                        j.IndexerProperty<int>("Idik").HasColumnName("IDIK");
                    });

            entity.HasMany(d => d.OZNJs).WithMany(p => p.IDKnjigas)
                .UsingEntity<Dictionary<string, object>>(
                    "Knjiganajeziku",
                    r => r.HasOne<Jezik>().WithMany()
                        .HasForeignKey("Oznj")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("KJZ_FK2"),
                    l => l.HasOne<Knjiga>().WithMany()
                        .HasForeignKey("Idknjiga")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("KJZ_FK1"),
                    j =>
                    {
                        j.HasKey("Idknjiga", "Oznj").HasName("KJZ_PK");
                        j.ToTable("KNJIGANAJEZIKU");
                        j.IndexerProperty<int>("Idknjiga").HasColumnName("IDKnjiga");
                        j.IndexerProperty<string>("Oznj")
                            .HasMaxLength(4)
                            .IsUnicode(false)
                            .HasColumnName("OZNJ");
                    });

            entity.HasMany(d => d.OZNZs).WithMany(p => p.IDKnjigas)
                .UsingEntity<Dictionary<string, object>>(
                    "Pripadazanru",
                    r => r.HasOne<Zanr>().WithMany()
                        .HasForeignKey("Oznz")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("KPZ_FK2"),
                    l => l.HasOne<Knjiga>().WithMany()
                        .HasForeignKey("Idknjiga")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("KPZ_FK1"),
                    j =>
                    {
                        j.HasKey("Idknjiga", "Oznz").HasName("KPZ_PK");
                        j.ToTable("PRIPADAZANRU");
                        j.IndexerProperty<int>("Idknjiga").HasColumnName("IDKnjiga");
                        j.IndexerProperty<string>("Oznz")
                            .HasMaxLength(4)
                            .IsUnicode(false)
                            .HasColumnName("OZNZ");
                    });
        });

        modelBuilder.Entity<KnjigaULokalu>(entity =>
        {
            entity.HasKey(e => new { e.IDKnjiga, e.IDBK }).HasName("KUL_PK");

            entity.ToTable("KNJIGAULOKALU");

            entity.Property(e => e.IDKnjiga).HasColumnName("IDKnjiga");
            entity.Property(e => e.IDBK).HasColumnName("IDBK");

            entity.HasOne(d => d.IDBKNavigation).WithMany(p => p.KnjigaULokalus)
                .HasForeignKey(d => d.IDBK)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("KUL_FK2");

            entity.HasOne(d => d.IDKnjigaNavigation).WithMany(p => p.KnjigaULokalus)
                .HasForeignKey(d => d.IDKnjiga)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("KUL_FK1");
        });

        modelBuilder.Entity<Kupovina>(entity =>
        {
            entity.HasKey(e => new { e.IDKup, e.IDSStivo, e.IDClan, e.IDBK }).HasName("KUP_PK");

            entity.ToTable("KUPOVINA");

            entity.Property(e => e.IDKup).HasColumnName("IDKup");
            entity.Property(e => e.IDSStivo).HasColumnName("IDSStivo");
            entity.Property(e => e.IDClan).HasColumnName("IDClan");
            entity.Property(e => e.IDBK).HasColumnName("IDBK");
            entity.Property(e => e.DatVr).HasColumnType("datetime");
            entity.Property(e => e.DatVrPot).HasColumnType("datetime");

            entity.HasOne(d => d.IDClanNavigation).WithMany(p => p.Kupovinas)
                .HasForeignKey(d => d.IDClan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("KUP_FK2");

            entity.HasOne(d => d.PotvrdioKupNavigation).WithMany(p => p.Kupovinas)
                .HasForeignKey(d => d.PotvrdioKup)
                .HasConstraintName("KUP_FK3");

            entity.HasOne(d => d.ISStivoULokaluNavigation).WithMany(p => p.Kupovinas)
                .HasForeignKey(d => new { d.IDSStivo, d.IDBK })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("KUP_FK1");
        });

        modelBuilder.Entity<Lokacija>(entity =>
        {
            entity.HasKey(e => new { e.Ulica, e.Broj, e.PosBr, e.OZND }).HasName("LOK_PK");

            entity.ToTable("LOKACIJA");

            entity.Property(e => e.Ulica)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Broj)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OZND)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZND");

            entity.HasOne(d => d.OZNDNavigation).WithMany(p => p.Lokacijas)
                .HasForeignKey(d => d.OZND)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LOK_FK3");

            entity.HasOne(d => d.PosBrNavigation).WithMany(p => p.Lokacijas)
                .HasForeignKey(d => d.PosBr)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LOK_FK2");

            entity.HasOne(d => d.Adresa).WithMany(p => p.Lokacijas)
                .HasForeignKey(d => new { d.Ulica, d.Broj })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LOK_FK1");
        });

        modelBuilder.Entity<Mesto>(entity =>
        {
            entity.HasKey(e => e.PosBr).HasName("MESTO_PK");

            entity.ToTable("MESTO");

            entity.Property(e => e.PosBr).ValueGeneratedNever();
            entity.Property(e => e.NazivMesta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Mesto");

            entity.HasMany(d => d.OZNDs).WithMany(p => p.PosBrs)
                .UsingEntity<Dictionary<string, object>>(
                    "Mestoudrzavi",
                    r => r.HasOne<Drzava>().WithMany()
                        .HasForeignKey("Oznd")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("MUD_FK2"),
                    l => l.HasOne<Mesto>().WithMany()
                        .HasForeignKey("PosBr")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("MUD_FK1"),
                    j =>
                    {
                        j.HasKey("PosBr", "Oznd").HasName("MUD_PK");
                        j.ToTable("MESTOUDRZAVI");
                        j.IndexerProperty<string>("Oznd")
                            .HasMaxLength(3)
                            .IsUnicode(false)
                            .HasColumnName("OZND");
                    });
        });

        modelBuilder.Entity<OcenaKnjige>(entity =>
        {
            entity.HasKey(e => new { e.IDOcenaK, e.IDClan, e.IDKnjiga }).HasName("OCK_PK");

            entity.ToTable("OCENAKNJIGE");

            entity.Property(e => e.IDOcenaK).HasColumnName("IDOcenaK");
            entity.Property(e => e.IDClan).HasColumnName("IDClan");
            entity.Property(e => e.IDKnjiga).HasColumnName("IDKnjiga");
            entity.Property(e => e.DatVr).HasColumnType("datetime");
            entity.Property(e => e.Komentar).IsUnicode(false);

            entity.HasOne(d => d.IDClanNavigation).WithMany(p => p.OcenaKnjiges)
                .HasForeignKey(d => d.IDClan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OCK_FK1");

            entity.HasOne(d => d.IDKnjigaNavigation).WithMany(p => p.OcenaKnjiges)
                .HasForeignKey(d => d.IDKnjiga)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OCK_FK2");
        });

        modelBuilder.Entity<OcenaSStiva>(entity =>
        {
            entity.HasKey(e => new { e.IDOcenaS, e.IDClan, e.IDSStivo }).HasName("OCS_PK");

            entity.ToTable("OCENASSTIVA");

            entity.Property(e => e.IDOcenaS).HasColumnName("IDOcenaS");
            entity.Property(e => e.IDClan).HasColumnName("IDClan");
            entity.Property(e => e.IDSStivo).HasColumnName("IDSStivo");
            entity.Property(e => e.DatVr).HasColumnType("datetime");
            entity.Property(e => e.Komentar).IsUnicode(false);

            entity.HasOne(d => d.IDClanNavigation).WithMany(p => p.OcenaSStivas)
                .HasForeignKey(d => d.IDClan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OCS_FK1");

            entity.HasOne(d => d.IDSStivoNavigation).WithMany(p => p.OcenaSStivas)
                .HasForeignKey(d => d.IDSStivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OCS_FK2");
        });

        modelBuilder.Entity<Periodicnost>(entity =>
        {
            entity.HasKey(e => e.PeriodIzd).HasName("PER_PK");

            entity.ToTable("PERIODICNOST");

            entity.Property(e => e.PeriodIzd)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Radnik>(entity =>
        {
            entity.HasKey(e => e.IDRadnik).HasName("RAD_PK");

            entity.ToTable("RADNIK");

            entity.Property(e => e.IDRadnik)
                .ValueGeneratedNever()
                .HasColumnName("IDRadnik");
            entity.Property(e => e.DatRodj)
                .HasDefaultValueSql("('1970-1-1')")
                .HasColumnType("date");
            entity.Property(e => e.DatZap)
                .HasDefaultValueSql("('2023-1-1')")
                .HasColumnType("date");
            entity.Property(e => e.IDBK).HasColumnName("IDBK");
            entity.Property(e => e.Ime)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.KorisnickoIme)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Prezime)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Sifra).IsUnicode(false);

            entity.HasOne(d => d.IDBKNavigation).WithMany(p => p.Radniks)
                .HasForeignKey(d => d.IDBK)
                .HasConstraintName("RAD_FK");
        });

        modelBuilder.Entity<Rezervacija>(entity =>
        {
            entity.HasKey(e => new { e.IDRez, e.IDKnjiga, e.IDClan, e.IDBK }).HasName("REZ_PK");

            entity.ToTable("REZERVACIJA");

            entity.Property(e => e.IDRez).HasColumnName("IDRez");
            entity.Property(e => e.IDKnjiga).HasColumnName("IDKnjiga");
            entity.Property(e => e.IDClan).HasColumnName("IDClan");
            entity.Property(e => e.IDBK).HasColumnName("IDBK");
            entity.Property(e => e.DatVr).HasColumnType("datetime");
            entity.Property(e => e.DatVrPot).HasColumnType("datetime");
            entity.Property(e => e.DatVrZak).HasColumnType("datetime");

            entity.HasOne(d => d.IDClanNavigation).WithMany(p => p.Rezervacijas)
                .HasForeignKey(d => d.IDClan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("REZ_FK2");

            entity.HasOne(d => d.PotvrdioRezNavigation).WithMany(p => p.RezervacijaPotvrdioRezNavigations)
                .HasForeignKey(d => d.PotvrdioRez)
                .HasConstraintName("REZ_FK3");

            entity.HasOne(d => d.ZakljucioRezNavigation).WithMany(p => p.RezervacijaZakljucioRezNavigations)
                .HasForeignKey(d => d.ZakljucioRez)
                .HasConstraintName("REZ_FK4");

            entity.HasOne(d => d.IDKULNavigation).WithMany(p => p.Rezervacijas)
                .HasForeignKey(d => new { d.IDKnjiga, d.IDBK })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("REZ_FK1");
        });

        modelBuilder.Entity<SerijskoStivo>(entity =>
        {
            entity.HasKey(e => e.IDSStivo).HasName("SS_PK");

            entity.ToTable("SERIJSKOSTIVO");

            entity.Property(e => e.IDSStivo)
                .ValueGeneratedNever()
                .HasColumnName("IDSStivo");
            entity.Property(e => e.DatIzd).HasColumnType("date");
            entity.Property(e => e.Naziv).IsUnicode(false);
            entity.Property(e => e.PeriodIzd)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.PeriodIzdNavigation).WithMany(p => p.SerijskoStivos)
                .HasForeignKey(d => d.PeriodIzd)
                .HasConstraintName("SS_FK");

            entity.HasMany(d => d.IDIKs).WithMany(p => p.IDSStivos)
                .UsingEntity<Dictionary<string, object>>(
                    "Izdajesstivo",
                    r => r.HasOne<IzdKuca>().WithMany()
                        .HasForeignKey("Idik")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("IZDS_FK2"),
                    l => l.HasOne<SerijskoStivo>().WithMany()
                        .HasForeignKey("Idsstivo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("IZDS_FK1"),
                    j =>
                    {
                        j.HasKey("Idsstivo", "Idik").HasName("IZDS_PK");
                        j.ToTable("IZDAJESSTIVO");
                        j.IndexerProperty<int>("Idsstivo").HasColumnName("IDSStivo");
                        j.IndexerProperty<int>("Idik").HasColumnName("IDIK");
                    });

            entity.HasMany(d => d.OZNJs).WithMany(p => p.IDSStivos)
                .UsingEntity<Dictionary<string, object>>(
                    "Sstivonajeziku",
                    r => r.HasOne<Jezik>().WithMany()
                        .HasForeignKey("Oznj")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("SSNJ_FK2"),
                    l => l.HasOne<SerijskoStivo>().WithMany()
                        .HasForeignKey("Idsstivo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("SSNJ_FK1"),
                    j =>
                    {
                        j.HasKey("Idsstivo", "Oznj").HasName("SSNJ_PK");
                        j.ToTable("SSTIVONAJEZIKU");
                        j.IndexerProperty<int>("Idsstivo").HasColumnName("IDSStivo");
                        j.IndexerProperty<string>("Oznj")
                            .HasMaxLength(4)
                            .IsUnicode(false)
                            .HasColumnName("OZNJ");
                    });
        });

        modelBuilder.Entity<SerijskoStivoULokalu>(entity =>
        {
            entity.HasKey(e => new { e.IDSStivo, e.IDBK }).HasName("SUL_PK");

            entity.ToTable("SERIJSKOSTIVOULOKALU");

            entity.Property(e => e.IDSStivo).HasColumnName("IDSStivo");
            entity.Property(e => e.IDBK).HasColumnName("IDBK");

            entity.HasOne(d => d.IDBKNavigation).WithMany(p => p.SerijskoStivoULokalus)
                .HasForeignKey(d => d.IDBK)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUL_FK2");

            entity.HasOne(d => d.IDSStivoNavigation).WithMany(p => p.SerijskoStivoULokalus)
                .HasForeignKey(d => d.IDSStivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUL_FK1");
        });

        modelBuilder.Entity<ZahtevZaKnjigu>(entity =>
        {
            entity.HasKey(e => new { e.Knjiga, e.Autor, e.Jezik, e.IDClan, e.IDBK }).HasName("ZZK_PK");

            entity.ToTable("ZAHTEVZAKNJIGU");

            entity.Property(e => e.Knjiga)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Autor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Jezik)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.IDClan).HasColumnName("IDClan");
            entity.Property(e => e.IDBK).HasColumnName("IDBK");
            entity.Property(e => e.DatVr).HasColumnType("datetime");

            entity.HasOne(d => d.IDBKNavigation).WithMany(p => p.ZahtevZaKnjigus)
                .HasForeignKey(d => d.IDBK)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ZZK_FK2");

            entity.HasOne(d => d.IDClanNavigation).WithMany(p => p.ZahtevZaKnjigus)
                .HasForeignKey(d => d.IDClan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ZZK_FK1");
        });

        modelBuilder.Entity<ZahtevZaSerijskoStivo>(entity =>
        {
            entity.HasKey(e => new { e.Naziv, e.Jezik, e.Tip, e.IDClan, e.IDBK }).HasName("ZZS_PK");

            entity.ToTable("ZAHTEVZASERIJSKOSTIVO");

            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Jezik)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.IDClan).HasColumnName("IDClan");
            entity.Property(e => e.IDBK).HasColumnName("IDBK");
            entity.Property(e => e.DatVr).HasColumnType("datetime");

            entity.HasOne(d => d.IDBKNavigation).WithMany(p => p.ZahtevZaSerijskoStivos)
                .HasForeignKey(d => d.IDBK)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ZZS_FK2");

            entity.HasOne(d => d.IDClanNavigation).WithMany(p => p.ZahtevZaSerijskoStivos)
                .HasForeignKey(d => d.IDClan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ZZS_FK1");
        });

        modelBuilder.Entity<Zanr>(entity =>
        {
            entity.HasKey(e => e.OZNZ).HasName("ZANR_PK");

            entity.ToTable("ZANR");

            entity.Property(e => e.OZNZ)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("OZNZ");
            entity.Property(e => e.NazivZanra)
                .IsUnicode(false)
                .HasColumnName("Zanr");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
