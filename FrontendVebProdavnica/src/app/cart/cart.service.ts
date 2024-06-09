import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, switchMap } from 'rxjs';
import { Proizvod } from 'src/app/shared/models/proizvod';
import { PorudzbinaCreationDTO, PorudzbinaDTO, PorudzbinaPaymentDTO } from '../shared/models/porudzbina';
import { StavkaPorudzbineCreationDTO } from '../shared/models/stavkaPorudzbine';
import { KorisniBezLozinke } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private korpa: Proizvod[] = [];
  private korpaSubject = new BehaviorSubject<Proizvod[]>([]);
  private totalItemsSubject = new BehaviorSubject<number>(0);

  korpa$ = this.korpaSubject.asObservable();
  totalItems$ = this.totalItemsSubject.asObservable();

  private paymentUrl = 'https://localhost:7295/api/payment';
  private porudzbinaUrl = 'https://localhost:7295/api/porudzbina';
  private stavkaPorudzbineUrl = 'https://localhost:7295/api/stavkaPorudzbine';

  constructor(private http: HttpClient) {
    const storedCart = localStorage.getItem('korpa');
    if (storedCart) {
      this.korpa = JSON.parse(storedCart);
      this.korpaSubject.next(this.korpa);
      this.updateTotalItems();
    }
  }

  createPaymentIntent() {
    const porudzbinaId = Number(localStorage.getItem('porudzbinaID'));
    if (!porudzbinaId) {
      console.error('Nema porudzbinaID u localStorage');
      return;
    }
  
    const url = `${this.paymentUrl}/${porudzbinaId}`;
    return this.http.post<PorudzbinaPaymentDTO>(url, {}).toPromise()
      .then(response => {
        console.log('Payment Intent Created or Updated:', response);
        return response;
      })
      .catch(error => {
        console.error('Error creating or updating payment intent:', error);
        throw error;
      });
  }

  sacuvajKorpu() {
    localStorage.setItem('korpa', JSON.stringify(this.korpa));
    this.korpaSubject.next(this.korpa);
    this.updateTotalItems();
  }

  dodajUKorpu(proizvod: Proizvod, kolicina: number) {
    const existingProduct = this.korpa.find(p => p.proizvodID === proizvod.proizvodID);
    if (existingProduct) {
      existingProduct.kolicina += kolicina;
    } else {
      proizvod.kolicina = kolicina;
      this.korpa.push(proizvod);
    }
    this.sacuvajKorpu();
  }

  ukloniIzKorpe(proizvod: Proizvod) {
    this.korpa = this.korpa.filter(p => p.proizvodID !== proizvod.proizvodID);
    this.sacuvajKorpu();
  }

  ukloniSveIzKorpe() {
    localStorage.removeItem('korpa');
    this.korpa = [];
    this.korpaSubject.next(this.korpa);
    this.updateTotalItems();
  }


  private updateTotalItems() {
    const totalItems = this.korpa.length;
    this.totalItemsSubject.next(totalItems);
  }

  kreirajPorudzbinu(porudzbina: any): Observable<any> {
    return this.http.post<any>(this.porudzbinaUrl, porudzbina);
  }

  kreirajStavkePorudzbine(stavkaPorudzbine: StavkaPorudzbineCreationDTO): Observable<any> {
    return this.http.post<any>(this.stavkaPorudzbineUrl, stavkaPorudzbine);
  }

  getPorudzbinaByID(porudzbinaID: number): Observable<PorudzbinaDTO> {
    return this.http.get<PorudzbinaDTO>(`${this.porudzbinaUrl}/${porudzbinaID}`);
  }

  deletePorudzbinaByID(porudzbinaID: number): Observable<void>{
    return this.http.delete<void>(`${this.porudzbinaUrl}/${porudzbinaID}`);
  }

}
