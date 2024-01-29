create table Mesto(
	PosBr int not null,
	NazivMesta varchar(MAX) not null,

	constraint MESTO_PK primary key (PosBr)
	);

create table Drzava(
	OZND varchar(4) not null,
	NazivDrzave varchar(MAX) not null,

	constraint DRZAVA_PK primary key (OZND)
	);

create table Adresa(
	Ulica varchar(200) not null,
	Broj varchar(50) not null,

	constraint ADRESA_PK primary key (Ulica, Broj)
	);

create table MestoUDrzavi(
	PosBr int not null,
	OZND varchar(4) not null,

	constraint MUD_PK primary key (PosBr, OZND),

	constraint MUD_FK1 foreign key (PosBr) references Mesto(PosBr),	
	constraint MUD_FK2 foreign key (OZND) references Drzava(OZND)
	);

create table Lokacija(
	Ulica varchar(200) not null,
	Broj varchar(50) not null,
	PosBr int not null,
	OZND varchar(4) not null,
	
	constraint LOKACIJA_PK primary key (Ulica, Broj, PosBr, OZND),

	constraint LOKACIJA_FK1 foreign key (Ulica, Broj) references Adresa(Ulica, Broj),	
	constraint LOKACIJA_FK2 foreign key (PosBr, OZND) references MestoUDrzavi(PosBr, OZND)
	);

/* POSLOVNI ENTITETI */
create table IzdKuca(
	IDIK int IDENTITY not null,
	Naziv varchar(MAX) not null,
	Ulica varchar(200) not null,
	Broj varchar(50) not null,
	PosBr int not null,
	OZND varchar(4) not null,
	DatZat date,

	constraint IK_PK primary key (IDIK),

	constraint IK_FK foreign key (Ulica, Broj, PosBr, OZND) references Lokacija(Ulica, Broj, PosBr, OZND)
	);
	
create table Biblikutak(
	IDBK int IDENTITY not null,
	Naziv varchar(MAX) not null,
	DatOtv date not null,
	DatZat date,
	Ulica varchar(200) not null,
	Broj varchar(50) not null,
	PosBr int not null,
	OZND varchar(4) not null,

	
	constraint BK_PK primary key (IDBK),

	constraint BK_FK foreign key (Ulica, Broj, PosBr, OZND) references Lokacija(Ulica, Broj, PosBr, OZND)
	);

create table KorisnickiNalog(
	KorisnickoIme varchar(50) not null,
	Sifra varchar(MAX) not null,
	DatKreiranja date not null,
	DatZatvaranja date,
	TipNaloga int not null,

	constraint NALOG_PK primary key (KorisnickoIme)
	);

create table Admin(
	IDRadnik int IDENTITY not null,
	Ime varchar(50) not null,
	Prezime varchar(50) not null,
	DatRodj date not null,
	DatZap date not null,
	DatOtp date,
	Ulica varchar(200),
	Broj varchar(50),
	PosBr int,
	OZND varchar(4),
	
	constraint ADMIN_PK primary key (IDRadnik),
	constraint ADMIN_FK foreign key (Ulica, Broj, PosBr, OZND) references Lokacija(Ulica, Broj, PosBr, OZND)
	);

create table Kurir(
	IDRadnik int IDENTITY not null,
	Ime varchar(50) not null,
	Prezime varchar(50) not null,
	DatRodj date not null,
	DatZap date not null,
	DatOtp date,
	IDAdmin int not null,
	DatVr datetime not null,
	Ulica varchar(200) not null,
	Broj varchar(50) not null,
	PosBr int not null,
	OZND varchar(4) not null,
	   
	constraint KURIR_PK primary key (IDRadnik),

	constraint KURIR_FK1 foreign key (IDAdmin) references Admin(IDRadnik),
	constraint KURIR_FK2 foreign key (Ulica, Broj, PosBr, OZND) references Lokacija(Ulica, Broj, PosBr, OZND)
	);

create table Bibliotekar(
	IDRadnik int IDENTITY not null,
	Ime varchar(50) not null,
	Prezime varchar(50) not null,
	DatRodj date not null,
	DatZap date not null,
	DatOtp date,
	IDAdmin int not null,
	DatVr datetime not null,
	Ulica varchar(200) not null,
	Broj varchar(50) not null,
	PosBr int not null,
	OZND varchar(4) not null,
	   
	constraint BIB_PK primary key (IDRadnik),

	constraint BIB_FK1 foreign key (IDAdmin) references Admin(IDRadnik),
	constraint BIB_FK2 foreign key (Ulica, Broj, PosBr, OZND) references Lokacija(Ulica, Broj, PosBr, OZND)
	);

