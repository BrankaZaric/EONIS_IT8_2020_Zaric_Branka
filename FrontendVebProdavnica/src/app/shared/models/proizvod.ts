import { Administrator, AdministratorLozinka } from "./administrator"
import { Kategorija } from "./kategorija"

export interface Proizvod {
    proizvodID: number
    nazivProizvoda: string
    opis: string
    cena: number
    kolicina: number
    slikaURL: string
    kategorijaID: number
    kategorija: Kategorija
    adminID: number
    administrator: Administrator
  }

  export class ProizvodUpdate {
    proizvodID: number;
    nazivProizvoda: string;
    opis: string;
    cena: number;
    kolicina: number;
    slikaURL: string;
    kategorijaID: number;
    kategorija: Kategorija;
    adminID: number;
    administrator: AdministratorLozinka;
    
    constructor(
      proizvodID: number,
      nazivProizvoda: string,
      opis: string,
      cena: number,
      kolicina: number,
      slikaURL: string,
      kategorijaID: number,
      kategorija: Kategorija,
      adminID: number,
      administrator: AdministratorLozinka
    ){
      this.proizvodID = proizvodID;
      this.nazivProizvoda = nazivProizvoda;
      this.opis = opis;
      this.cena = cena;
      this.kolicina = kolicina;
      this.slikaURL = slikaURL;
      this.kategorijaID = kategorijaID;
      this.kategorija = kategorija;
      this.adminID = adminID;
      this.administrator = administrator;
    }
}

export class ProizvodCreation {
  nazivProizvoda: string;
  opis: string;
  cena: number;
  kolicina: number;
  slikaURL: string;
  kategorijaID: number;
  adminID: number;
  
  constructor(
    nazivProizvoda: string,
    opis: string,
    cena: number,
    kolicina: number,
    slikaURL: string,
    kategorijaID: number,
    adminID: number
  ){
    this.nazivProizvoda = nazivProizvoda;
    this.opis = opis;
    this.cena = cena;
    this.kolicina = kolicina;
    this.slikaURL = slikaURL;
    this.kategorijaID = kategorijaID;
    this.adminID = adminID;
  }
}
  
