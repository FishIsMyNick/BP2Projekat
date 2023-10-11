EXEC sp_MSforeachtable @command1 = "DROP TABLE ?"


CREATE TABLE DRZAVA(
	OZND varchar(3) NOT NULL,
	NazivDrzave varchar(50) NOT NULL,

	CONSTRAINT OZND_PK PRIMARY KEY (OZND)
);

CREATE TABLE MESTO(
	PosBr int NOT NULL,
	NazivMesta varchar(50) NOT NULL,

	CONSTRAINT MESTO_PK PRIMARY KEY (PosBr)
);

CREATE TABLE ADRESA(
	Ulica varchar(50) NOT NULL,
	Broj varchar(10) NOT NULL,

	CONSTRAINT ADRESA_PK PRIMARY KEY (Ulica, Broj)
);

CREATE TABLE MESTOUDRZAVI(
	PosBr int NOT NULL,
	OZND varchar(3) NOT NULL,

	CONSTRAINT MUD_PK PRIMARY KEY (PosBr, OZND),

	CONSTRAINT MUD_FK1 FOREIGN KEY (PosBr) REFERENCES MESTO(PosBr),
	CONSTRAINT MUD_FK2 FOREIGN KEY (OZND) REFERENCES DRZAVA(OZND)
);

CREATE TABLE LOKACIJA(
	Ulica varchar(50) NOT NULL,
	Broj varchar(10) NOT NULL,
	PosBr int NOT NULL,
	OZND varchar(3) NOT NULL,

	CONSTRAINT LOK_PK PRIMARY KEY (Ulica, Broj, PosBr, OZND),
	
	CONSTRAINT LOK_FK1 FOREIGN KEY (Ulica, Broj) REFERENCES ADRESA(Ulica, Broj),
	
	CONSTRAINT LOK_FK2 FOREIGN KEY (PosBr) REFERENCES MESTO(PosBr),
	CONSTRAINT LOK_FK3 FOREIGN KEY (OZND) REFERENCES DRZAVA(OZND)
);


CREATE TABLE BIBLIKUTAK(
	IDBK int NOT NULL,
	Naziv varchar(50) NOT NULL,
	Ulica varchar(50) NOT NULL,
	Broj varchar(10) NOT NULL,
	PosBr int NOT NULL,
	OZND varchar(3) NOT NULL,

	CONSTRAINT BIB_PK PRIMARY KEY (IDBK),

	CONSTRAINT BIB_FK FOREIGN KEY (Ulica, Broj, PosBr, OZND) REFERENCES LOKACIJA(Ulica, Broj, PosBr, OZND)
);

CREATE TABLE IZDKUCA(
	IDIK int NOT NULL,
	Naziv varchar(50) not null,
	Ulica varchar(50) not null,
	Broj varchar(10) not null,
	PosBr int not null,
	OZND varchar(3) not null,

	CONSTRAINT IK_PK PRIMARY KEY (IDIK),

	CONSTRAINT IK_FK FOREIGN KEY (Ulica, Broj, PosBr, OZND) REFERENCES LOKACIJA(Ulica, Broj, PosBr, OZND)
);

CREATE TABLE RADNIK(
	IDRadnik int not null,
	KorisnickoIme varchar(50) NOT NULL,
	Sifra varchar(MAX) not null,
	Ime varchar(20) NOT NULL,
	Prezime varchar(20) NOT NULL,
	DatRodj date DEFAULT '1970-1-1' not null,
	DatZap date DEFAULT '2023-1-1' NOT NULL,
	Tip int NOT NULL CHECK (Tip BETWEEN 1 AND 2),
	IDBK int,

	CONSTRAINT RAD_PK PRIMARY KEY (IDRadnik),

	CONSTRAINT RAD_FK FOREIGN KEY (IDBK) REFERENCES BIBLIKUTAK(IDBK),
);

CREATE TABLE ZANR(
	OZNZ varchar(4) NOT NULL,
	NazivZanra varchar(MAX) not null,

	CONSTRAINT ZANR_PK PRIMARY KEY (OZNZ)
);

CREATE TABLE AUTOR(
	IDAutor int not null,
	Ime varchar(50) not null,
	Prezime varchar(50),
	DatRodj date,
	Biografija varchar(MAX),
	OZND varchar(3),

	CONSTRAINT AUT_PK PRIMARY KEY (IDAutor),

	CONSTRAINT AUT_FK FOREIGN KEY (OZND) REFERENCES DRZAVA(OZND)
);

create table JEZIK(
	OZNJ varchar(4) not null,
	NazivJezika varchar(max) not null,

	constraint JEZ_PK primary key (OZNJ)
);

