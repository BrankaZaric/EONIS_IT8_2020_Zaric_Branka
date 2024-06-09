import { Component, OnInit } from '@angular/core';
import { Proizvod } from 'src/app/shared/models/proizvod';
import { CartService } from './cart.service';
import { Router } from '@angular/router';
import { ShopService } from '../shop/shop.service';
import { AccountService } from '../account/account.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  korpa: Proizvod[] = [];
  dozvoljenaKolicinaMap: Map<number, boolean> = new Map();
  poruka: string | null = null;

  constructor(
            private cartService: CartService,
            private shopService: ShopService,  
            private accountService: AccountService,
            private router: Router) { }

  ngOnInit(): void {
    this.cartService.korpa$.subscribe(korpa => {
      this.korpa = korpa;
      // Inicijalizujte dozvoljenaKolicinaMap sa true za sve proizvode
      this.korpa.forEach(proizvod => this.dozvoljenaKolicinaMap.set(proizvod.proizvodID, true));
    });
  }

  incrementKolicina(proizvod: Proizvod) {
    // Provera dostupne količine
    this.shopService.getProizvod(proizvod.proizvodID).subscribe({
      next: p => {
        if (proizvod.kolicina < p.kolicina) {
          proizvod.kolicina++;
          this.cartService.sacuvajKorpu();
          this.dozvoljenaKolicinaMap.set(proizvod.proizvodID, true); // Postavlja se na true
        } else {
          this.dozvoljenaKolicinaMap.set(proizvod.proizvodID, false); // Postavlja se na false
        }
      },
      error: error => console.log(error)
    });
  }

  decrementKolicina(proizvod: Proizvod) {
    if (proizvod.kolicina > 1) {
      proizvod.kolicina--;
      this.cartService.sacuvajKorpu();
      this.dozvoljenaKolicinaMap.set(proizvod.proizvodID, true); 
    }
  }

  ukloniIzKorpe(proizvod: Proizvod) {
    this.cartService.ukloniIzKorpe(proizvod);
    this.dozvoljenaKolicinaMap.delete(proizvod.proizvodID);
  }

  getUkupnaCena() {
    return this.korpa.reduce((acc, proizvod) => acc + (proizvod.cena * proizvod.kolicina), 0);
  }

  nastaviNaPlacanje() {
    if (this.korpa.length === 0) {
      this.poruka = 'Nema proizvoda u korpi!';
      setTimeout(() => this.poruka = null, 3000); 
      return;
    }

    const userId = localStorage.getItem('userId');
    
    if (!userId) {
      console.log('Korisnik nije prijavljen!');
      // Možeš dodati redirekciju na login stranicu ili prikazati poruku korisniku
      this.router.navigate(['/login']);
      return;
    }
  
    const porudzbinaDTO = {
      datum: new Date(),
      status: 'Otvorena',
      iznos: this.getUkupnaCena() + 300,
      korisnikID: Number(userId)
    };
  
    this.cartService.kreirajPorudzbinu(porudzbinaDTO).subscribe({
      next: (createdOrder) => {
        console.log('Kreirana porudžbina:', createdOrder);
        const porudzbinaID = createdOrder.porudzbinaID;
        
        // Iteriramo kroz svaku stavku porudžbine i upisujemo je pojedinačno
        this.korpa.forEach(proizvod => {
          const stavkaPorudzbine = {
            cenaStavka: proizvod.cena,
            kolicinaStavka: proizvod.kolicina,
            proizvodID: proizvod.proizvodID,
            porudzbinaID: porudzbinaID
          };
    
          // Kreiramo jednu stavku porudžbine i upisujemo je
          this.cartService.kreirajStavkePorudzbine(stavkaPorudzbine).subscribe({
            next: () => {
              console.log('Stavka porudžbine uspešno upisana.');
            },
            error: error => {
              console.log('Greška prilikom kreiranja stavke porudžbine:', error);
            }
          });
        });
        
        // Nakon što smo upisali sve stavke porudžbine, možemo obaviti dodatne akcije kao što je čišćenje korpe ili preusmerenje korisnika
        //this.cartService.ukloniSveIzKorpe();
        localStorage.setItem('porudzbinaID', porudzbinaID.toString());
        this.router.navigate(['/checkout']);
      },
      error: error => {
        console.log('Greška prilikom kreiranja porudžbine:', error);
      }
    });
  }
  

}
