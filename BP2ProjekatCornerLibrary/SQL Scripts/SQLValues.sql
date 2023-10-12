DELETE FROM REZERVACIJA;
DELETE FROM KNJIGAULOKALU;
DELETE FROM BIBLIKUTAK;
DELETE FROM KNJIGA;
DELETE FROM AUTOR;
DELETE FROM JEZIK;
DELETE FROM ZANR;

DELETE FROM CLAN;
DELETE FROM LOKACIJA;
DELETE FROM MESTOUDRZAVI;
DELETE FROM MESTO;
DELETE FROM DRZAVA;
DELETE FROM ADRESA;



insert into ADRESA (Ulica, Broj)
values
    ('Blagoja Parovica', '9a'),
    ('Fruskogorska', '3');

insert into MESTO (PosBr, NazivMesta)
values
    (21000, 'Novi Sad'),
    (11000, 'Beograd');

insert into DRZAVA (OZND, NazivDrzave)
values
    ('SRB', 'Srbija');

insert into MESTOUDRZAVI (PosBr, OZND)
values
    (21000, 'SRB'),
    (11000, 'SRB');

insert into LOKACIJA (Ulica, Broj, PosBr, OZND)
values
    ('Blagoja Parovica', '9a', 21000, 'SRB'),
    ('Fruskogorska', '3', 21000, 'SRB');


INSERT INTO CLAN (IDClan, KorisnickoIme, Sifra, Ime, Prezime, DatRodj, BrTel, Tip, Ulica, Broj, PosBr, OZND)
VALUES
    (1, 'Fish', 'd74ff0ee8da3b9806b18c877dbf29bbde50b5bd8e4dad7a3a725000feb82e8f1', 'Luka', 'Miletic', '1997-11-15', '031311223', 2, 'Blagoja Parovica', '9a', 21000, 'SRB');
    --(2, 'user2', HASHBYTES('SHA2_256', 'pass'), 'Jane', 'Smith', '1992-05-15', RAND() * 1000 + 1, FLOOR(RAND() * 3)),
    --(3, 'user3', HASHBYTES('SHA2_256', 'pass'), 'Michael', 'Johnson', '1985-09-22', RAND() * 1000 + 1, FLOOR(RAND() * 3)),
    --(4, 'user4', HASHBYTES('SHA2_256', 'pass'), 'Emily', 'Brown', '1998-12-10', RAND() * 1000 + 1, FLOOR(RAND() * 3)),
    --(5, 'user5', HASHBYTES('SHA2_256', 'pass'), 'David', 'Wilson', '1977-07-31', RAND() * 1000 + 1, FLOOR(RAND() * 3)),
    --(6, 'user6', HASHBYTES('SHA2_256', 'pass'), 'Olivia', 'Taylor', '1990-03-25', RAND() * 1000 + 1, FLOOR(RAND() * 3)),
    --(7, 'user7', HASHBYTES('SHA2_256', 'pass'), 'Daniel', 'Anderson', '1982-11-08', RAND() * 1000 + 1, FLOOR(RAND() * 3)),
    --(8, 'user8', HASHBYTES('SHA2_256', 'pass'), 'Sophia', 'Johnson', '1995-06-19', RAND() * 1000 + 1, FLOOR(RAND() * 3)),
    --(9, 'user9', HASHBYTES('SHA2_256', 'pass'), 'Jacob', 'Wilson', '1987-04-12', RAND() * 1000 + 1, FLOOR(RAND() * 3)),
    --(10, 'user10', HASHBYTES('SHA2_256', 'pass'), 'Isabella', 'Thompson', '1991-08-29', RAND() * 1000 + 1, FLOOR(RAND() * 3)),
    --(11, 'user1', HASHBYTES('SHA2_256', 'pass'), 'John', 'Doe', '1980-01-01', RAND() * 1000 + 1, FLOOR(RAND() * 3));

insert into AUTOR (IDAutor, Ime, Prezime, DatRodj, Biografija, OZND)
values
    (1, 'Mika', 'Antic', '1968-5-3', 'Mika Antic nije pisao sci-fi.', 'SRB');

insert into JEZIK (OZNJ, NazivJezika)
values
    ('SRB', 'Srpski');

insert into ZANR (OZNZ, NazivZanra)
values
    ('SIFI', 'Sci-Fi');