create table PERIODICNOST(
	PeriodIzd varchar(20) not null,

	constraint PER_PK primary key (PeriodIzd)
);

create table KNJIGA(
	IDKnjiga int not null,
	Naziv varchar(max) not null,
	GodIzd date,
	BrIzd int,
	Ograniceno bit,

	constraint KNJ_PK primary key (IDKnjiga)
);

create table KNJIGANAJEZIKU(
	IDKnjiga int not null,
	OZNJ varchar(4) not null,

	constraint KJZ_PK primary key (IDKnjiga, OZNJ),

	constraint KJZ_FK1 foreign key (IDKnjiga) references KNJIGA(IDKnjiga),
	constraint KJZ_FK2 foreign key (OZNJ) references JEZIK(OZNJ)
);

create table PRIPADAZANRU(
	IDKnjiga int not null,
	OZNZ varchar(4) not null,

	constraint KPZ_PK primary key (IDKnjiga, OZNZ),

	constraint KPZ_FK1 foreign key (IDKnjiga) references KNJIGA(IDKnjiga),
	constraint KPZ_FK2 foreign key (OZNZ) references ZANR(OZNZ)
);

create table PISE(
	IDKnjiga int not null,
	IDAutor int not null,

	constraint PIS_PK primary key (IDKnjiga, IDAutor),

	constraint PIS_FK1 foreign key (IDKnjiga) references KNJIGA(IDKnjiga),
	constraint PIS_FK2 foreign key (IDAutor) references AUTOR(IDAutor)
);

create table IZDAJEKNJIGU(
	IDKnjiga int not null,
	IDIK int not null,

	constraint IKK_PK primary key (IDKnjiga, IDIK),

	constraint IKK_FK1 foreign key (IDKnjiga) references KNJIGA(IDKnjiga),
	constraint IKK_FK2 foreign key (IDIK) references IZDKUCA(IDIK)
);

create table SERIJSKOSTIVO(
	IDSStivo int not null,
	Naziv varchar(max) not null,
	Cena int not null,
	DatIzd date,
	BrIzd int,
	Tip int not null check(Tip between 1 and 2),
	PeriodIzd varchar(20),

	constraint SS_PK primary key (IDSStivo),

	constraint SS_FK foreign key (PeriodIzd) references PERIODICNOST(PeriodIzd)
);

create table SSTIVONAJEZIKU(
	IDSStivo int not null,
	OZNJ varchar(4) not null,

	constraint SSNJ_PK primary key (IDSStivo, OZNJ),

	constraint SSNJ_FK1 foreign key (IDSStivo) references SERIJSKOSTIVO(IDSStivo),
	constraint SSNJ_FK2 foreign key (OZNJ) references JEZIK(OZNJ)
);

create table IZDAJESSTIVO(
	IDSStivo int not null,
	IDIK int not null,

	constraint IZDS_PK primary key (IDSStivo, IDIK),

	constraint IZDS_FK1 foreign key (IDSStivo) references SERIJSKOSTIVO(IDSStivo),
	constraint IZDS_FK2 foreign key (IDIK) references IZDKUCA(IDIK)
);

create table CLAN(
	IDClan int not null,
	KorisnickoIme varchar(20) not null,
	Sifra varchar(max) not null,
	Ime varchar(20) not null,
	Prezime varchar(20) not null,
	DatRodj Date not null,
	BrTel varchar(20),
	Tip int not null check(Tip between 1 and 3),
	Ulica varchar(50) not null,
	Broj varchar(10) not null,
	PosBr int not null,
	OZND varchar(3) not null,

	constraint CL_PK primary key (IDClan),

	constraint CL_FK foreign key (Ulica, Broj , PosBr, OZND) references LOKACIJA(Ulica, Broj , PosBr, OZND)
);

create table IZMENASIFRE(
	IDSifra int not null,
	IDClan int not null,
	Sifra varchar(max) not null,
	DatVr datetime not null,

	constraint IZSI_PK primary key (IDSifra, IDClan),

	constraint IZSI_FK foreign key (IDClan) references CLAN(IDClan)
);

create table IZMENAKREDITA(
	IDKredit int not null,
	IDClan int not null,
	IDBK int not null,
	Kolicina int not null,
	DatVr datetime not null,

	constraint IKR_PK primary key (IDKredit, IDClan, IDBK),

	constraint IKR_FK1 foreign key (IDClan) references CLAN(IDClan),
	constraint IKR_FK2 foreign key (IDBK) references BIBLIKUTAK(IDBK)
); 

create table IZMENASTATUSA(
	IDStatus int not null,
	IDClan int not null,
	Tip int,
	Prosek float,
	Budzet bit,
	Zaposlen bit,
	DatVr datetime not null,

	constraint IZST_PK primary key (IDStatus, IDClan),

	constraint IZST_FK foreign key (IDClan) references CLAN(IDClan)
); 

