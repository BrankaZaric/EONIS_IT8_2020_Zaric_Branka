import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CartService } from 'src/app/cart/cart.service';
import { Proizvod } from 'src/app/shared/models/proizvod';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.scss']
})
export class CheckoutReviewComponent implements OnInit {
  @Input() appStepper?: CdkStepper
  
  korpa: Proizvod[] = [];

  constructor(private cartService: CartService, private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.cartService.korpa$.subscribe(korpa => {
      this.korpa = korpa;
    });
  }

  async createPaymentIntent() {
    try {
      const response = await this.cartService.createPaymentIntent();
      this.snackBar.open('Payment Intent Created or Updated', 'Close', {
        duration: 3000, 
      });
      this.appStepper?.next();
    } catch (error) {
      console.error('Error creating or updating payment intent:', error);
      this.snackBar.open('Error creating or updating payment intent', 'Close', {
        duration: 3000, 
      });
    }
  }

}