insert into KNJIGA (IDKnjiga, Naziv, GodIzd, BrIzd, Ograniceno)
values
    (1, 'Rat i Mir', '1999-1-1', 1, 0);

insert into BIBLIKUTAK (IDBK, Naziv, Ulica, Broj, PosBr, OZND)
values
    (1, 'Biblka', 'Fruskogorska', '3', 21000, 'SRB');

insert into KNJIGAULOKALU (IDKnjiga, IDBK, Kolicina)
values
    (1, 1, 5);

insert into REZERVACIJA (IDRez, IDKnjiga, IDClan, IDBK, DatVr, PotvrdioRez, DatVrPot, ZakljucioRez, DatVrZak)
values
    (1, 1, 1, 1, '2023-10-11', NULL, NULL, NULL, NULL);


--INSERT INTO KNJIGA (ID, Naziv, Autor, IzdKuca, DatIzd, Zanr, Jezik, Ograniceno)
--VALUES
--    (1, 'To Kill a Mockingbird', 'Harper Lee', 'Harper Perennial Modern Classics', CONVERT(DATE, '1960-07-11', 23), 'Fiction', 'English', 0),
--    (2, '1984', 'George Orwell', 'Signet Classic', CONVERT(DATE, '1949-06-08', 23), 'Dystopian', 'English', 1),
--    (3, 'Pride and Prejudice', 'Jane Austen', 'Penguin Classics', CONVERT(DATE, '1813-01-28', 23), 'Romance', 'English', 0),
--    (4, 'The Great Gatsby', 'F. Scott Fitzgerald', 'Scribner', CONVERT(DATE, '1925-04-10', 23), 'Fiction', 'English', 0),
--    (5, 'To the Lighthouse', 'Virginia Woolf', 'Harvest Books', CONVERT(DATE, '1927-05-05', 23), 'Modernist', 'English', 0),
--    (6, 'The Lord of the Rings', 'J.R.R. Tolkien', 'Allen & Unwin', CONVERT(DATE, '1954-07-29', 23), 'Fantasy', 'English', 0),
--    (7, 'Harry Potter and the Sorcerer''s Stone', 'J.K. Rowling', 'Bloomsbury Publishing', CONVERT(DATE, '1997-06-26', 23), 'Fantasy', 'English', 0),
--    (8, 'Moby-Dick', 'Herman Melville', 'Harper & Brothers', CONVERT(DATE, '1851-10-18', 23), 'Adventure', 'English', 0),
--    (9, 'The Chronicles of Narnia', 'C.S. Lewis', 'Geoffrey Bles', CONVERT(DATE, '1950-10-16', 23), 'Fantasy', 'English', 0),
--    (10, 'Crime and Punishment', 'Fyodor Dostoevsky', 'The Russian Messenger', CONVERT(DATE, '1866-01-01', 23), 'Fiction', 'Russian', 0),
--    (11, 'Brave New World', 'Aldous Huxley', 'Chatto & Windus', CONVERT(DATE, '1932-05-02', 23), 'Dystopian', 'English', 0),
--    (12, 'Jane Eyre', 'Charlotte Brontë', 'Smith, Elder & Co.', CONVERT(DATE, '1847-10-16', 23), 'Gothic', 'English', 0),
--    (13, 'The Hobbit', 'J.R.R. Tolkien', 'George Allen & Unwin', CONVERT(DATE, '1937-09-21', 23), 'Fantasy', 'English', 0),
--    (14, 'The Picture of Dorian Gray', 'Oscar Wilde', 'Ward, Lock & Co.', CONVERT(DATE, '1890-07-01', 23), 'Gothic', 'English', 0),
--    (16, 'Frankenstein', 'Mary Shelley', 'Lackington, Hughes, Harding, Mavor & Jones', CONVERT(DATE, '1818-01-01', 23), 'Gothic', 'English', 0),
--    (17, 'The Alchemist', 'Paulo Coelho', 'HarperOne', CONVERT(DATE, '1988-01-01', 23), 'Fiction', 'Portuguese', 0),
--    (18, 'The Adventures of Huckleberry Finn', 'Mark Twain', 'Chatto & Windus', CONVERT(DATE, '1884-12-10', 23), 'Adventure', 'English', 0),
--    (19, 'The Scarlet Letter', 'Nathaniel Hawthorne', 'Ticknor, Reed, and Fields', CONVERT(DATE, '1850-03-16', 23), 'Romance', 'English', 0),
--    (20, 'One Hundred Years of Solitude', 'Gabriel García Márquez', 'Harper & Row', CONVERT(DATE, '1967-05-30', 23), 'Magical Realism', 'Spanish', 0),
--    (21, 'The Brothers Karamazov', 'Fyodor Dostoevsky', 'The Russian Messenger', CONVERT(DATE, '1880-11-26', 23), 'Fiction', 'Russian', 0),
--    (22, 'The Divine Comedy', 'Dante Alighieri', 'Various', CONVERT(DATE, '1320-1-1', 23), 'Epic Poetry', 'Italian', 0),
--    (23, 'The Sun Also Rises', 'Ernest Hemingway', 'Scribner', CONVERT(DATE, '1926-10-22', 23), 'Fiction', 'English', 0),
--    (24, 'The Kite Runner', 'Khaled Hosseini', 'Riverhead Books', CONVERT(DATE, '2003-05-29', 23), 'Historical Fiction', 'English', 0),
--    (25, 'Gone with the Wind', 'Margaret Mitchell', 'Macmillan Publishers', CONVERT(DATE, '1936-06-30', 23), 'Historical Fiction', 'English', 0),
--    (46, 'Slaughterhouse-Five', 'Kurt Vonnegut', 'Delacorte Press/Seymour Lawrence', CONVERT(DATE, '1969-03-31', 23), 'Science Fiction', 'English', 0),
--    (47, 'The Little Prince', 'Antoine de Saint-Exupéry', 'Reynal & Hitchcock', CONVERT(DATE, '1943-04-06', 23), 'Children''s', 'French', 0),
--    (48, 'The Count of Monte Cristo', 'Alexandre Dumas', 'Pétion', CONVERT(DATE, '1844-08-28', 23), 'Adventure', 'French', 0),
--    (49, 'The Grapes of Wrath', 'John Steinbeck', 'The Viking Press', CONVERT(DATE, '1939-04-14', 23), 'Fiction', 'English', 0),
--    (50, 'The Catcher in the Rye', 'J.D. Salinger', 'Little, Brown and Company', CONVERT(DATE, '1951-07-16', 23), 'Fiction', 'English', 1);