create table IZMENALOKACIJE(
	IDIL int not null,
	IDClan int not null,
	Ulica varchar(50) not null,
	Broj varchar(10) not null,
	PosBr int not null,
	OZND varchar(3) not null,
	DatVr datetime not null,

	constraint IZL_PK primary key (IDIL, IDClan, Ulica, Broj, PosBr, OZND),

	constraint IZL_FK1 foreign key (IDClan) references CLAN(IDClan),
	constraint IZL_FK2 foreign key (Ulica, Broj, PosBr, OZND) references LOKACIJA(Ulica, Broj, PosBr, OZND)
); 

create table IZMENAPODATAKA(
	IDPodatak int not null,
	IDClan int not null,
	Ime varchar(50),
	Prezime varchar(10),
	BrTel int,
	DatVr datetime not null,

	constraint IZP_PK primary key (IDPodatak, IDClan),

	constraint IZP_FK foreign key (IDClan) references CLAN(IDClan)
); 

create table KNJIGAULOKALU(
	IDKnjiga int not null,
	IDBK int not null,
	Kolicina int not null,

	constraint KUL_PK primary key (IDKnjiga, IDBK),

	constraint KUL_FK1 foreign key (IDKnjiga) references KNJIGA(IDKnjiga),
	constraint KUL_FK2 foreign key (IDBK) references BIBLIKUTAK(IDBK)
);

create table REZERVACIJA(
	IDRez int not null,
	IDKnjiga int not null,
	IDClan int not null,
	IDBK int not null,
	DatVr datetime not null,
	PotvrdioRez int,
	DatVrPot datetime,
	ZakljucioRez int,
	DatVrZak datetime,

	constraint REZ_PK primary key (IDRez, IDKnjiga, IDClan, IDBK),

	constraint REZ_FK1 foreign key (IDKnjiga, IDBK) references KNJIGAULOKALU(IDKnjiga, IDBK),
	constraint REZ_FK2 foreign key (IDClan) references CLAN(IDClan),
	constraint REZ_FK3 foreign key (PotvrdioRez) references RADNIK(IDRadnik),
	constraint REZ_FK4 foreign key (ZakljucioRez) references RADNIK(IDRadnik),
);

create table SERIJSKOSTIVOULOKALU(
	IDSStivo int not null,
	IDBK int not null,
	Kolicina int not null,

	constraint SUL_PK primary key (IDSStivo, IDBK),

	constraint SUL_FK1 foreign key (IDSStivo) references SERIJSKOSTIVO(IDSStivo),
	constraint SUL_FK2 foreign key (IDBK) references BIBLIKUTAK(IDBK)
);

create table KUPOVINA(
	IDKup int not null,
	IDSStivo int not null,
	IDClan int not null,
	IDBK int not null,
	DatVr datetime not null,
	PotvrdioKup int,
	DatVrPot datetime,

	constraint KUP_PK primary key (IDKup, IDSStivo, IDClan, IDBK),

	constraint KUP_FK1 foreign key (IDSStivo, IDBK) references SERIJSKOSTIVOULOKALU(IDSStivo, IDBK),
	constraint KUP_FK2 foreign key (IDClan) references CLAN(IDClan),
	constraint KUP_FK3 foreign key (PotvrdioKup) references RADNIK(IDRadnik)
);

-- KRITICNO
--
--
--

create table ZAHTEVZAKNJIGU(
	Knjiga varchar(50) not null,
	Autor varchar(50) not null,
	Jezik varchar(30) not null,
	IDClan int not null,
	IDBK int not null,
	DatVr datetime not null,
	Ispunjen bit not null,

	constraint ZZK_PK primary key (Knjiga, Autor, Jezik, IDClan, IDBK),

	constraint ZZK_FK1 foreign key (IDClan) references CLAN(IDClan),
	constraint ZZK_FK2 foreign key (IDBK) references BIBLIKUTAK(IDBK)
);

create table ISPUNJENZAHTEVKNJIGA(
	IDClan int not null,
	IDKnjiga int not null,
	IDBK int not null,
	DatVrIsp datetime not null,
	Procitano bit not null,

	constraint IZK_PK primary key (IDClan, IDKnjiga, IDBK),

	constraint IZK_FK1 foreign key (IDClan) references CLAN(IDClan),
	constraint IZK_FK2 foreign key (IDKnjiga, IDBK) references KNJIGAULOKALU(IDKnjiga, IDBK),
);


