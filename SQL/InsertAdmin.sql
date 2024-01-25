insert into KorisnickiNalog(KorisnickoIme, Sifra, DatKreiranja, DatZatvaranja, TipNaloga) values
	('admin', 'd74ff0ee8da3b9806b18c877dbf29bbde50b5bd8e4dad7a3a725000feb82e8f1', '2023-1-28', null, 1);

insert into Admin(Ime, Prezime, DatRodj, DatZap, DatOtp, Ulica, Broj, PosBr, OZND) values
	('Admin', 'Adminović', '1997-11-15', '2023-1-28', null, null, null, null, null)
	;

insert into AdminKoristiNalog(ID, KorisnickoIme) values
	(1, 'admin')
	;