--    INSERT INTO NOVINE (ID, NazivNov, PeriodIzd, Cena)
--VALUES
--    (1, 'Blic', 'Dnevni', 89.99),
--    (2, 'Politika', 'Dnevni', 99.99),
--    (3, 'Večernje novosti', 'Dnevni', 79.99),
--    (4, 'Kurir', 'Dnevni', 69.99),
--    (5, 'Danas', 'Dnevni', 89.99),
--    (6, 'NIN', 'Nedeljni', 199.99),
--    (7, 'Vreme', 'Nedeljni', 149.99),
--    (8, 'Auto Bild', 'Nedeljni', 129.99),
--    (9, 'Nacionalna revija', 'Nedeljni', 169.99),
--    (10, 'Halo oglasi', 'Nedeljni', 59.99),
--    (11, 'PC Press', 'Mesecni', 189.99),
--    (12, 'Nacionalna geografija', 'Mesecni', 149.99),
--    (13, 'Moja tajna', 'Mesecni', 99.99),
--    (14, 'Story', 'Mesecni', 79.99),
--    (15, 'National Enquirer', 'Mesecni', 109.99),
--    (16, 'Politikin zabavnik', 'Nedeljni', 129.99),
--    (17, 'Svet kompjutera', 'Mesecni', 149.99),
--    (18, 'Serbian Herald', 'Dnevni', 89.99),
--    (19, 'Ilustrovana politika', 'Nedeljni', 99.99),
--    (20, 'Krug dvojke', 'Nedeljni', 59.99);