create table ZAHTEVZASERIJSKOSTIVO(
	Naziv varchar(50) not null,
	Jezik varchar(30) not null,
	Tip int not null,
	IDClan int not null,
	IDBK int not null,
	DatVr datetime not null,
	Ispunjen bit not null,

	constraint ZZS_PK primary key (Naziv, Jezik, Tip, IDClan, IDBK),

	constraint ZZS_FK1 foreign key (IDClan) references CLAN(IDClan),
	constraint ZZS_FK2 foreign key (IDBK) references BIBLIKUTAK(IDBK)
);

create table ISPUNJENZAHTEVSERIJSKOSTIVO(
	IDClan int not null,
	IDSStivo int not null,
	IDBK int not null,
	DatVrIsp datetime not null,
	Procitano bit not null,

	constraint IZS_PK primary key (IDClan, IDSStivo, IDBK),

	constraint IZS_FK1 foreign key (IDClan) references CLAN(IDClan),
	constraint IZS_FK2 foreign key (IDSStivo, IDBK) references SERIJSKOSTIVOULOKALU(IDSStivo, IDBK),
);

--
--
--
--

create table OCENAKNJIGE(
	IDOcenaK int not null,
	IDClan int not null,
	IDKnjiga int not null,
	DatVr datetime not null,
	Ocena int not null check(Ocena between 1 and 10),
	Komentar varchar(max),

	constraint OCK_PK primary key (IDOcenaK, IDClan, IDKnjiga),

	constraint OCK_FK1 foreign key (IDClan) references CLAN(IDClan),
	constraint OCK_FK2 foreign key (IDKnjiga) references KNJIGA(IDKnjiga)
);

create table OCENASSTIVA(
	IDOcenaS int not null,
	IDClan int not null,
	IDSStivo int not null,
	DatVr datetime not null,
	Ocena int not null check(Ocena between 1 and 10),
	Komentar varchar(max),

	constraint OCS_PK primary key (IDOcenaS, IDClan, IDSStivo),

	constraint OCS_FK1 foreign key (IDClan) references CLAN(IDClan),
	constraint OCS_FK2 foreign key (IDSStivo) references SERIJSKOSTIVO(IDSStivo)
);

create table CLANARINA(
	OZNC varchar(3) not null,
	DatUvoda date not null,
	NazivClanarine varchar(20) not null,
	Cena float not null,
	Izbacena bit not null,

	constraint CLA_PK primary key (OZNC, DatUvoda)
);

create table ISTORIJACLANARINA(
	IDCL int not null,
	IDClan int not null,
	OZNC varchar(3) not null,
	DatUvoda date not null,
	DatVrStart datetime not null,
	DatVrUplate datetime not null,

	constraint ICL_PK primary key (IDCL, IDClan, OZNC, DatUvoda),

	constraint ICL_FK1 foreign key (IDClan) references CLAN(IDClan),
	constraint ICL_FK2 foreign key (OZNC, DatUvoda) references CLANARINA(OZNC, DatUvoda)
);
























--ALTER TABLE LOKACIJA DROP CONSTRAINT LOK_FK1;
--ALTER TABLE LOKACIJA DROP CONSTRAINT LOK_FK2;
--ALTER TABLE LOKACIJA DROP CONSTRAINT LOK_FK3;
--ALTER TABLE LOKACIJA DROP CONSTRAINT LOK_FK4;
--ALTER TABLE MESTOUDRZAVI DROP CONSTRAINT MUD_FK1;
--ALTER TABLE MESTOUDRZAVI DROP CONSTRAINT MUD_FK2;

--DROP TABLE IF EXISTS LOKACIJA;
--DROP TABLE IF EXISTS MESTOUDRZAVI;
--DROP TABLE IF EXISTS ADRESA;
--DROP TABLE IF EXISTS MESTO;
--DROP TABLE IF EXISTS DRZAVA;

