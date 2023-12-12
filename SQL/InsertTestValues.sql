/*
delete from IzmenaIzdKuce;
delete from IzmenaIzdSStiva;
delete from IzSStivaIzdKuca;
delete from IzSStivaJezik;
delete from IzmenaSStiva;
delete from IzmenaAutora;
delete from RasporedjenBibliotekar;
delete from IzmenaLokala;
delete from IzKnjigeIzdKuca;
delete from IzKnjigeZanr;
delete from IzKnjigeJezik;
delete from IzKnjigeAutor;
delete from IzmenaKnjige;
delete from IzdSStivoULokalu;
delete from IzdanjeSStiva;
delete from IzdajeSStivo;
delete from SStivoNaJeziku;
delete from SerijskoStivo;
delete from Periodicnost;
delete from KnjigaULokalu;
delete from IzdajeKnjigu;
delete from IzdajeKnjigu;
delete from PripadaZanru;
delete from Pise;
delete from Knjiga;
delete from Autor;
delete from Jezik;
delete from Zanr;
delete from Format;
delete from KurirKoristiNalog;
delete from BibliotekarKoristiNalog;
delete from AdminKoristiNalog;
delete from Bibliotekar;
delete from Kurir;
delete from Admin;
delete from KorisnickiNalog;
delete from Biblikutak;
delete from IzdKuca;
delete from Lokacija;
delete from MestoUDrzavi;
delete from Mesto;
delete from Drzava;
delete from Adresa;
*/

insert into Mesto(PosBr, NazivMesta) values
	(21000, 'Novi Sad'),
	(11000, 'Beograd'),
	(1000, 'Ljubljana'),
	(10000, 'Zagreb')
	;


insert into Drzava(OZND, NazivDrzave) values
	('XXX', 'Nepoznato'),
	('SRB', 'Srbija'),
	('HRV', 'Hrvatska'),
	('SLO', 'Slovenija'),
	('USA', 'Sjedinjene Američke Države'),
	('ENG', 'Engleska')
	;

insert into Adresa(Ulica, Broj) values
	('Blagoja Parovića', '9a'),
	('Vuka Karadžića', '1'),
	('Vuka Karadžića', '2'),
	('Jurija Gararina', '177'),
	('Zagrebačka avenija', '44'),
	('Žaucerjeva ulica', '2a'),
	('Knez Mijahlova', '14'),
	('Slovenska cesta', '33')
	;

insert into MestoUDrzavi(PosBr, OZND) values
	(21000, 'SRB'),
	(11000, 'SRB'),
	(1000, 'SLO'),
	(10000, 'SLO'),
	(10000, 'HRV')
	;

insert into Lokacija(Ulica, Broj, PosBr, OZND) values
	('Blagoja Parovića', '9a', 21000, 'SRB'),
	('Vuka Karadžića', '1', 21000, 'SRB'),
	('Vuka Karadžića', '1', 11000, 'SRB'),
	('Vuka Karadžića', '2', 11000, 'SRB'),
	('Jurija Gararina', '177', 11000, 'SRB'),
	('Zagrebačka avenija', '44', 10000, 'HRV'),
	('Žaucerjeva ulica', '2a', 1000, 'SLO'),
	('Knez Mijahlova', '14', 11000, 'SRB'),
	('Slovenska cesta', '33', 10000, 'HRV'),
	('Slovenska cesta', '33', 1000, 'SLO')
	;

insert into IzdKuca(Naziv, Ulica, Broj, PosBr, OZND) values
	('Pingvin', 'Vuka Karadžića', '2', 11000, 'SRB'),
	('Arhipelag', 'Vuka Karadžića', '2', 11000, 'SRB'),
	('Abalon', 'Jurija Gararina', '177', 11000, 'SRB'),
	('Jesenski i Turk', 'Zagrebačka avenija', '44', 10000, 'HRV'),
	('Beletrina', 'Slovenska cesta', '33', 1000, 'SLO')
	;