--    INSERT INTO MAGAZIN (ID, NazivMag, PeriodIzd, Cena)
--VALUES
--    (1, 'National Geographic', 'Mesecni', 349.99),
--    (2, 'Time', 'Nedeljni', 299.99),
--    (3, 'Forbes', 'Nedeljni', 279.99),
--    (4, 'Vogue', 'Mesecni', 249.99),
--    (5, 'Wired', 'Mesecni', 199.99),
--    (6, 'GQ', 'Mesecni', 189.99),
--    (7, 'Cosmopolitan', 'Mesecni', 169.99),
--    (8, 'Sports Illustrated', 'Nedeljni', 219.99),
--    (9, 'Rolling Stone', 'Nedeljni', 179.99),
--    (10, 'New Yorker', 'Nedeljni', 249.99),
--    (11, 'People', 'Nedeljni', 199.99),
--    (12, 'Architectural Digest', 'Mesecni', 239.99),
--    (13, 'Scientific American', 'Mesecni', 219.99),
--    (14, 'Harper''s Bazaar', 'Mesecni', 199.99),
--    (15, 'Fortune', 'Nedeljni', 259.99),
--    (16, 'National Review', 'Nedeljni', 189.99),
--    (17, 'Smithsonian', 'Mesecni', 199.99),
--    (18, 'Food & Wine', 'Mesecni', 179.99),
--    (19, 'Wine Spectator', 'Mesecni', 169.99),
--    (20, 'Entertainment Weekly', 'Nedeljni', 209.99);


--    INSERT INTO CORNERLIBRARY (ID, Adresa, Grad, Drzava)
--VALUES
--    (1, '123 Main Street', 'New York', 'United States'),
--    (2, '456 Elm Avenue', 'London', 'United Kingdom'),
--    (3, '789 Oak Lane', 'Paris', 'France'),
--    (4, '987 Maple Road', 'Berlin', 'Germany'),
--    (5, '321 Pine Street', 'Tokyo', 'Japan'),
--    (6, '654 Cedar Avenue', 'Sydney', 'Australia'),
--    (7, '876 Walnut Lane', 'Rome', 'Italy'),
--    (8, '543 Birch Road', 'Moscow', 'Russia'),
--    (9, '210 Oakwood Drive', 'Toronto', 'Canada'),
--    (10, '753 Elm Street', 'Madrid', 'Spain'),
--    (11, '111 Willow Avenue', 'Belgrade', 'Serbia'),
--    (12, '222 Oak Street', 'Belgrade', 'Serbia'),
--    (13, '333 Elm Lane', 'Belgrade', 'Serbia'),
--    (14, '444 Pine Road', 'London', 'United Kingdom'),
--    (15, '555 Maple Street', 'London', 'United Kingdom'),
--    (16, '666 Birch Avenue', 'London', 'United Kingdom'),
--    (17, '777 Willow Lane', 'Paris', 'France'),
--    (18, '888 Oak Road', 'Paris', 'France'),
--    (19, '999 Elm Drive', 'Berlin', 'Germany'),
--    (20, '000 Pine Avenue', 'Berlin', 'Germany');

--    INSERT INTO BIBLIOTEKAR (ID, Username, Pass, Ime, Prezime, IDLib)
--VALUES
--    (1, 'user1', '5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8d4f12e54777c29ddc3f64a33', 'John', 'Doe', 5),
--    (2, 'user2', '5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8d4f12e54777c29ddc3f64a33', 'Jane', 'Smith', 12),
--    (3, 'user3', '5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8d4f12e54777c29ddc3f64a33', 'Michael', 'Johnson', 8),
--    (4, 'user4', '5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8d4f12e54777c29ddc3f64a33', 'Emily', 'Williams', 1),
--    (5, 'user5', '5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8d4f12e54777c29ddc3f64a33', 'David', 'Brown', 19),
--    (6, 'user6', '5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8d4f12e54777c29ddc3f64a33', 'Sarah', 'Taylor', 7),
--    (7, 'user7', '5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8d4f12e54777c29ddc3f64a33', 'Daniel', 'Anderson', 14),
--    (8, 'user8', '5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8d4f12e54777c29ddc3f64a33', 'Jessica', 'Thomas', 6),
--    (9, 'user9', '5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8d4f12e54777c29ddc3f64a33', 'Christopher', 'Lee', 3),
--    (10, 'user10', '5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8d4f12e54777c29ddc3f64a33', 'Amanda', 'Wang', 16);

--    insert into KNJIGAULOKALU(IDK, IDL, Kolicina)
--    Values
--        (1, 1, 3),
--        (2, 1, 3),
--        (3, 1, 3),
--        (4, 1, 3),
--        (5, 1, 3),
--        (6, 1, 3),
--        (7, 1, 3),
--        (8, 1, 3),
--        (9, 1, 3),
--        (10, 1, 3),
--        (11, 1, 3),
--        (1, 2, 3),
--        (2, 2, 3),
--        (3, 2, 3),
--        (4, 2, 3),
--        (5, 2, 3),
--        (6, 2, 3),
--        (7, 2, 3),
--        (8, 2, 3),
--        (9, 2, 3),
--        (10, 3, 3),
--        (11, 3, 3);