--ALTER TABLE CLANSKAKARTICA DROP CONSTRAINT CLK_FK;
--ALTER TABLE CLAN DROP CONSTRAINT CLN_FK;
--ALTER TABLE DJAK DROP CONSTRAINT DJK_FK;
--ALTER TABLE STUDENT DROP CONSTRAINT STD_FK;
--ALTER TABLE ODRASTAO DROP CONSTRAINT ODR_FK;
--ALTER TABLE MAGAZINULOKALU DROP CONSTRAINT MULM_FK;
--ALTER TABLE MAGAZINULOKALU DROP CONSTRAINT MULL_FK;
--ALTER TABLE NOVINEULOKALU DROP CONSTRAINT NULN_FK;
--ALTER TABLE NOVINEULOKALU DROP CONSTRAINT NULL_FK;
--ALTER TABLE KNJIGAULOKALU DROP CONSTRAINT KULK_FK;
--ALTER TABLE KNJIGAULOKALU DROP CONSTRAINT KULL_FK;
--ALTER TABLE BIBLIOTEKAR DROP CONSTRAINT BIB_FK;
--ALTER TABLE ZAHTEVZAKNJIGU DROP CONSTRAINT ZZKL_FK;
--ALTER TABLE CLANZAHTEVAKNJIGU DROP CONSTRAINT CZKC_FK;
--ALTER TABLE CLANZAHTEVAKNJIGU DROP CONSTRAINT CZKN_FK;
--ALTER TABLE CLANZAHTEVAKNJIGU DROP CONSTRAINT CZKA_FK;
--ALTER TABLE CLANZAHTEVAKNJIGU DROP CONSTRAINT CZKL_FK;
--ALTER TABLE ISPUNJENZAHTEVZAKNJIGU DROP CONSTRAINT IZKC_FK;
--ALTER TABLE ISPUNJENZAHTEVZAKNJIGU DROP CONSTRAINT IZKK_FK;
--ALTER TABLE ISPUNJENZAHTEVZAKNJIGU DROP CONSTRAINT IZKL_FK;
--ALTER TABLE ZAHTEVZANOVINE DROP CONSTRAINT ZZNL_FK;
--ALTER TABLE CLANZAHTEVANOVINE DROP CONSTRAINT CZNC_FK;
--ALTER TABLE CLANZAHTEVANOVINE DROP CONSTRAINT CZNN_FK;
--ALTER TABLE CLANZAHTEVANOVINE DROP CONSTRAINT CZNL_FK;
--ALTER TABLE ISPUNJENZAHTEVZANOVINE DROP CONSTRAINT IZNC_FK;
--ALTER TABLE ISPUNJENZAHTEVZANOVINE DROP CONSTRAINT IZNN_FK;
--ALTER TABLE ISPUNJENZAHTEVZANOVINE DROP CONSTRAINT IZNL_FK;
--ALTER TABLE ZAHTEVZAMAGAZIN DROP CONSTRAINT ZZML_FK;
--ALTER TABLE CLANZAHTEVAMAGAZIN DROP CONSTRAINT CZMC_FK;
--ALTER TABLE CLANZAHTEVAMAGAZIN DROP CONSTRAINT CZMN_FK;
--ALTER TABLE CLANZAHTEVAMAGAZIN DROP CONSTRAINT CZML_FK;
--ALTER TABLE ISPUNJENZAHTEVZAMAGAZIN DROP CONSTRAINT IZMC_FK;
--ALTER TABLE ISPUNJENZAHTEVZAMAGAZIN DROP CONSTRAINT IZMM_FK;
--ALTER TABLE ISPUNJENZAHTEVZAMAGAZIN DROP CONSTRAINT IZML_FK;
--ALTER TABLE REZERVACIJA DROP CONSTRAINT REZC_FK;
--ALTER TABLE REZERVACIJA DROP CONSTRAINT REZK_FK;
--ALTER TABLE REZERVACIJA DROP CONSTRAINT REZL_FK;

--DROP TABLE IF EXISTS CENOVNIK;
--DROP TABLE IF EXISTS CORNERLIBRARY;
--DROP TABLE IF EXISTS BIBLIOTEKAR;
--DROP TABLE IF EXISTS KNJIGA;
--DROP TABLE IF EXISTS KNJIGAULOKALU;
--DROP TABLE IF EXISTS NOVINE;
--DROP TABLE IF EXISTS NOVINEULOKALU;
--DROP TABLE IF EXISTS MAGAZIN;
--DROP TABLE IF EXISTS MAGAZINULOKALU;
--DROP TABLE IF EXISTS CLANSKAKARTICA;
--DROP TABLE IF EXISTS CLAN;
--DROP TABLE IF EXISTS DJAK;
--DROP TABLE IF EXISTS STUDENT;
--DROP TABLE IF EXISTS ODRASTAO;
--DROP TABLE IF EXISTS ZAHTEVZAKNJIGU;
--DROP TABLE IF EXISTS CLANZAHTEVAKNJIGU;
--DROP TABLE IF EXISTS ISPUNJENZAHTEVZAKNJIGU;
--DROP TABLE IF EXISTS ZAHTEVZANOVINE;
--DROP TABLE IF EXISTS CLANZAHTEVANOVINE;
--DROP TABLE IF EXISTS ISPUNJENZAHTEVZANOVINE;
--DROP TABLE IF EXISTS ZAHTEVZAMAGAZIN;
--DROP TABLE IF EXISTS CLANZAHTEVAMAGAZIN;
--DROP TABLE IF EXISTS ISPUNJENZAHTEVZAMAGAZIN;
--DROP TABLE IF EXISTS REZERVACIJA;







