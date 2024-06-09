import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { Stripe, StripeCardCvcElement, StripeCardExpiryElement, StripeCardNumberElement, loadStripe } from '@stripe/stripe-js';
import { CartService } from 'src/app/cart/cart.service';
import { PorudzbinaDTO } from 'src/app/shared/models/porudzbina';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit{
  @Input() checkoutForm?: FormGroup;
  @ViewChild('cardNumber') cardNumberElement?: ElementRef;
  @ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
  @ViewChild('cardCvc') cardCvcElement?: ElementRef;
  stripe: Stripe | null = null;
  cardNumber?: StripeCardNumberElement;
  cardExpiry?: StripeCardExpiryElement;
  cardCvc?: StripeCardCvcElement;
  cardNumberComplete = false;
  cardExpiryComplete = false;
  cardCvcComplete = false;
  cardErrors: any;
  secret: string = ''
  loading = false;  // State to handle loading

  constructor(private cartService: CartService, private router: Router){}

  ngOnInit(): void {
    loadStripe('pk_test_51PP2WYRoAWGojdnAdQWd12mBGsr6rQQjIoS8Ct6kdb665V9wPorbfUdNuLrtti3hUFKr7141TRoEB0kri9BYgv9m00dAM4wEFQ').then(stripe => {
      this.stripe = stripe;
      const elements = stripe?.elements();
      if (elements) {
        this.cardNumber = elements.create('cardNumber');
        this.cardNumber.mount(this.cardNumberElement?.nativeElement);
        this.cardNumber.on('change', event => {
          this.cardNumberComplete = event.complete;
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })

        this.cardExpiry = elements.create('cardExpiry');
        this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);
        this.cardExpiry.on('change', event => {
          this.cardExpiryComplete = event.complete;
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })

        this.cardCvc = elements.create('cardCvc');
        this.cardCvc.mount(this.cardCvcElement?.nativeElement);
        this.cardCvc.on('change', event => {
          this.cardCvcComplete = event.complete;
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })
      }
    })
  }

  get paymentFormComplete() {
    return this.checkoutForm?.get('paymentForm')?.valid 
    && this.cardNumberComplete 
    && this.cardExpiryComplete 
    && this.cardCvcComplete
  }

  submitOrder() {
    console.log('submitOrder() function called');
    if (this.loading) return;  // Prevent multiple submissions

    this.loading = true;
    const porudzbinaID = Number(localStorage.getItem('porudzbinaID'));

    this.cartService.getPorudzbinaByID(porudzbinaID).subscribe(res => {
      console.log('Response from server:', res);
      if (res.clientSecret !== null) {
        this.secret = res.clientSecret;
      }

      this.stripe?.confirmCardPayment(this.secret, {
        payment_method: {
          card: this.cardNumber!,
          billing_details: {
            name: this.checkoutForm?.get('paymentForm')?.get('nameOnCard')?.value
          }
        }
      }).then(result => {
        this.loading = false;  // Reset loading state
        if (result.error) {
          this.cardErrors = result.error.message;
        } else if (result.paymentIntent) {
          const id = localStorage.getItem('porudzbinaID');
          // Remove order ID from local storage
          //localStorage.removeItem('porudzbinaID');

          // Clear the cart
          this.cartService.ukloniSveIzKorpe();

          // Navigate to success page
          this.router.navigate(['/checkout/success']);
        }
      }).catch(error => {
        this.loading = false;  // Reset loading state
        this.cardErrors = error.message;
      });
    }, error => {
      this.loading = false;  // Reset loading state
      this.cardErrors = 'Failed to retrieve order details.';
    });
  }

}
