import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/account/account.service';
import { CartService } from 'src/app/cart/cart.service';
import { KorisnikUpdate } from 'src/app/shared/models/user';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent {
  @Input() checkoutForm?: FormGroup;
  
  constructor(private accountService: AccountService, private cartService: CartService, private router: Router,){}

    saveAddress() {
      const userId = localStorage.getItem('userId');
    
      if (userId != null) {
        const userIdNumber = Number(userId);
    
        const korisnikUpdate: KorisnikUpdate = {
          korisnikID: userIdNumber,
          ime: this.checkoutForm?.get('addressForm')?.get('firstName')?.value || '',
          prezime: this.checkoutForm?.get('addressForm')?.get('lastName')?.value || '',
          adresa: this.checkoutForm?.get('addressForm')?.get('street')?.value + ', ' +
                  this.checkoutForm?.get('addressForm')?.get('streetNumber')?.value + ', ' +
                  this.checkoutForm?.get('addressForm')?.get('zipcode')?.value + ', ' +
                  this.checkoutForm?.get('addressForm')?.get('city')?.value,
          telefon: this.checkoutForm?.get('addressForm')?.get('phoneNumber')?.value || ''
        };
    
        this.accountService.updateKorisnik(korisnikUpdate).subscribe({
          next: () => {
            console.log('Adresa uspešno sačuvana');
            this.checkoutForm?.get('addressForm')?.reset(this.checkoutForm?.get('addressForm')?.value);
          },
          error: err => {
            console.error('Greška prilikom čuvanja adrese:', err);
          }
        });
      }
    }


    deletePorudzbina() {
      const porudzbinaID = localStorage.getItem('porudzbinaID');
      if (porudzbinaID) {
        this.cartService.deletePorudzbinaByID(Number(porudzbinaID)).subscribe({
          next: () => {
            console.log('Stavke i porudžbina uspešno obrisane');
            localStorage.removeItem('porudzbinaID');
            this.router.navigate(['/cart']);
          },
          error: (error) => {
            console.log('Greška prilikom brisanja stavki i porudžbine:', error);
          }
        });
      }
    }
}