insert into Biblikutak(Naziv, DatOtv, DatZat, Ulica, Broj, PosBr, OZND) values
	('Prikutna Knjigozbirnica', '2023-10-11', null, 'Zagrebačka avenija', '44', 10000, 'HRV'),
	('Biblikutak', '2021-5-6', null, 'Vuka Karadžića', '1', 11000, 'SRB'),
	('Biblikutak', '2021-1-6', '2023-5-10', 'Blagoja Parovića', '9a', 21000, 'SRB'),
	('Kotna Knjižnica', '2022-1-3', null, 'Slovenska cesta', '33', 1000, 'SLO')
	;

insert into KorisnickiNalog(KorisnickoIme, Sifra, DatKreiranja, DatZatvaranja, TipNaloga) values
	('admin', 'd74ff0ee8da3b9806b18c877dbf29bbde50b5bd8e4dad7a3a725000feb82e8f1', '2023-1-28', null, 1),
	('bib', 'd74ff0ee8da3b9806b18c877dbf29bbde50b5bd8e4dad7a3a725000feb82e8f1', '2023-11-28', null, 2),
	('bibOtp', 'd74ff0ee8da3b9806b18c877dbf29bbde50b5bd8e4dad7a3a725000feb82e8f1', '2023-4-28', '2023-11-28', 2),
	('kurir', 'd74ff0ee8da3b9806b18c877dbf29bbde50b5bd8e4dad7a3a725000feb82e8f1', '2023-11-28', null, 3)
	;

insert into Admin(Ime, Prezime, DatRodj, DatZap, DatOtp, Ulica, Broj, PosBr, OZND) values
	('Admin', 'Adminović', '1997-11-15', '2023-1-28', null, null, null, null, null)
	;

insert into Kurir(Ime, Prezime, DatRodj, DatZap, DatOtp, IDAdmin, DatVr, Ulica, Broj, PosBr, OZND) values
	('Kuki', 'Dostavić', '2000-1-1','2023-11-28', null, 1, '2023-11-28 14:00:00','Vuka Karadžića', '2', 11000, 'SRB')
	;

insert into Bibliotekar(Ime, Prezime, DatRodj, DatZap, DatOtp, IDAdmin, DatVr, Ulica, Broj, PosBr, OZND) values
	('Bilja', 'Knjigović', '2000-1-1', '2023-11-28', null, 1, '2023-11-28 14:00:00', 'Vuka Karadžića', '2', 11000, 'SRB'),
	('Slavica', 'Neradović', '2000-1-1', '2023-4-28', '2023-11-28', 1, '2023-11-28 14:00:00', 'Vuka Karadžića', '2', 11000, 'SRB')
	;

insert into AdminKoristiNalog(ID, KorisnickoIme) values
	(1, 'admin')
	;

insert into KurirKoristiNalog(ID, KorisnickoIme) values
	(1, 'kurir')
	;
	
insert into BibliotekarKoristiNalog(ID, KorisnickoIme) values
	(1, 'bib'),
	(2, 'bibOtp')
	;

insert into Jezik(OZNJ, NazivJezika) values
	('SRB', 'Srpski'),
	('HRV', 'Hrvatski'),
	('SLO', 'Slovenački'),
	('ENG', 'Engleski'),
	('DEU', 'Nemački'),
	('FRA', 'Francuski')
	;

insert into Zanr(OZNZ, NazivZanra) values
	('KOMD', 'Komedija'),
	('DRAM', 'Drama'),
	('SIFI', 'Sci-Fi'),
	('TRIL', 'Triler'),
	('HORR', 'Horor'),
	('FICT', 'Fikcija'),
	('EP', 'Epska proza'),
	('POEZ', 'Poezija'),
	('ROM', 'Romantika')
	;

insert into Format(NazivFormata) values
	('A4'),
	('A5'),
	('1x1')
	;

