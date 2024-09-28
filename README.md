RAZVOJ VEB APLIKACIJE ZA PODRŠKU POSLOVANJU PRODAVNICE DEČIJIH IGRAČAKA I EDUKATIVNIH MATERIJALA

Koraci razvoja:
1. prikupljanjem korisničkih zahteva napravljen je dijagram slučaja upotrebe, klasa i sekvenci
2. baza podataka sadrži tabele korisnik, administrator, kategorija proizvoda, proizvod, porudžbina i stavka porudžbine u skladu sa klasama na dijagramu klasa; definisano je automatsko generisanje primarnog ključa, kao i ostala potrebna ograničenja; kreiran je triger kao deo biznis logike
3. za svaku tabelu kreirane su CRUD operacije, kao i dodatne operaxije koje će zadovoljiti biznis logiku sistema
4. kreiranje klijentske strane aplikacije odnosno korisničkog interfejsa

* Backend deo aplikacije (serverska strana) razvijen je upotrebom .NET tehnologije u Visual Studio okruženju, dok je frontend deo (klijentska strana) razvijen upotrebom Angular-a u Visual Studio Code okruženju

* U sistemu postoje dve uloge korisnika:
  1. korisnik
  2. administrator
