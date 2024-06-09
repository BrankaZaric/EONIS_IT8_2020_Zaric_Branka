export interface User {
    korisnikID: number;
    ime: string;
    prezime: string;
    email: string;
    korisnickoIme: string;
    lozinka: string;
    adresa?: string;
    telefon?: string; 
    token: string;
  }
  
export class KorisnikCreation {
  ime: string;
  prezime: string;
  email: string;
  korisnickoIme: string;
  lozinka: string;
  adresa?: string;
  telefon?: string; 

  constructor(
    ime: string,
    prezime: string,
    email: string,
    korisnickoIme: string,
    lozinka: string,
    adresa?: string,
    telefon?: string 
  ) {
    this.ime = ime;
    this.prezime = prezime;
    this.email = email;
    this.korisnickoIme = korisnickoIme;
    this.lozinka = lozinka;
    this.adresa = adresa;
    this.telefon = telefon;
  }
}

export class KorisnikDTO {
  ime: string;
  prezime: string;
  email: string;
  korisnickoIme: string;
  adresa: string;
  telefon: string; 

  constructor(
    ime: string,
    prezime: string,
    email: string,
    korisnickoIme: string,
    adresa: string,
    telefon: string 
  ) {
    this.ime = ime;
    this.prezime = prezime;
    this.email = email;
    this.korisnickoIme = korisnickoIme;
    this.adresa = adresa;
    this.telefon = telefon;
  }
}
export class KorisniBezLozinke {
  korisnikID: number;
  ime: string;
  prezime: string;
  adresa: string;
  telefon: string; 
  email: string;
  korisnickoIme: string;

  constructor(
    korisnikID: number,
    ime: string,
    prezime: string,
    email: string,
    korisnickoIme: string,
    adresa: string,
    telefon: string 
  ) {
    this.korisnikID = korisnikID;
    this.ime = ime;
    this.prezime = prezime;
    this.email = email;
    this.korisnickoIme = korisnickoIme;
    this.adresa = adresa;
    this.telefon = telefon;
    
  }
}

export class KorisnikUpdate {
  korisnikID: number;
  ime: string;
  prezime: string;
  adresa: string;
  telefon: string; 

  constructor(
    korisnikID: number,
    ime: string,
    prezime: string,
    adresa: string,
    telefon: string 
  ) {
    this.korisnikID = korisnikID;
    this.ime = ime;
    this.prezime = prezime;
    this.adresa = adresa;
    this.telefon = telefon;
  }
}