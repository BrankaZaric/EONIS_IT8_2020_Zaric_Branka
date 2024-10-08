# RAZVOJ VEB-APLIKACIJE ZA PODRŠKU POSLOVANJU PRODAVNICE DEČIJIH IGRAČAKA I EDUKATIVNIH MATERIJALA

## Koraci razvoja

1. prikupljanjem korisničkih zahteva napravljen je dijagram slučaja upotrebe, klasa i sekvenci
2. analizirano je trenutno stanje na tržištu, kao i uporedna analiza već postojećih veb-prodavnica za prodaju dečijih igračaka (Dexy Co Kids, Pertini Toys, Toyzzz), uočeni su nedostaci i dat predlog za poboljšanje
3. baza podataka sadrži tabele korisnik, administrator, kategorija proizvoda, proizvod, porudžbina i stavka porudžbine u skladu sa klasama na dijagramu klasa; definisano je automatsko generisanje primarnog ključa, kao i ostala potrebna ograničenja; kreiran je triger kao deo biznis logike
4. za svaku tabelu kreirane su CRUD operacije, kao i dodatne operaxije koje će zadovoljiti biznis logiku sistema
5. kreiranje klijentske strane aplikacije odnosno korisničkog interfejsa
---

## Korišćene tehnologije
- za kreiranje dijagrama korišćen je online alat **GenMyModel**
- baza podataka kreirana je u **Microsoft SQL Server Management-u**
- backend deo aplikacije (serverska strana) razvijen je upotrebom **.NET** tehnologije u **Visual Studio** okruženju
- frontend deo (klijentska strana) razvijen upotrebom **Angular-a** u **Visual Studio Code** okruženju
- proces plaćanja razvijen je upotrebom **Stripe-a**
---

## Opis aplikacije 

* U sistemu postoje dve uloge korisnika:
  1. korisnik
  2. administrator

S obzirom da imaju ista obeležja ono što ih razlikuje jesu funkcionalnosti koje mogu izvršavati u okviru sistema. Obezbeđena je autorizacija i autentifikacija korisnika, pa se prijavom u sistem pomoću korisničkog imena i lozinke jasno razdvajaju uloge.
Prva stranica koja se prikazuje na sajtu jeste *Početna stranica* na kojoj je dat detaljniji opis značaja dečijih igračaka i edukativnih materijala za razvoj dece (emocionalni, kognitivni, motorički, socijalni razvoj). Pored te stranice postoji glavna stranica, odnosno *Shop stranica* na kojoj je dat prikaz svih proizvoda, kao i neke dodatne funkcionalnosti koje će biti opisane u nastavku.

| FUNKCIONALNOSTI | Opis  |
|-----------------|----------|
| prijava u sistem    | Korisnik i administrator vrše prijavu u sistem putem korisničkog imena i lozinke.   |
| registracija   | Ukoliko korisnik nema nalog moguće je izvršiti registraciju popunjavanjem odgovarajuće forme.   |
| pregled proizvoda   | Korisnik i administrator mogu pregledati sve dostupne proizvode u okviru Shop stranice. Omogućena je paginacija, kako bi proizvodi bili pregledni za prikaz, a korisnik ima mogućnost da prelista stranice.   |
| sortiranje proizvoda   | Proizvode je moguće sortirati po nazivu, po ceni - od najjeftinijeg do najskupljeg ili obrnuto, ali moguće je i prikazati sve dostupne proizvode.   |
| pretraga proizvoda   | Unošenjem slova ili konkretne reči u polje za pretragu izlistavaju se proizvodi ukoliko su dostupni.  |
| kategorija proizvoda   | Odabirom određene kategorije, prikazuju se proizvodi koji im pripadaju.   |
| pregled detalja proizvoda   | Prilikom pregleda proizvoda, prelaskom preko određenog proizvoda nude se opcije Add to cart i View. Odabirom View opcije moguće je pregledati detaljniji opis proizvoda, cenu, dodati proizvod u korpu i slično   |
| pregled korpe   | U okviru navigacinog bara postoji simbol korpe sa brojem proizvoda koji su dodati u korpu. Prilikom provere dodatih proizvoda u korpu moguće je ažurirati stanje odnosno smanjiti ili dodati količinu određenog proizvoda, kao i ukloniti određeni proizvod.  |
| plaćanje   | Ukoliko se korisnik odluči da potvrdi porudžbinu i izvrši plaćanje, prelaskom na tu funkciju popunjavaju se forme u tri koraka. Prvi korak podrazumeva popunjavanje adrese i podataka korisnika, potom se vrši konačni pregled porudžbine, dok treći korak obuhvata popunjavanje podataka sa kartice koja može biti validna ili nevalidna. Ukoliko je validna, porudžbina će biti prihvaćena, dok se u suprotnom dobija poruka o problemu.   |
| upravljanje proizvodima   | Administrator je u mogućnosti da izvrši dodavanje novog proizvoda, ažuriranje već postojećeg ili brisanje proizvoda.   |
| upravljanje korisnicima   | Administrator takođe može pregledati korisnike, kao i porudžbine koje su oni izvršili.   |
| pregled porudžbina   | Pored svih porudžbina koje su korisnici kreirali, administrator može pregledati i porudžbine koje su plaćene.   |


Veb-aplikaciju je moguće proširiti sa dodatnim funkcionalnostima, poboljšanjima korisničkog interfejsa, ali na primer i povezivanjem sa eksternim API-jima.