--CREATE TABLE CLAN(
--	ID int UNIQUE NOT NULL,
--	Username varchar(20) UNIQUE NOT NULL,
--	Pass varchar(MAX) NOT NULL,
--	Ime varchar(20) NOT NULL,
--	Prezime varchar(20) NOT NULL,
--	DatRodj date DEFAULT '1970-1-1',
--	Kredit float DEFAULT 0 NOT NULL,
--	Tip int NOT NULL CHECK (Tip BETWEEN 0 AND 2),

--	CONSTRAINT CLN_PK PRIMARY KEY (ID),
--	);


--CREATE TABLE DJAK(
--	ID int UNIQUE NOT NULL,
--	Skola varchar(50),
--	Prosek float NOT NULL,

--	CONSTRAINT DJK_PK PRIMARY KEY (ID),

--	CONSTRAINT DJK_FK FOREIGN KEY (ID) REFERENCES CLAN(ID)
--	);

--CREATE TABLE STUDENT(
--	ID int UNIQUE NOT NULL,
--	Fakultet varchar(50),
--	Budzet binary(1) DEFAULT 0 NOT NULL,

--	CONSTRAINT STD_PK PRIMARY KEY (ID),

--	CONSTRAINT STD_FK FOREIGN KEY (ID) REFERENCES CLAN(ID)
--	);
	
--CREATE TABLE ODRASTAO(
--	ID int UNIQUE NOT NULL,
--	Zaposlen binary(1) DEFAULT 0 NOT NULL,

--	CONSTRAINT ORD_PK PRIMARY KEY (ID),

--	CONSTRAINT ODR_FK FOREIGN KEY (ID) REFERENCES CLAN(ID)
--	);

--CREATE TABLE MAGAZIN(
--	ID int UNIQUE NOT NULL,
--	NazivMag varchar(50) NOT NULL,
--	PeriodIzd varchar(10) DEFAULT 'mesecno' NOT NULL,
--	Cena float NOT NULL,

--	CONSTRAINT MAG_PK PRIMARY KEY(ID)
--	);

--CREATE TABLE MAGAZINULOKALU(
--	IDM int UNIQUE NOT NULL,
--	IDL int UNIQUE NOT NULL,
--	Kolicina int DEFAULT 0 NOT NULL,

--	CONSTRAINT MUL_PK PRIMARY KEY (IDM, IDL),

--	CONSTRAINT MULM_FK FOREIGN KEY (IDM) REFERENCES MAGAZIN(ID),
--	CONSTRAINT MULL_FK FOREIGN KEY (IDL) REFERENCES CORNERLIBRARY(ID)
--	);

--CREATE TABLE NOVINE(
--	ID int UNIQUE NOT NULL,
--	NazivNov varchar(50) NOT NULL,
--	PeriodIzd varchar(10) DEFAULT 'mesecno' NOT NULL,
--	Cena float NOT NULL,

--	CONSTRAINT NOV_PK PRIMARY KEY(ID)
--	);

--CREATE TABLE NOVINEULOKALU(
--	IDN int UNIQUE NOT NULL,
--	IDL int UNIQUE NOT NULL,
--	Kolicina int DEFAULT 0 NOT NULL,

--	CONSTRAINT NUL_PK PRIMARY KEY (IDN, IDL),

--	CONSTRAINT NULN_FK FOREIGN KEY (IDN) REFERENCES NOVINE(ID),
--	CONSTRAINT NULL_FK FOREIGN KEY (IDL) REFERENCES CORNERLIBRARY(ID)
--	);

--CREATE TABLE KNJIGA(
--	ID int UNIQUE NOT NULL,
--	Naziv varchar(50) NOT NULL,
--	Autor varchar(50) NOT NULL,
--	IzdKuca varchar(50),
--	DatIzd date DEFAULT GETDATE() NOT NULL,
--	Zanr varchar(20) NOT NULL,
--	Jezik varchar(20) NOT NULL,
--	Ograniceno binary(1) DEFAULT 0 NOT NULL,

--	CONSTRAINT KNJ_PK PRIMARY KEY(ID)
--	);

--CREATE TABLE KNJIGAULOKALU(
--	IDK int UNIQUE NOT NULL,
--	IDL int UNIQUE NOT NULL,
--	Kolicina int DEFAULT 0 NOT NULL,

--	CONSTRAINT KUL_PK PRIMARY KEY (IDK, IDL),

--	CONSTRAINT KULK_FK FOREIGN KEY (IDK) REFERENCES KNJIGA(ID),
--	CONSTRAINT KULL_FK FOREIGN KEY (IDL) REFERENCES CORNERLIBRARY(ID)
--	);