insert into Autor(Ime, Prezime, DatRodj, Drzava, Biografija) values
	('Nepoznat', null, null, null, null),
	('Ivo', 'Andrić', '1892-10-9', 'SRB', 'Ivo Andrić (Dolac, kod Travnika, 9. oktobar 1892 — Beograd, 13. mart 1975) bio je srpski i jugoslovenski književnik i diplomata Kraljevine Jugoslavije. Godine 1961. dobio je Nobelovu nagradu za književnost „za epsku snagu kojom je oblikovao teme i prikazao sudbine ljudi tokom istorije svoje zemlje”.[10] Kao gimnazijalac, Andrić je bio pripadnik naprednog revolucionarnog pokreta protiv Austrougarske vlasti Mlada Bosna i strastveni borac za oslobođenje južnoslovenskih naroda od Austrougarske monarhije. U austrijskom Gracu je diplomirao i doktorirao, a vreme između dva svetska rata proveo je u službi u konzulatima i poslanstvima Kraljevine Jugoslavije u Rimu, Bukureštu, Gracu, Parizu, Madridu, Briselu, Ženevi i Berlinu. Bio je član Srpske akademije nauka i umetnosti u koju je primljen 1926. godine. Njegova najpoznatija dela su pored romana Na Drini ćuprija i Travnička hronika, Prokleta avlija, Gospođica i Jelena, žena koje nema. U svojim delima se uglavnom bavio opisivanjem života u Bosni za vreme osmanske vlasti.'),
	('Ernest', 'Hemingway', '1899-7-21', 'USA', 'Ernest Miler Hemingvej (engl. Ernest Miller Hemingway; Ouk Park, 21. jul 1899 — Kečum, 2. jul 1961) bio je američki pisac i novinar. Bio je pripadnik pariskog udruženja izgnanika dvadesetih godina dvadesetog veka i jedan od veterana Prvog svetskog rata, koji su kasnije bili poznati kao „izgubljena generacija“, kako ih je nazvala Gertruda Stajn. Dobio je Pulicerovu nagradu 1953. godine za svoj roman Starac i more, kao i Nobelovu nagradu za književnost 1954. godine. Svojim posebnim načinom pisanja koji karakterišu kratke rečenice, nasuprot stilu njegovog književnog suparnika Vilijama Foknera, Hemingvej je značajno uticao na razvoj lepe književnosti dvadesetog veka. Mnogi njegovi romani se danas smatraju klasičnim delima američke književnosti.'),
	('John Ronald Reuel', 'Tolkien', '1892-1-3', 'ENG', 'Džon Ronald Ruel Tolkin (engl. John Ronald Reuel Tolkien; Blumfontejn, 3. januar 1892 — Oksford, 2. septembar 1973) bio je engleski univerzitetski profesor, književnik i filolog. Tokin je bio profesor anglosaksonskog jezika na Oksfordskom univerzitetu u periodu od 1925. do 1945. godine, kao i profesor engleskog jezika i književnosti, takođe na Oksfordu, od 1945. do 1959. godine. Bavio se pisanjem epske fantastike, fantastike uopšte i poezije tokom celog života, i kroz njih je doživeo međunarodnu slavu. Van naučnih krugova, najpoznatiji je kao autor romana „Gospodar prstenova“, zatim njegovog prethodnika, „Hobita“, kao i velikog broja posthumno izdatih knjiga o istoriji zamišljenog sveta zvanog Arda, najviše jednog njenog kontinenta, Srednje zemlje, gde se odigrava radnja ova njegova dva najpoznatija romana. Velika popularnost i uticaj ovih dela su ustoličila Tolkina kao oca žanra moderne epske fantastike. Što se tiče naučnih krugova, bio je cenjeni leksikograf i stručnjak za anglosaksonski i staronordijski jezik. Bio je član udruženja pisaca The Inklings i blizak prijatelj K. S. Luisa.'),
	('Dhonielle', 'Clayton', '1983-1-1', 'USA', null),
	('Tiffany D.', 'Jackson', null, 'USA', null)
	;