create table AdminKoristiNalog(
	ID int not null,
	KorisnickoIme varchar(50) not null,

	constraint AKN_PK primary key (ID, KorisnickoIme),

	constraint AKN_FK1 foreign key (ID) references Admin(IDRadnik),	
	constraint AKN_FK2 foreign key (KorisnickoIme) references KorisnickiNalog(KorisnickoIme)
	);
	
create table BibliotekarKoristiNalog(
	ID int not null,
	KorisnickoIme varchar(50) not null,

	constraint BKN_PK primary key (ID, KorisnickoIme),

	constraint BKN_FK1 foreign key (ID) references Bibliotekar(IDRadnik),	
	constraint BKN_FK2 foreign key (KorisnickoIme) references KorisnickiNalog(KorisnickoIme)
	);
	
create table KurirKoristiNalog(
	ID int not null,
	KorisnickoIme varchar(50) not null,

	constraint KKN_PK primary key (ID, KorisnickoIme),

	constraint KKN_FK1 foreign key (ID) references Kurir(IDRadnik),	
	constraint KKN_FK2 foreign key (KorisnickoIme) references KorisnickiNalog(KorisnickoIme)
	);

/* KNJIGE */
create table Jezik(
	OZNJ varchar(4) not null,
	NazivJezika varchar(MAX) not null,

	constraint JEZIK_PK primary key (OZNJ)
	);
	
create table Zanr(
	OZNZ varchar(4) not null,
	NazivZanra varchar(MAX) not null,

	constraint ZANR_PK primary key (OZNZ)
	);
	
create table Format(
	NazivFormata varchar(50) not null,

	constraint FORMAT_PK primary key (NazivFormata)
	);
	
create table Autor(
	IDAutor int IDENTITY not null,
	Ime varchar(50) not null,
	Prezime varchar(50),
	DatRodj date,
	Biografija varchar(MAX),
	Drzava varchar(4),
	DatBrisanja date,

	constraint AUTOR_PK primary key (IDAutor),

	constraint AUTOR_FK foreign key (Drzava) references Drzava(OZND)
	);

create table Knjiga(
	IDKnjiga int IDENTITY not null,
	Naziv varchar(MAX) not null,
	BrIzd int not null,
	GodIzd int, 
	VrIzd varchar(MAX),
	BrStrana int,
	VelicinaFonta int,
	Korice int,
	Ograniceno int,
	Format varchar(50),
	DatBrisanja date,

	constraint KNJIGA_PK primary key (IDKnjiga),

	constraint KNJIGA_FK foreign key (Format) references Format(NazivFormata)
	);

create table Pise(
	IDKnjiga int not null,
	IDAutor int not null,

	constraint PISE_PK primary key (IDKnjiga, IDAutor),

	constraint PISE_FK1 foreign key (IDKnjiga) references Knjiga(IDKnjiga),
	constraint PISE_FK2 foreign key (IDAutor) references Autor(IDAutor)
	);

create table KnjigaNaJeziku(
	IDKnjiga int not null,
	OZNJ varchar(4) not null,

	constraint KNJ_PK primary key (IDKnjiga, OZNJ),
	
	constraint KNJ_FK1 foreign key (IDKnjiga) references Knjiga(IDKnjiga),
	constraint KNJ_FK2 foreign key (OZNJ) references Jezik(OZNJ)
	);

create table PripadaZanru(
	IDKnjiga int not null,
	OZNZ varchar(4) not null,

	constraint KPZ_PK primary key (IDKnjiga, OZNZ),
	
	constraint KPZ_FK1 foreign key (IDKnjiga) references Knjiga(IDKnjiga),
	constraint KPZ_FK2 foreign key (OZNZ) references Zanr(OZNZ)
	);
	
create table IzdajeKnjigu(
	IDKnjiga int not null,
	IDIK int not null,

	constraint IZDK_PK primary key (IDKnjiga, IDIK),
	
	constraint IZDK_FK1 foreign key (IDKnjiga) references Knjiga(IDKnjiga),
	constraint IZDK_FK2 foreign key (IDIK) references IzdKuca(IDIK)
	);


create table KnjigaULokalu(
	IDKnjiga int not null,
	IDBK int not null,
	DatVrIzmene datetime not null,
	Kolicina int not null,
	DatBrisanja date,

	constraint KUL_PK primary key (IDKnjiga, IDBK, DatVrIzmene),
	
	constraint KUL_FK1 foreign key (IDKnjiga) references Knjiga(IDKnjiga),
	constraint KUL_FK2 foreign key (IDBK) references Biblikutak(IDBK)
	);

/* SERIJSKO STIVO */
create table Periodicnost(
	PeriodIzd varchar(50) not null,
	Ucestalost int not null,

	constraint PERIOD_PK primary key (PeriodIzd)
	);