--    INSERT INTO ZAHTEVZAKNJIGU (NazK, Autor, Lokal)
--VALUES
--    ('The Shadow of the Wind', 'Carlos Ruiz Zafón', 1),
--    ('The Nightingale', 'Kristin Hannah', 14),
--    ('The Alchemist', 'Paulo Coelho', 3),
--    ('Gone Girl', 'Gillian Flynn', 10),
--    ('The Book Thief', 'Markus Zusak', 19);

--    INSERT INTO CLANZAHTEVAKNJIGU (Clan, NazK, Autor, Lokal)
--VALUES
--    (1, 'The Shadow of the Wind', 'Carlos Ruiz Zafón', 1),
--    (2, 'The Nightingale', 'Kristin Hannah', 14),
--    (3, 'The Alchemist', 'Paulo Coelho', 3),
--    (4, 'Gone Girl', 'Gillian Flynn', 10),
--    (7, 'The Book Thief', 'Markus Zusak', 19);


--    INSERT INTO ZAHTEVZANOVINE (NazN, Lokal)
--VALUES
--    ('Glas javnosti', 1),
--    ('Pravda', 17),
--    ('Novosti', 13),
--    ('Kraljevina', 14),
--    ('Svedok', 2),
--    ('Press', 18),
--    ('Borba', 10),
--    ('Beograd', 4),
--    ('Srbija', 16),
--    ('Vesti', 11),
--    ('NIN', 20);

--    INSERT INTO CLANZAHTEVANOVINE (Clan, NazN, Lokal)
--VALUES
--    (1, 'Glas javnosti', 1),
--    (2, 'Pravda', 17),
--    (3, 'Novosti', 13),
--    (4, 'Kraljevina', 14),
--    (5, 'Svedok', 2),
--    (6, 'Press', 18),
--    (7, 'Borba', 10),
--    (8, 'Beograd', 4),
--    (9, 'Srbija', 16),
--    (10, 'Vesti', 11),
--    (11, 'NIN', 20);

--    INSERT INTO ZAHTEVZAMAGAZIN (NazM, Lokal)
--VALUES
--    ('Nature Magazine', 9),
--    ('Tech Insider', 15),
--    ('Business Weekly', 3),
--    ('Fashion Trends', 8),
--    ('Science World', 12),
--    ('Design & Architecture', 5),
--    ('Health & Wellness', 19),
--    ('Sports Gazette', 7),
--    ('Music Review', 6),
--    ('Travel Discoveries', 1),
--    ('Art Enthusiast', 17),
--    ('Science Insights', 13),
--    ('Fashionista', 14),
--    ('Business Now', 2),
--    ('Cinema Insider', 18),
--    ('Nature''s Wonders', 10),
--    ('Culinary Delights', 4),
--    ('Wine Connoisseur', 16),
--    ('Entertainment Spectator', 11),
--    ('Technology Trends', 20);
    
--    INSERT INTO CLANZAHTEVAMAGAZIN (Clan, NazM, Lokal)
--VALUES
--    (1, 'Nature Magazine', 9),
--    (6, 'Tech Insider', 15),
--    (2, 'Business Weekly', 3),
--    (7, 'Fashion Trends', 8),
--    (3, 'Science World', 12),
--    (4, 'Design & Architecture', 5),
--    (5, 'Health & Wellness', 19),
--    (8, 'Sports Gazette', 7);

--    INSERT INTO ISPUNJENZAHTEVZAKNJIGU (Clan, IZKnjiga, IZLokal)
--    VALUES
--    (1, 2, 3);

