import { Administrator } from "./administrator"
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

  export interface ProizvodUpdate {
    proizvodID: number;
    nazivProizvoda: string;
    opis: string;
    cena: number;
    kolicina: number;
    slikaURL: string;
    kategorija: Kategorija; 
  }
  