insert into Knjiga(Naziv, BrIzd, GodIzd, VrIzd, BrStrana, VelicinaFonta, Korice, Ograniceno, Format) values
	('Na Drini Ćuprija', 1, 1945, null, 200, 14, 0, 0, 'A5'),					--1
	('Travnička hronika', 1, 1945, null, 250, 12, 1, 0, 'A5'),					--2
	('Prokleta avlija', 1, 1954, null, 126, 12, 1, 0, 'A5'),					--3
	('Starac i more', 1, 1952, null, 118, 12, 0, 0, 'A5'),						--4
	('Hobit', 1, 1937, null, 310, 12, 1, 0, 'A5'),								--5
	('Velika Knjiga', 1, 2023, null, 100, 16, 1, 1, 'A4'),						--6
	('Ep o Gilgamešu', 1, null, 'cca. 2700.-2650. p.n.e.', 80, 14, 1, 0, 'A5'),	--7
	('Whiteout', 2, 2022, null, 291, 12, 1, 0, 'A5')							--8
	;

insert into Pise(IDKnjiga, IDAutor) values
	(1, 2),
	(2, 2),
	(3, 2),
	(4, 3),
	(5, 4),
	(6, 1),
	(7, 1),
	(8, 5),
	(8, 6)
	;

insert into KnjigaNaJeziku(IDKnjiga, OZNJ) values
	(1, 'SRB'),
	(2, 'SRB'),
	(3, 'SRB'),
	(4, 'ENG'),
	(5, 'ENG'),
	(6, 'SRB'),
	(6, 'ENG'),
	(7, 'SRB'),
	(8, 'ENG')
	;

insert into PripadaZanru(IDKnjiga, OZNZ) values
	(1, 'DRAM'),
	(2, 'DRAM'),
	(3, 'TRIL'),
	(4, 'DRAM'),
	(5, 'FICT'),
	(6, 'KOMD'),
	(7, 'EP'),
	(8, 'DRAM'),
	(8, 'ROM')
	;

insert into IzdajeKnjigu(IDKnjiga, IDIK) values
	(1, 1),
	(2, 1),
	(3, 1),
	(4, 2),
	(5, 3),
	(6, 1),
	(7, 2), 
	(8, 2),
	(8, 3)
	;

insert into KnjigaULokalu(IDKnjiga, IDBK, DatVrIzmene, Koilcina) values
	(1, 1, '2022-1-1 14:00:00', 3),	
	(1, 1, '2022-1-1 14:01:00', 4),	
	(2, 1, '2022-1-1 14:00:00', 6),	
	(3, 1, '2022-1-1 14:00:00', 2),	
	(4, 1, '2022-1-1 14:00:00', 4),	
	(5, 1, '2022-1-1 14:00:00', 3),	
	(6, 1, '2022-1-1 14:00:00', 1),	
	(7, 1, '2022-1-1 14:00:00', 0),	
	(8, 1, '2022-1-1 14:00:00', 3),
	(1, 2, '2022-1-1 14:00:00', 1),
	(3, 2, '2022-1-1 14:00:00', 5),
	(4, 2, '2022-1-1 14:00:00', 3),
	(6, 2, '2022-1-1 14:00:00', 2),
	(8, 2, '2022-1-1 14:00:00', 2),
	(1, 3, '2022-1-1 14:00:00', 1),
	(2, 3, '2022-1-1 14:00:00', 0),
	(5, 3, '2022-1-1 14:00:00', 12),
	(6, 3, '2022-1-1 14:00:00', 2),
	(3, 4, '2022-1-1 14:00:00', 2),
	(1, 4, '2022-1-1 14:00:00', 3),
	(5, 4, '2022-1-1 14:00:00', 22)
	;

insert into Periodicnost(PeriodIzd, Ucestalost) values
	('Dnevni', 1),
	('Polunedeljni', 3),
	('Nedeljni', 7),
	('Dvonedeljni', 14),
	('Mesečni', 30),
	('Dvomesecni', 60),
	('Polugodišnji', 180),
	('Godišnji', 365),
	('Dvogodišnji', 730)
	;