--INSERT INTO ISTORIJAREZERVACIJA (Clan, Knjiga, DatumUzeo, DatumVratio)
--VALUES
--    (1, 12, '2022-12-25', '2023-01-05'),
--    (2, 6, '2023-02-15', '2023-02-25'),
--    (3, 18, '2022-11-10', '2022-11-20'),
--    (4, 10, '2023-03-05', '2023-03-15'),
--    (5, 3, '2023-04-20', '2023-04-30'),
--    (6, 16, '2022-10-15', '2022-10-25'),
--    (7, 1, '2022-12-05', '2022-12-15'),
--    (8, 8, '2022-09-10', '2022-09-20'),
--    (9, 11, '2023-01-15', '2023-01-25'),
--    (10, 25, '2023-02-05', '2023-02-15'),
--    (11, 20, '2022-11-20', '2022-11-30'),
--    (1, 17, '2023-04-05', '2023-04-15'),
--    (2, 9, '2022-10-10', '2022-10-20'),
--    (3, 12, '2022-12-15', '2022-12-25'),
--    (4, 5, '2023-01-25', '2023-02-04'),
--    (5, 13, '2023-03-05', '2023-03-15'),
--    (6, 7, '2022-09-20', '2022-09-30'),
--    (7, 2, '2022-11-05', '2022-11-15'),
--    (8, 15, '2023-04-10', '2023-04-20'),
--    (9, 21, '2022-10-15', '2022-10-25'),
--    (10, 14, '2022-12-25', '2023-01-04'),
--    (11, 24, '2023-02-10', '2023-02-20'),
--    (1, 19, '2022-11-15', '2022-11-25'),
--    (2, 4, '2023-03-15', '2023-03-25'),
--    (3, 23, '2023-04-20', '2023-04-30'),
--    (4, 25, '2022-09-15', '2022-09-25'),
--    (5, 8, '2022-12-10', '2022-12-20'),
--    (6, 2, '2023-02-15', '2023-02-25'),
--    (7, 17, '2022-10-20', '2022-10-30'),
--    (8, 3, '2022-12-05', '2022-12-15'),
--    (9, 11, '2023-04-10', '2023-04-20'),
--    (10, 6, '2022-09-25', '2022-10-05'),
--    (11, 14, '2022-11-15', '2022-11-25'),
--    (1, 21, '2023-01-20', '2023-01-30'),
--    (2, 16, '2023-03-15', '2023-03-25'),
--    (3, 9, '2022-10-20', '2022-10-30'),
--    (4, 7, '2022-12-15', '2022-12-25'),
--    (5, 1, '2023-02-10', '2023-02-20'),
--    (6, 18, '2022-11-15', '2022-11-25'),
--    (7, 13, '2023-04-10', '2023-04-20'),
--    (8, 25, '2022-09-25', '2022-10-05'),
--    (9, 20, '2022-12-15', '2022-12-25'),
--    (10, 4, '2023-02-20', '2023-03-02'),
--    (11, 24, '2023-04-20', '2023-04-30'),
--    (1, 19, '2022-10-15', '2022-10-25'),
--    (2, 8, '2022-12-10', '2022-12-20'),
--    (3, 2, '2023-02-15', '2023-02-25'),
--    (4, 17, '2022-10-20', '2022-10-30'),
--    (5, 3, '2022-12-05', '2022-12-15'),
--    (6, 11, '2023-04-10', '2023-04-20'),
--    (7, 6, '2022-09-25', '2022-10-05');


--    INSERT INTO REZERVACIJA (Clan, Knjiga, Lokal, Datum)
--VALUES
--    (1, 12, 8, '2022-12-25'),
--    (5, 4, 16, '2022-10-01'),
--    (1, 17, 10, '2022-07-12'),
--    (8, 21, 7, '2023-01-09'),
--    (1, 6, 15, '2022-08-05'),
--    (2, 9, 18, '2022-11-19'),
--    (9, 22, 4, '2022-09-30'),
--    (11, 2, 13, '2022-06-23'),
--    (1, 14, 1, '2023-05-02'),
--    (4, 23, 6, '2022-07-28'),
--    (6, 16, 19, '2023-04-11'),
--    (3, 11, 12, '2022-07-02'),
--    (8, 20, 2, '2023-03-20'),
--    (10, 25, 9, '2022-09-01'),
--    (5, 5, 17, '2023-02-12'),
--    (9, 19, 11, '2022-08-19'),
--    (1, 3, 3, '2022-12-02'),
--    (7, 8, 5, '2022-10-15'),
--    (4, 15, 14, '2023-01-27'),
--    (11, 1, 20, '2022-06-10'),
--    (1, 10, 8, '2022-11-25'),
--    (2, 18, 16, '2022-09-06'),
--    (1, 7, 10, '2022-08-02'),
--    (8, 21, 7, '2023-02-03'),
--    (5, 13, 15, '2022-10-20'),
--    (9, 24, 4, '2022-07-15'),
--    (1, 2, 13, '2023-03-08'),
--    (1, 16, 8, '2022-09-15'),
--    (3, 12, 1, '2022-10-05'),
--    (7, 4, 14, '2022-12-18');