create table SerijskoStivo(
	IDSStivo int IDENTITY not null,
	Naziv varchar(MAX) not null,
	TipStiva int not null,
	Format varchar(50),
	Period varchar(50),
	DatBrisanja date,

	constraint SSTIVO_PK primary key (IDSStivo),

	constraint SSTIVO_FK1 foreign key (Format) references Format(NazivFormata),
	constraint SSTIVO_FK2 foreign key (Period) references Periodicnost(PeriodIzd)
	);

create table SStivoNaJeziku(
	IDSStivo int not null,
	OZNJ varchar(4) not null,

	constraint SSNJ_PK primary key (IDSStivo, OZNJ),
		
	constraint SSNJ_FK1 foreign key (IDSStivo) references SerijskoStivo(IDSStivo),
	constraint SSNJ_FK2 foreign key (OZNJ) references Jezik(OZNJ)
	);

create table IzdajeSStivo(
	IDSStivo int not null,
	IDIK int not null,

	constraint IZDSS_PK primary key (IDSStivo, IDIK),
		
	constraint IZDSS_FK1 foreign key (IDSStivo) references SerijskoStivo(IDSStivo),
	constraint IZDSS_FK2 foreign key (IDIK) references IzdKuca(IDIK)
	);

create table IzdanjeSStiva(
	IDSStivo int not null,
	BrIzd int not null,
	DatIzd date not null,
	Cena decimal(10,2) not null,
	DatBrisanja date,

	constraint ISS_PK primary key (IDSStivo, BrIzd),

	constraint ISS_FK foreign key (IDSStivo) references SerijskoStivo(IDSStivo)
	);
		
create table IzdSStivoULokalu(
	IDSStivo int not null,
	BrIzd int not null,
	IDBK int not null,
	DatVrIzmene datetime not null,
	Kolicina int not null,
	DatBrisanja date,

	constraint SSUL_PK primary key (IDSStivo, BrIzd, IDBK, DatVrIzmene),
	
	constraint SSUL_FK1 foreign key (IDSStivo, BrIzd) references IzdanjeSStiva(IDSStivo, BrIzd),
	constraint SSUL_FK2 foreign key (IDBK) references Biblikutak(IDBK)
	);

/* IZMENE */
create table IzmenaLokala(
	IDLokal int not null,
	IDAdmin int not null,
	DatVrIL datetime not null,
	Naziv varchar(MAX),
	DatOtv date,
	DatZat date,

	constraint IZLOK_PK primary key (IDLokal, IDAdmin, DatVrIL),

	constraint IZLOK_FK1 foreign key (IDLokal) references Biblikutak(IDBK),
	constraint IZLOK_FK2 foreign key (IDAdmin) references Admin(IDRadnik)
	);

create table RasporedjenBibliotekar(
	IDBib int not null,
	IDBK int not null,
	DatVr datetime not null,
	DatOd date,
	DatDo date,

	constraint RASBIB_PK primary key (IDBib, IDBK, DatVr),
	
	constraint RASBIB_FK1 foreign key (IDBib) references Bibliotekar(IDRadnik),
	constraint RASBIB_FK2 foreign key (IDBK) references Biblikutak(IDBK)
	);

create table IzmenaKnjige(
	IDKnjiga int not null,
	IDBib int not null,
	DatVr datetime not null,
	Naziv varchar(MAX),
	BrIzd int,
	GodIzd int, 
	VrIzd varchar(MAX),
	BrStrana int,
	VelicinaFonta int,
	Korice int,
	Ograniceno int,
	Format varchar(50),
	DatBrisanja date,

	constraint IZKNJIGA_PK primary key (IDKnjiga, IDBib, DatVr),
	
	constraint IZKNJIGA_FK1 foreign key (IDKnjiga) references Knjiga(IDKnjiga),
	constraint IZKNJIGA_FK2 foreign key (IDBib) references Bibliotekar(IDRadnik)
	);
	
create table IzKnjigeAutor(
	IDKnjiga int not null,
	IDBib int not null,
	DatVr datetime not null,
	IDAutor int not null,

	constraint IZKA_PK primary key (IDKnjiga, IDBib, DatVr, IDAutor),

	constraint IZKA_FK1 foreign key (IDKnjiga, IDBib, DatVr) references IzmenaKnjige(IDKnjiga, IDBib, DatVr),
	constraint IZKA_FK2 foreign key (IDAutor) references Autor(IDAutor)
	);

create table IzKnjigeJezik(
	IDKnjiga int not null,
	IDBib int not null,
	DatVr datetime not null,
	OZNJ varchar(4) not null,
		
	constraint IZKJ_PK primary key (IDKnjiga, IDBib, DatVr, OZNJ),

	constraint IZKJ_FK1 foreign key (IDKnjiga, IDBib, DatVr) references IzmenaKnjige(IDKnjiga, IDBib, DatVr),
	constraint IZKJ_FK2 foreign key (OZNJ) references Jezik(OZNJ)
	);