insert into SerijskoStivo(Naziv, TipStiva, Format, Period) values
	('Blic', 1, 'A4', 'Dnevni'),
	('Politikin Zabavnik', 1, 'A4', 'Nedeljni'),
	('National Geographic', 2, 'A4', 'Mesečni')
	;

insert into SStivoNaJeziku(IDSStivo, OZNJ) values
	(1, 'SRB'),
	(2, 'SRB'),
	(3, 'SRB'),
	(3, 'ENG')
	;

insert into IzdajeSStivo(IDSStivo, IDIK) values
	(1, 1),
	(2, 2),
	(3, 3)
	;

insert into IzdanjeSStiva(IDSStivo, BrIzd, DatIzd, Cena) values
	(1, 1, '2023-1-1', 100.00),
	(1, 2, '2023-1-2', 100.00),
	(1, 3, '2023-1-3', 100.00),
	(1, 4, '2023-1-4', 100.00),
	(1, 5, '2023-1-5', 100.00),
	(1, 6, '2023-1-6', 100.00),
	(2, 1, '2023-2-1', 150.00),
	(2, 2, '2023-2-8', 150.00),
	(2, 3, '2023-2-15', 150.00),
	(2, 4, '2023-2-22', 150.00),
	(3, 1, '2023-2-22', 250.00),
	(3, 2, '2023-3-22', 250.00),
	(3, 3, '2023-4-22', 250.00),
	(3, 4, '2023-5-22', 250.00),
	(3, 5, '2023-6-22', 250.00),
	(3, 6, '2023-7-22', 250.00)
	;

insert into IzdSStivoULokalu(IDSStivo, BrIzd, IDBK, DatVrIzmene, Koilcina) values
	(1, 1, 1, '2023-2-1 15:00:00', 2),
	(1, 2, 1, '2023-2-2 15:00:00', 2),
	(1, 3, 1, '2023-2-3 15:00:00', 2),
	(1, 4, 1, '2023-2-4 15:00:00', 2),
	(1, 5, 1, '2023-2-5 15:00:00', 2),
	(1, 6, 1, '2023-2-6 15:00:00', 2),
	(2, 1, 1, '2023-2-1 15:00:00', 3),
	(2, 2, 1, '2023-2-8 15:00:00', 3),
	(3, 1, 1, '2023-3-1 15:00:00', 1),
	(3, 2, 1, '2023-4-1 15:00:00', 1),
	(3, 3, 1, '2023-5-1 15:00:00', 1),
	(3, 4, 1, '2023-6-1 15:00:00', 1),
	(3, 5, 1, '2023-7-1 15:00:00', 1),
	(1, 1, 2, '2023-2-1 15:00:00', 2),
	(1, 2, 2, '2023-2-2 15:00:00', 2),
	(1, 3, 2, '2023-2-3 15:00:00', 2),
	(1, 5, 2, '2023-2-5 15:00:00', 2),
	(1, 6, 2, '2023-2-6 15:00:00', 2),
	(2, 1, 2, '2023-2-1 15:00:00', 3),
	(3, 2, 2, '2023-4-1 15:00:00', 1),
	(3, 3, 2, '2023-5-1 15:00:00', 1),
	(3, 4, 2, '2023-6-1 15:00:00', 1),
	(3, 5, 2, '2023-7-1 15:00:00', 1),
	(1, 1, 3, '2023-2-1 15:00:00', 2),
	(1, 2, 3, '2023-2-2 15:00:00', 2),
	(1, 3, 3, '2023-2-3 15:00:00', 2),
	(1, 6, 3, '2023-2-6 15:00:00', 2),
	(2, 1, 3, '2023-2-1 15:00:00', 3),
	(2, 2, 3, '2023-2-8 15:00:00', 3),
	(3, 1, 3, '2023-3-1 15:00:00', 1),
	(3, 2, 3, '2023-4-1 15:00:00', 1),
	(3, 5, 3, '2023-7-1 15:00:00', 1)
	;

insert into RasporedjenBibliotekar(IDBib, IDBK, DatVr, DatOd, DatDo) values
	(1, 1, '2023-11-28 14:00:00', '2023-11-28', null)
	;