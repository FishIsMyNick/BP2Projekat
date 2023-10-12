﻿using System;
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

    public virtual DbSet<Ispunjenzahtevknjiga> Ispunjenzahtevknjigas { get; set; }

    public virtual DbSet<Ispunjenzahtevserijskostivo> Ispunjenzahtevserijskostivos { get; set; }

    public virtual DbSet<Istorijaclanarina> Istorijaclanarinas { get; set; }

    public virtual DbSet<Izdajeknjigu> Izdajeknjigus { get; set; }

    public virtual DbSet<Izdkuca> Izdkucas { get; set; }

    public virtual DbSet<Izmenakreditum> Izmenakredita { get; set; }

    public virtual DbSet<Izmenalokacije> Izmenalokacijes { get; set; }

    public virtual DbSet<Izmenapodataka> Izmenapodatakas { get; set; }

    public virtual DbSet<Izmenasifre> Izmenasifres { get; set; }

    public virtual DbSet<Izmenastatusa> Izmenastatusas { get; set; }

    public virtual DbSet<Jezik> Jeziks { get; set; }

    public virtual DbSet<Knjiganajeziku> Knjiganajezikus { get; set; }

    public virtual DbSet<Knjigaulokalu> Knjigaulokalus { get; set; }

    public virtual DbSet<Kupovina> Kupovinas { get; set; }

    public virtual DbSet<Lokacija> Lokacijas { get; set; }

    public virtual DbSet<Mesto> Mestos { get; set; }

    public virtual DbSet<Ocenaknjige> Ocenaknjiges { get; set; }

    public virtual DbSet<Ocenasstiva> Ocenasstivas { get; set; }

    public virtual DbSet<Periodicnost> Periodicnosts { get; set; }

    public virtual DbSet<Pise> Pises { get; set; }

    public virtual DbSet<Pripadazanru> Pripadazanrus { get; set; }

    public virtual DbSet<Radnik> Radniks { get; set; }

    public virtual DbSet<Rezervacija> Rezervacijas { get; set; }

    public virtual DbSet<Serijskostivo> Serijskostivos { get; set; }

    public virtual DbSet<Serijskostivoulokalu> Serijskostivoulokalus { get; set; }

    public virtual DbSet<Zahtevzaknjigu> Zahtevzaknjigus { get; set; }

    public virtual DbSet<Zahtevzaserijskostivo> Zahtevzaserijskostivos { get; set; }

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
            entity.HasKey(e => e.Idautor).HasName("AUT_PK");

            entity.ToTable("AUTOR");

            entity.Property(e => e.Idautor)
                .ValueGeneratedNever()
                .HasColumnName("IDAutor");
            entity.Property(e => e.Biografija).IsUnicode(false);
            entity.Property(e => e.DatRodj).HasColumnType("date");
            entity.Property(e => e.Ime)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Oznd)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZND");
            entity.Property(e => e.Prezime)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.OzndNavigation).WithMany(p => p.Autors)
                .HasForeignKey(d => d.Oznd)
                .HasConstraintName("AUT_FK");
        });

        modelBuilder.Entity<Biblikutak>(entity =>
        {
            entity.HasKey(e => e.Idbk).HasName("BIB_PK");

            entity.ToTable("BIBLIKUTAK");

            entity.Property(e => e.Idbk)
                .ValueGeneratedNever()
                .HasColumnName("IDBK");
            entity.Property(e => e.Broj)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Oznd)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZND");
            entity.Property(e => e.Ulica)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Lokacija).WithMany(p => p.Biblikutaks)
                .HasForeignKey(d => new { d.Ulica, d.Broj, d.PosBr, d.Oznd })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BIB_FK");
        });

        modelBuilder.Entity<Clan>(entity =>
        {
            entity.HasKey(e => e.Idclan).HasName("CL_PK");

            entity.ToTable("CLAN");

            entity.Property(e => e.Idclan)
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
            entity.Property(e => e.Oznd)
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
                .HasForeignKey(d => new { d.Ulica, d.Broj, d.PosBr, d.Oznd })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CL_FK");
        });

        modelBuilder.Entity<Clanarina>(entity =>
        {
            entity.HasKey(e => new { e.Oznc, e.DatUvoda }).HasName("CLA_PK");

            entity.ToTable("CLANARINA");

            entity.Property(e => e.Oznc)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZNC");
            entity.Property(e => e.DatUvoda).HasColumnType("date");
            entity.Property(e => e.NazivClanarine)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Drzava>(entity =>
        {
            entity.HasKey(e => e.Oznd).HasName("OZND_PK");

            entity.ToTable("DRZAVA");

            entity.Property(e => e.Oznd)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZND");
            entity.Property(e => e.NazivDrzave)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ispunjenzahtevknjiga>(entity =>
        {
            entity.HasKey(e => new { e.Idclan, e.Idknjiga, e.Idbk }).HasName("IZK_PK");

            entity.ToTable("ISPUNJENZAHTEVKNJIGA");

            entity.Property(e => e.Idclan).HasColumnName("IDClan");
            entity.Property(e => e.Idknjiga).HasColumnName("IDKnjiga");
            entity.Property(e => e.Idbk).HasColumnName("IDBK");
            entity.Property(e => e.DatVrIsp).HasColumnType("datetime");

            entity.HasOne(d => d.IdclanNavigation).WithMany(p => p.Ispunjenzahtevknjigas)
                .HasForeignKey(d => d.Idclan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZK_FK1");

            entity.HasOne(d => d.Id).WithMany(p => p.Ispunjenzahtevknjigas)
                .HasForeignKey(d => new { d.Idknjiga, d.Idbk })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZK_FK2");
        });

        modelBuilder.Entity<Ispunjenzahtevserijskostivo>(entity =>
        {
            entity.HasKey(e => new { e.Idclan, e.Idsstivo, e.Idbk }).HasName("IZS_PK");

            entity.ToTable("ISPUNJENZAHTEVSERIJSKOSTIVO");

            entity.Property(e => e.Idclan).HasColumnName("IDClan");
            entity.Property(e => e.Idsstivo).HasColumnName("IDSStivo");
            entity.Property(e => e.Idbk).HasColumnName("IDBK");
            entity.Property(e => e.DatVrIsp).HasColumnType("datetime");

            entity.HasOne(d => d.IdclanNavigation).WithMany(p => p.Ispunjenzahtevserijskostivos)
                .HasForeignKey(d => d.Idclan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZS_FK1");

            entity.HasOne(d => d.Id).WithMany(p => p.Ispunjenzahtevserijskostivos)
                .HasForeignKey(d => new { d.Idsstivo, d.Idbk })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZS_FK2");
        });

        modelBuilder.Entity<Istorijaclanarina>(entity =>
        {
            entity.HasKey(e => new { e.Idcl, e.Idclan, e.Oznc, e.DatUvoda }).HasName("ICL_PK");

            entity.ToTable("ISTORIJACLANARINA");

            entity.Property(e => e.Idcl).HasColumnName("IDCL");
            entity.Property(e => e.Idclan).HasColumnName("IDClan");
            entity.Property(e => e.Oznc)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZNC");
            entity.Property(e => e.DatUvoda).HasColumnType("date");
            entity.Property(e => e.DatVrStart).HasColumnType("datetime");
            entity.Property(e => e.DatVrUplate).HasColumnType("datetime");

            entity.HasOne(d => d.IdclanNavigation).WithMany(p => p.Istorijaclanarinas)
                .HasForeignKey(d => d.Idclan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ICL_FK1");

            entity.HasOne(d => d.Clanarina).WithMany(p => p.Istorijaclanarinas)
                .HasForeignKey(d => new { d.Oznc, d.DatUvoda })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ICL_FK2");
        });

        modelBuilder.Entity<Izdajeknjigu>(entity =>
        {
            entity.HasKey(e => new { e.Idknjiga, e.Idik }).HasName("IKK_PK");

            entity.ToTable("IZDAJEKNJIGU");

            entity.Property(e => e.Idknjiga).HasColumnName("IDKnjiga");
            entity.Property(e => e.Idik).HasColumnName("IDIK");

            entity.HasOne(d => d.IdikNavigation).WithMany(p => p.Izdajeknjigus)
                .HasForeignKey(d => d.Idik)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IKK_FK2");
        });

        modelBuilder.Entity<Izdkuca>(entity =>
        {
            entity.HasKey(e => e.Idik).HasName("IK_PK");

            entity.ToTable("IZDKUCA");

            entity.Property(e => e.Idik)
                .ValueGeneratedNever()
                .HasColumnName("IDIK");
            entity.Property(e => e.Broj)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Oznd)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZND");
            entity.Property(e => e.Ulica)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Lokacija).WithMany(p => p.Izdkucas)
                .HasForeignKey(d => new { d.Ulica, d.Broj, d.PosBr, d.Oznd })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IK_FK");
        });

        modelBuilder.Entity<Izmenakreditum>(entity =>
        {
            entity.HasKey(e => new { e.Idkredit, e.Idclan, e.Idbk }).HasName("IKR_PK");

            entity.ToTable("IZMENAKREDITA");

            entity.Property(e => e.Idkredit).HasColumnName("IDKredit");
            entity.Property(e => e.Idclan).HasColumnName("IDClan");
            entity.Property(e => e.Idbk).HasColumnName("IDBK");
            entity.Property(e => e.DatVr).HasColumnType("datetime");

            entity.HasOne(d => d.IdbkNavigation).WithMany(p => p.Izmenakredita)
                .HasForeignKey(d => d.Idbk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IKR_FK2");

            entity.HasOne(d => d.IdclanNavigation).WithMany(p => p.Izmenakredita)
                .HasForeignKey(d => d.Idclan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IKR_FK1");
        });

        modelBuilder.Entity<Izmenalokacije>(entity =>
        {
            entity.HasKey(e => new { e.Idil, e.Idclan, e.Ulica, e.Broj, e.PosBr, e.Oznd }).HasName("IZL_PK");

            entity.ToTable("IZMENALOKACIJE");

            entity.Property(e => e.Idil).HasColumnName("IDIL");
            entity.Property(e => e.Idclan).HasColumnName("IDClan");
            entity.Property(e => e.Ulica)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Broj)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Oznd)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZND");
            entity.Property(e => e.DatVr).HasColumnType("datetime");

            entity.HasOne(d => d.IdclanNavigation).WithMany(p => p.Izmenalokacijes)
                .HasForeignKey(d => d.Idclan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZL_FK1");

            entity.HasOne(d => d.Lokacija).WithMany(p => p.Izmenalokacijes)
                .HasForeignKey(d => new { d.Ulica, d.Broj, d.PosBr, d.Oznd })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZL_FK2");
        });

        modelBuilder.Entity<Izmenapodataka>(entity =>
        {
            entity.HasKey(e => new { e.Idpodatak, e.Idclan }).HasName("IZP_PK");

            entity.ToTable("IZMENAPODATAKA");

            entity.Property(e => e.Idpodatak).HasColumnName("IDPodatak");
            entity.Property(e => e.Idclan).HasColumnName("IDClan");
            entity.Property(e => e.DatVr).HasColumnType("datetime");
            entity.Property(e => e.Ime)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Prezime)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdclanNavigation).WithMany(p => p.Izmenapodatakas)
                .HasForeignKey(d => d.Idclan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZP_FK");
        });

        modelBuilder.Entity<Izmenasifre>(entity =>
        {
            entity.HasKey(e => new { e.Idsifra, e.Idclan }).HasName("IZSI_PK");

            entity.ToTable("IZMENASIFRE");

            entity.Property(e => e.Idsifra).HasColumnName("IDSifra");
            entity.Property(e => e.Idclan).HasColumnName("IDClan");
            entity.Property(e => e.DatVr).HasColumnType("datetime");
            entity.Property(e => e.Sifra).IsUnicode(false);

            entity.HasOne(d => d.IdclanNavigation).WithMany(p => p.Izmenasifres)
                .HasForeignKey(d => d.Idclan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZSI_FK");
        });

        modelBuilder.Entity<Izmenastatusa>(entity =>
        {
            entity.HasKey(e => new { e.Idstatus, e.Idclan }).HasName("IZST_PK");

            entity.ToTable("IZMENASTATUSA");

            entity.Property(e => e.Idstatus).HasColumnName("IDStatus");
            entity.Property(e => e.Idclan).HasColumnName("IDClan");
            entity.Property(e => e.DatVr).HasColumnType("datetime");

            entity.HasOne(d => d.IdclanNavigation).WithMany(p => p.Izmenastatusas)
                .HasForeignKey(d => d.Idclan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IZST_FK");
        });

        modelBuilder.Entity<Jezik>(entity =>
        {
            entity.HasKey(e => e.Oznj).HasName("JEZ_PK");

            entity.ToTable("JEZIK");

            entity.Property(e => e.Oznj)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("OZNJ");
            entity.Property(e => e.NazivJezika).IsUnicode(false);
        });

        modelBuilder.Entity<Knjiganajeziku>(entity =>
        {
            entity.HasKey(e => new { e.Idknjiga, e.Oznj }).HasName("KJZ_PK");

            entity.ToTable("KNJIGANAJEZIKU");

            entity.Property(e => e.Idknjiga).HasColumnName("IDKnjiga");
            entity.Property(e => e.Oznj)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("OZNJ");

            entity.HasOne(d => d.OznjNavigation).WithMany(p => p.Knjiganajezikus)
                .HasForeignKey(d => d.Oznj)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("KJZ_FK2");
        });

        modelBuilder.Entity<Knjigaulokalu>(entity =>
        {
            entity.HasKey(e => new { e.Idknjiga, e.Idbk }).HasName("KUL_PK");

            entity.ToTable("KNJIGAULOKALU");

            entity.Property(e => e.Idknjiga).HasColumnName("IDKnjiga");
            entity.Property(e => e.Idbk).HasColumnName("IDBK");

            entity.HasOne(d => d.IdbkNavigation).WithMany(p => p.Knjigaulokalus)
                .HasForeignKey(d => d.Idbk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("KUL_FK2");
        });

        modelBuilder.Entity<Kupovina>(entity =>
        {
            entity.HasKey(e => new { e.Idkup, e.Idsstivo, e.Idclan, e.Idbk }).HasName("KUP_PK");

            entity.ToTable("KUPOVINA");

            entity.Property(e => e.Idkup).HasColumnName("IDKup");
            entity.Property(e => e.Idsstivo).HasColumnName("IDSStivo");
            entity.Property(e => e.Idclan).HasColumnName("IDClan");
            entity.Property(e => e.Idbk).HasColumnName("IDBK");
            entity.Property(e => e.DatVr).HasColumnType("datetime");
            entity.Property(e => e.DatVrPot).HasColumnType("datetime");

            entity.HasOne(d => d.IdclanNavigation).WithMany(p => p.Kupovinas)
                .HasForeignKey(d => d.Idclan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("KUP_FK2");

            entity.HasOne(d => d.PotvrdioKupNavigation).WithMany(p => p.Kupovinas)
                .HasForeignKey(d => d.PotvrdioKup)
                .HasConstraintName("KUP_FK3");

            entity.HasOne(d => d.Id).WithMany(p => p.Kupovinas)
                .HasForeignKey(d => new { d.Idsstivo, d.Idbk })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("KUP_FK1");
        });

        modelBuilder.Entity<Lokacija>(entity =>
        {
            entity.HasKey(e => new { e.Ulica, e.Broj, e.PosBr, e.Oznd }).HasName("LOK_PK");

            entity.ToTable("LOKACIJA");

            entity.Property(e => e.Ulica)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Broj)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Oznd)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("OZND");

            entity.HasOne(d => d.OzndNavigation).WithMany(p => p.Lokacijas)
                .HasForeignKey(d => d.Oznd)
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
                .IsUnicode(false);

            entity.HasMany(d => d.Oznds).WithMany(p => p.PosBrs)
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

        modelBuilder.Entity<Ocenaknjige>(entity =>
        {
            entity.HasKey(e => new { e.IdocenaK, e.Idclan, e.Idknjiga }).HasName("OCK_PK");

            entity.ToTable("OCENAKNJIGE");

            entity.Property(e => e.IdocenaK).HasColumnName("IDOcenaK");
            entity.Property(e => e.Idclan).HasColumnName("IDClan");
            entity.Property(e => e.Idknjiga).HasColumnName("IDKnjiga");
            entity.Property(e => e.DatVr).HasColumnType("datetime");
            entity.Property(e => e.Komentar).IsUnicode(false);

            entity.HasOne(d => d.IdclanNavigation).WithMany(p => p.Ocenaknjiges)
                .HasForeignKey(d => d.Idclan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OCK_FK1");
        });

        modelBuilder.Entity<Ocenasstiva>(entity =>
        {
            entity.HasKey(e => new { e.IdocenaS, e.Idclan, e.Idsstivo }).HasName("OCS_PK");

            entity.ToTable("OCENASSTIVA");

            entity.Property(e => e.IdocenaS).HasColumnName("IDOcenaS");
            entity.Property(e => e.Idclan).HasColumnName("IDClan");
            entity.Property(e => e.Idsstivo).HasColumnName("IDSStivo");
            entity.Property(e => e.DatVr).HasColumnType("datetime");
            entity.Property(e => e.Komentar).IsUnicode(false);

            entity.HasOne(d => d.IdclanNavigation).WithMany(p => p.Ocenasstivas)
                .HasForeignKey(d => d.Idclan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OCS_FK1");

            entity.HasOne(d => d.IdsstivoNavigation).WithMany(p => p.Ocenasstivas)
                .HasForeignKey(d => d.Idsstivo)
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

        modelBuilder.Entity<Pise>(entity =>
        {
            entity.HasKey(e => new { e.Idknjiga, e.Idautor }).HasName("PIS_PK");

            entity.ToTable("PISE");

            entity.Property(e => e.Idknjiga).HasColumnName("IDKnjiga");
            entity.Property(e => e.Idautor).HasColumnName("IDAutor");

            entity.HasOne(d => d.IdautorNavigation).WithMany(p => p.Pises)
                .HasForeignKey(d => d.Idautor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PIS_FK2");
        });

        modelBuilder.Entity<Pripadazanru>(entity =>
        {
            entity.HasKey(e => new { e.Idknjiga, e.Oznz }).HasName("KPZ_PK");

            entity.ToTable("PRIPADAZANRU");

            entity.Property(e => e.Idknjiga).HasColumnName("IDKnjiga");
            entity.Property(e => e.Oznz)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("OZNZ");

            entity.HasOne(d => d.OznzNavigation).WithMany(p => p.Pripadazanrus)
                .HasForeignKey(d => d.Oznz)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("KPZ_FK2");
        });

        modelBuilder.Entity<Radnik>(entity =>
        {
            entity.HasKey(e => e.Idradnik).HasName("RAD_PK");

            entity.ToTable("RADNIK");

            entity.Property(e => e.Idradnik)
                .ValueGeneratedNever()
                .HasColumnName("IDRadnik");
            entity.Property(e => e.DatRodj)
                .HasDefaultValueSql("('1970-1-1')")
                .HasColumnType("date");
            entity.Property(e => e.DatZap)
                .HasDefaultValueSql("('2023-1-1')")
                .HasColumnType("date");
            entity.Property(e => e.Idbk).HasColumnName("IDBK");
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

            entity.HasOne(d => d.IdbkNavigation).WithMany(p => p.Radniks)
                .HasForeignKey(d => d.Idbk)
                .HasConstraintName("RAD_FK");
        });

        modelBuilder.Entity<Rezervacija>(entity =>
        {
            entity.HasKey(e => new { e.Idrez, e.Idknjiga, e.Idclan, e.Idbk }).HasName("REZ_PK");

            entity.ToTable("REZERVACIJA");

            entity.Property(e => e.Idrez).HasColumnName("IDRez");
            entity.Property(e => e.Idknjiga).HasColumnName("IDKnjiga");
            entity.Property(e => e.Idclan).HasColumnName("IDClan");
            entity.Property(e => e.Idbk).HasColumnName("IDBK");
            entity.Property(e => e.DatVr).HasColumnType("datetime");
            entity.Property(e => e.DatVrPot).HasColumnType("datetime");
            entity.Property(e => e.DatVrZak).HasColumnType("datetime");

            entity.HasOne(d => d.IdclanNavigation).WithMany(p => p.Rezervacijas)
                .HasForeignKey(d => d.Idclan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("REZ_FK2");

            entity.HasOne(d => d.PotvrdioRezNavigation).WithMany(p => p.RezervacijaPotvrdioRezNavigations)
                .HasForeignKey(d => d.PotvrdioRez)
                .HasConstraintName("REZ_FK3");

            entity.HasOne(d => d.ZakljucioRezNavigation).WithMany(p => p.RezervacijaZakljucioRezNavigations)
                .HasForeignKey(d => d.ZakljucioRez)
                .HasConstraintName("REZ_FK4");

            entity.HasOne(d => d.Id).WithMany(p => p.Rezervacijas)
                .HasForeignKey(d => new { d.Idknjiga, d.Idbk })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("REZ_FK1");
        });

        modelBuilder.Entity<Serijskostivo>(entity =>
        {
            entity.HasKey(e => e.Idsstivo).HasName("SS_PK");

            entity.ToTable("SERIJSKOSTIVO");

            entity.Property(e => e.Idsstivo)
                .ValueGeneratedNever()
                .HasColumnName("IDSStivo");
            entity.Property(e => e.DatIzd).HasColumnType("date");
            entity.Property(e => e.Naziv).IsUnicode(false);
            entity.Property(e => e.PeriodIzd)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.PeriodIzdNavigation).WithMany(p => p.Serijskostivos)
                .HasForeignKey(d => d.PeriodIzd)
                .HasConstraintName("SS_FK");

            entity.HasMany(d => d.Idiks).WithMany(p => p.Idsstivos)
                .UsingEntity<Dictionary<string, object>>(
                    "Izdajesstivo",
                    r => r.HasOne<Izdkuca>().WithMany()
                        .HasForeignKey("Idik")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("IZDS_FK2"),
                    l => l.HasOne<Serijskostivo>().WithMany()
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

            entity.HasMany(d => d.Oznjs).WithMany(p => p.Idsstivos)
                .UsingEntity<Dictionary<string, object>>(
                    "Sstivonajeziku",
                    r => r.HasOne<Jezik>().WithMany()
                        .HasForeignKey("Oznj")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("SSNJ_FK2"),
                    l => l.HasOne<Serijskostivo>().WithMany()
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

        modelBuilder.Entity<Serijskostivoulokalu>(entity =>
        {
            entity.HasKey(e => new { e.Idsstivo, e.Idbk }).HasName("SUL_PK");

            entity.ToTable("SERIJSKOSTIVOULOKALU");

            entity.Property(e => e.Idsstivo).HasColumnName("IDSStivo");
            entity.Property(e => e.Idbk).HasColumnName("IDBK");

            entity.HasOne(d => d.IdbkNavigation).WithMany(p => p.Serijskostivoulokalus)
                .HasForeignKey(d => d.Idbk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUL_FK2");

            entity.HasOne(d => d.IdsstivoNavigation).WithMany(p => p.Serijskostivoulokalus)
                .HasForeignKey(d => d.Idsstivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUL_FK1");
        });

        modelBuilder.Entity<Zahtevzaknjigu>(entity =>
        {
            entity.HasKey(e => new { e.Knjiga, e.Autor, e.Jezik, e.Idclan, e.Idbk }).HasName("ZZK_PK");

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
            entity.Property(e => e.Idclan).HasColumnName("IDClan");
            entity.Property(e => e.Idbk).HasColumnName("IDBK");
            entity.Property(e => e.DatVr).HasColumnType("datetime");

            entity.HasOne(d => d.IdbkNavigation).WithMany(p => p.Zahtevzaknjigus)
                .HasForeignKey(d => d.Idbk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ZZK_FK2");

            entity.HasOne(d => d.IdclanNavigation).WithMany(p => p.Zahtevzaknjigus)
                .HasForeignKey(d => d.Idclan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ZZK_FK1");
        });

        modelBuilder.Entity<Zahtevzaserijskostivo>(entity =>
        {
            entity.HasKey(e => new { e.Naziv, e.Jezik, e.Tip, e.Idclan, e.Idbk }).HasName("ZZS_PK");

            entity.ToTable("ZAHTEVZASERIJSKOSTIVO");

            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Jezik)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Idclan).HasColumnName("IDClan");
            entity.Property(e => e.Idbk).HasColumnName("IDBK");
            entity.Property(e => e.DatVr).HasColumnType("datetime");

            entity.HasOne(d => d.IdbkNavigation).WithMany(p => p.Zahtevzaserijskostivos)
                .HasForeignKey(d => d.Idbk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ZZS_FK2");

            entity.HasOne(d => d.IdclanNavigation).WithMany(p => p.Zahtevzaserijskostivos)
                .HasForeignKey(d => d.Idclan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ZZS_FK1");
        });

        modelBuilder.Entity<Zanr>(entity =>
        {
            entity.HasKey(e => e.Oznz).HasName("ZANR_PK");

            entity.ToTable("ZANR");

            entity.Property(e => e.Oznz)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("OZNZ");
            entity.Property(e => e.NazivZanra).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
