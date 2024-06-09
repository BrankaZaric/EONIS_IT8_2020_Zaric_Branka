import { Component, OnInit } from '@angular/core';
import { Router, NavigationExtras } from '@angular/router';
import { CartService } from '../cart/cart.service';
import { PorudzbinaDTO } from '../shared/models/porudzbina';
import { FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../account/account.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {
  ukupanIznos: number = 0;
  iznosIsporuke: number = 300; // Fiksni iznos isporuke
  adresa: string[]=[]

  constructor(
      private router: Router,
      private fb: FormBuilder, 
      private cartService: CartService,
      private accountService: AccountService) { }

  checkoutForm = this.fb.group({
    addressForm: this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      street: ['', Validators.required],
      streetNumber: ['', Validators.required],
      city: ['', Validators.required],
      zipcode : ['', Validators.required],
      phoneNumber: ['', Validators.required]
    }),
    deliveryForm: this.fb.group({
      deliveryMethod: ['', Validators.required]
    }),
    paymentForm: this.fb.group({
      nameOnCard: ['', Validators.required]
    }),
  })

  ngOnInit(): void {
    this.getAddress();
    
    const porudzbinaID = localStorage.getItem('porudzbinaID');
    if (porudzbinaID) {
      this.cartService.getPorudzbinaByID(Number(porudzbinaID)).subscribe({
        next: (porudzbina: PorudzbinaDTO) => {
          this.ukupanIznos = porudzbina.iznos - 300;
          console.log('Podaci o porudžbini:', porudzbina);
        },
        error: (error) => {
          console.log('Greška prilikom dobijanja podataka o porudžbini:', error);
        }
      });
    } else {
      console.log('Nema podataka o porudžbini u localStorage.');
    }
  }


    getAddress() {
      this.accountService.getKorisnikById().subscribe({
        next: korisnik => {
          console.log('Korisnik podaci:', korisnik);  

          if (korisnik) {
            const parts: string[] = korisnik.adresa.split(', ');
    
            if (parts && parts.length === 4) {
              this.adresa = parts;
    
              this.checkoutForm.get('addressForm')?.patchValue({
                street: this.adresa[0],
                streetNumber: this.adresa[1],
                zipcode: this.adresa[2],
                city: this.adresa[3],
                firstName: korisnik.ime,
                lastName: korisnik.prezime,
                phoneNumber: korisnik.telefon
              });
            } else {
              console.error('Adresa nije u očekivanom formatu');
            }
          } else {
            console.log('Nema korisnika');
          }
        },
        error: err => {
          console.error('Greška prilikom dobijanja korisnika:', err);
        }
      });
    }
    
    
}