--CREATE TABLE BIBLIOTEKAR(
--	ID int UNIQUE NOT NULL,
--	Username varchar(20) NOT NULL,
--	Pass varchar(MAX) NOT NULL,
--	Ime varchar(20),
--	Prezime varchar(20),
--	IDLib int NOT NULL,

--	CONSTRAINT BIB_PK PRIMARY KEY(ID),

--	CONSTRAINT BIB_FK FOREIGN KEY (IDLib) REFERENCES CORNERLIBRARY(ID)
--	);


--CREATE TABLE CENOVNIK(
--	Izdanje varchar(10) UNIQUE NOT NULL,
--	Cena float NOT NULL,

--	CONSTRAINT IZD_PK PRIMARY KEY (Izdanje)
--	);

--CREATE TABLE ZAHTEVZAKNJIGU(
--	NazK varchar(50) UNIQUE NOT NULL,
--	Autor varchar(50) UNIQUE NOT NULL,
--	Lokal int UNIQUE NOT NULL,

--	CONSTRAINT ZZK_PK PRIMARY KEY (NazK, Autor, Lokal),

--	CONSTRAINT ZZKL_FK FOREIGN KEY (Lokal) REFERENCES CORNERLIBRARY(ID)
--	);

--CREATE TABLE CLANZAHTEVAKNJIGU(
--	Clan int UNIQUE NOT NULL,
--	NazK varchar(50) UNIQUE NOT NULL,
--	Autor varchar(50) UNIQUE NOT NULL,
--	Lokal int UNIQUE NOT NULL,
--	CONSTRAINT CZK_PK PRIMARY KEY (Clan, NazK, Autor, Lokal),

--	CONSTRAINT CZKC_FK FOREIGN KEY (Clan) REFERENCES CLAN(ID),
--	CONSTRAINT CZKN_FK FOREIGN KEY (NazK) REFERENCES ZAHTEVZAKNJIGU(NazK),
--	CONSTRAINT CZKA_FK FOREIGN KEY (Autor) REFERENCES ZAHTEVZAKNJIGU(Autor),
--	CONSTRAINT CZKL_FK FOREIGN KEY (Lokal) REFERENCES ZAHTEVZAKNJIGU(Lokal),
--	);

--CREATE TABLE ISPUNJENZAHTEVZAKNJIGU(
--	Clan int UNIQUE NOT NULL,
--	IZKnjiga int UNIQUE NOT NULL,
--	IZLokal int UNIQUE NOT NULL,

--	CONSTRAINT IZK_PK PRIMARY KEY (Clan, IZKnjiga, IZLokal),

--	CONSTRAINT IZKC_FK FOREIGN KEY (Clan) REFERENCES CLAN(ID),
--	CONSTRAINT IZKK_FK FOREIGN KEY (IZKnjiga) REFERENCES KNJIGAULOKALU(IDK),
--	CONSTRAINT IZKL_FK FOREIGN KEY (IZLokal) REFERENCES KNJIGAULOKALU(IDL)
--	);

	
--CREATE TABLE ZAHTEVZANOVINE(
--	NazN varchar(50) UNIQUE NOT NULL,
--	Lokal int UNIQUE NOT NULL,

--	CONSTRAINT ZZN_PK PRIMARY KEY (NazN, Lokal),

--	CONSTRAINT ZZNL_FK FOREIGN KEY (Lokal) REFERENCES CORNERLIBRARY(ID)
--	);

--CREATE TABLE CLANZAHTEVANOVINE(
--	Clan int UNIQUE NOT NULL,
--	NazN varchar(50) UNIQUE NOT NULL,
--	Lokal int UNIQUE NOT NULL,
--	CONSTRAINT CZN_PK PRIMARY KEY (Clan, NazN, Lokal),

--	CONSTRAINT CZNC_FK FOREIGN KEY (Clan) REFERENCES CLAN(ID),
--	CONSTRAINT CZNN_FK FOREIGN KEY (NazN) REFERENCES ZAHTEVZANOVINE(NazN),
--	CONSTRAINT CZNL_FK FOREIGN KEY (Lokal) REFERENCES ZAHTEVZANOVINE(Lokal),
--	);