create table IzKnjigeZanr(
	IDKnjiga int not null,
	IDBib int not null,
	DatVr datetime not null,
	OZNZ varchar(4) not null,

	constraint IZKZ_PK primary key (IDKnjiga, IDBib, DatVr, OZNZ),

	constraint IZKZ_FK1 foreign key (IDKnjiga, IDBib, DatVr) references IzmenaKnjige(IDKnjiga, IDBib, DatVr),
	constraint IZKZ_FK2 foreign key (OZNZ) references Zanr(OZNZ)
	);
	
create table IzKnjigeIzdKuca(
	IDKnjiga int not null,
	IDBib int not null,
	DatVr datetime not null,
	IDIK int not null,

	constraint IZKIK_PK primary key (IDKnjiga, IDBib, IDIK, DatVr),

	constraint IZKIK_FK1 foreign key (IDKnjiga, IDBib, DatVr) references IzmenaKnjige(IDKnjiga, IDBib, DatVr),
	constraint IZKIK_FK2 foreign key (IDIK) references IzdKuca(IDIK)
	);


create table IzmenaAutora(
	IDAutor int not null,
	IDBib int not null,
	DatVr datetime not null,
	Ime varchar(50),
	Prezime varchar(50),
	DatRodj date,
	Biografija varchar(MAX),
	Drzava varchar(4),
	DatBrisanja date,

	constraint IZA_PK primary key (IDAutor, IDBib, DatVr),

	constraint IZA_FK1 foreign key (IDAutor) references Autor(IDAutor),
	constraint IZA_FK2 foreign key (IDBib) references Bibliotekar(IDRadnik),
	constraint IZA_FK3 foreign key (Drzava) references Drzava(OZND)
	);

create table IzmenaSStiva(
	IDSStivo int not null,
	IDBib int not null,
	DatVr datetime not null,
	Naziv varchar(MAX),
	TipStiva int,
	Format varchar(50),
	Period varchar(50),
	DatBrisanja date,

	constraint IZSS_PK primary key (IDSStivo, IDBib, DatVr),

	constraint IZSS_FK1 foreign key (IDSStivo) references SerijskoStivo(IDSStivo),
	constraint IZSS_FK2 foreign key (IDBib) references Bibliotekar(IDRadnik),
	constraint IZSS_FK3 foreign key (Format) references Format(NazivFormata),
	constraint IZSS_FK4 foreign key (Period) references Periodicnost(PeriodIzd)
	);

create table IzSStivaJezik(
	IDSStivo int not null,
	IDBib int not null,
	DatVr datetime not null,
	OZNJ varchar(4) not null,

	constraint IZSSJ_PK primary key (IDSStivo, IDBib, DatVr, OZNJ),

	constraint IZSSJ_FK1 foreign key (IDSStivo, IDBib, DatVr) references IzmenaSStiva(IDSStivo, IDBib, DatVr),
	constraint IZSSJ_FK2 foreign key (OZNJ) references Jezik(OZNJ)
	);

create table IzSStivaIzdKuca(
	IDSStivo int not null,
	IDBib int not null,
	DatVr datetime not null,
	IDIK int not null,

	constraint IZSSIK_PK primary key (IDSStivo, IDBib, DatVr, IDIK),

	constraint IZSSIK_FK1 foreign key (IDSStivo, IDBib, DatVr) references IzmenaSStiva(IDSStivo, IDBib, DatVr),
	constraint IZSSIK_FK2 foreign key (IDIK) references IzdKuca(IDIK)
	);
	
create table IzmenaIzdSStiva(
	IDSStivo int not null,
	BrIzd int not null,
	IDBib int not null,
	DatVr datetime not null,
	DatIzd date,
	Cena decimal(10,2),
	DatBrisanja date,

	constraint IZISS_PK primary key (IDSStivo, BrIzd, IDBib, DatVr),

	constraint IZISS_FK1 foreign key (IDSStivo, BrIzd) references IzdanjeSStiva(IDSStivo, BrIzd),
	constraint IZISS_FK2 foreign key (IDBib) references Bibliotekar(IDRadnik)
	);

create table IzmenaIzdKuce(
	IDIK int not null,
	IDBib int not null,
	DatVr datetime not null,
	Naziv varchar(MAX),
	Ulica varchar(200),
	Broj varchar(50),
	PosBr int,
	OZND varchar(4),
	DatZat date,

	constraint IZIK_PK primary key (IDIK, IDBib, DatVr),

	constraint IZIK_FK1 foreign key (IDIK) references IzdKuca(IDIK),
	constraint IZIK_FK2 foreign key (IDBib) references Bibliotekar(IDRadnik),
	constraint IZIK_FK3 foreign key (Ulica, Broj, PosBr, OZND) references Lokacija(Ulica, Broj, PosBr, OZND)
	);