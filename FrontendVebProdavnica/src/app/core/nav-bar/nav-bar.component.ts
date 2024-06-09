import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { CartService } from 'src/app/cart/cart.service';
import { Administrator } from 'src/app/shared/models/administrator';
import { Proizvod } from 'src/app/shared/models/proizvod';
import { ShopParams } from 'src/app/shared/models/shopParams';
import { User } from 'src/app/shared/models/user';
import { ShopService } from 'src/app/shop/shop.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  totalItems: number = 0;

  constructor(public accountService: AccountService, private cartService: CartService) { }

  ngOnInit(): void {
    
    this.cartService.totalItems$.subscribe(total => {
      this.totalItems = total;
    });
  }

  isAdmin(): boolean {
    const isAdmin = localStorage.getItem('isAdmin');
    return isAdmin === "true";
} 

  getUserName(user: User | Administrator): string {
    if ('korisnickoIme' in user) {
      return user.korisnickoIme;
    } else if ('korisnickoImeAdmin' in user) {
      return user.korisnickoImeAdmin;
    } else {
      return '';
    }
  }
  
}