--CREATE TABLE ISPUNJENZAHTEVZANOVINE(
--	Clan int UNIQUE NOT NULL,
--	IZNovine int UNIQUE NOT NULL,
--	IZLokal int UNIQUE NOT NULL,

--	CONSTRAINT IZN_PK PRIMARY KEY (Clan, IZNovine, IZLokal),

--	CONSTRAINT IZNC_FK FOREIGN KEY (Clan) REFERENCES CLAN(ID),
--	CONSTRAINT IZNN_FK FOREIGN KEY (IZNovine) REFERENCES NOVINEULOKALU(IDN),
--	CONSTRAINT IZNL_FK FOREIGN KEY (IZLokal) REFERENCES NOVINEULOKALU(IDL)
--	);

	
--CREATE TABLE ZAHTEVZAMAGAZIN(
--	NazM varchar(50) UNIQUE NOT NULL,
--	Lokal int UNIQUE NOT NULL,

--	CONSTRAINT ZZM_PK PRIMARY KEY (NazM, Lokal),

--	CONSTRAINT ZZML_FK FOREIGN KEY (Lokal) REFERENCES CORNERLIBRARY(ID)
--	);

--CREATE TABLE CLANZAHTEVAMAGAZIN(
--	Clan int UNIQUE NOT NULL,
--	NazM varchar(50) UNIQUE NOT NULL,
--	Lokal int UNIQUE NOT NULL,

--	CONSTRAINT CZM_PK PRIMARY KEY (Clan, NazM, Lokal),

--	CONSTRAINT CZMC_FK FOREIGN KEY (Clan) REFERENCES CLAN(ID),
--	CONSTRAINT CZMN_FK FOREIGN KEY (NazM) REFERENCES ZAHTEVZAMAGAZIN(NazM),
--	CONSTRAINT CZML_FK FOREIGN KEY (Lokal) REFERENCES ZAHTEVZAMAGAZIN(Lokal),
--	);

--CREATE TABLE ISPUNJENZAHTEVZAMAGAZIN(
--	Clan int UNIQUE NOT NULL,
--	IZMagazin int UNIQUE NOT NULL,
--	IZLokal int UNIQUE NOT NULL,

--	CONSTRAINT IZM_PK PRIMARY KEY (Clan, IZMagazin, IZLokal),

--	CONSTRAINT IZMC_FK FOREIGN KEY (Clan) REFERENCES CLAN(ID),
--	CONSTRAINT IZMM_FK FOREIGN KEY (IZMagazin) REFERENCES MAGAZINULOKALU(IDM),
--	CONSTRAINT IZML_FK FOREIGN KEY (IZLokal) REFERENCES MAGAZINULOKALU(IDL)
--	);

--CREATE TABLE REZERVACIJA(
--	Clan int UNIQUE NOT NULL,
--	Knjiga int UNIQUE NOT NULL,
--	Lokal int UNIQUE NOT NULL,
--	Datum date DEFAULT GETDATE() NOT NULL,

--	CONSTRAINT REZ_PK PRIMARY KEY (Clan, Knjiga, Lokal),

--	CONSTRAINT REZC_FK FOREIGN KEY (Clan) REFERENCES CLAN(ID),
--	CONSTRAINT REZK_FK FOREIGN KEY (Knjiga) REFERENCES KNJIGAULOKALU(IDK),
--	CONSTRAINT REZL_FK FOREIGN KEY (Lokal) REFERENCES KNJIGAULOKALU(IDL)
--	);

--CREATE TABLE ISTORIJAREZERVACIJA(
--	Clan int NOT NULL,
--	Knjiga int NOT NULL,
--	DatumUzeo date NOT NULL,
--	DatumVratio date,

--	CONSTRAINT IR_PK PRIMARY KEY (Clan, Knjiga, DatumUzeo),

--	CONSTRAINT IRC_FK FOREIGN KEY (Clan) REFERENCES CLAN(ID),
--	CONSTRAINT IRK_FK FOREIGN KEY (Knjiga) REFERENCES KNJIGA(ID)
--	);

--CREATE TABLE KUPIONOVINE(
--	Clan int NOT NULL,
--	Novine int NOT NULL,
--	Datum date NOT NULL,

--	CONSTRAINT KN_PK PRIMARY KEY (Clan, Novine, Datum),

--	CONSTRAINT KNC_FK FOREIGN KEY (Clan) REFERENCES CLAN(ID),
--	CONSTRAINT KNN_FK FOREIGN KEY (Novine) REFERENCES NOVINE(ID)
--	);

--CREATE TABLE KUPIOMAGAZIN(
--	Clan int NOT NULL,
--	Magazin int NOT NULL,
--	Datum date NOT NULL,

--	CONSTRAINT KM_PK PRIMARY KEY (Clan, Magazin, Datum),

--	CONSTRAINT KMC_FK FOREIGN KEY (Clan) REFERENCES CLAN(ID),
--	CONSTRAINT KMM_FK FOREIGN KEY (Magazin) REFERENCES MAGAZIN(ID)
--	);

--Alter TABLE NOVINE ADD  Cena float NOT NULL;	
--Alter TABLE MAGAZIN ADD  Cena float NOT NULL;