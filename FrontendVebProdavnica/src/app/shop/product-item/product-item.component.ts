import { Component, Input, OnInit } from '@angular/core';
import { CartService } from 'src/app/cart/cart.service';
import { Proizvod } from 'src/app/shared/models/proizvod';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent  {
  @Input() proizvod?: Proizvod;

  constructor(private cartService: CartService) {}

  isAdmin(): boolean {
    const isAdmin = localStorage.getItem('isAdmin');
    return isAdmin === "true";
  } 
  
  addToCart() {
    if (this.proizvod) {
      this.cartService.dodajUKorpu(this.proizvod, 1);
    }
  }
}
