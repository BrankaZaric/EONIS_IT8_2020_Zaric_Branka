import { Component, OnInit } from '@angular/core';
import { Proizvod } from 'src/app/shared/models/proizvod';
import { ShopService } from '../shop.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CartService } from 'src/app/cart/cart.service';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProductDialogComponent } from 'src/app/admin/product-dialogs/product-dialog/product-dialog.component';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  proizvod?: Proizvod;
  kolicina: number = 1;
  dozvoljenaKolicina: boolean = true;

  constructor(private shopService: ShopService, 
              private activatedRoute: ActivatedRoute, 
              private cartService: CartService,
              private snackBar: MatSnackBar,
              private router: Router,
              private dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadProizvod();
  }

  isAdmin(): boolean {
    const isAdmin = localStorage.getItem('isAdmin');
    return isAdmin === "true";
  } 

  loadProizvod() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) this.shopService.getProizvod(+id).subscribe({
      next: proizvod => this.proizvod = proizvod,
      error: error => console.log(error)
    });
  }

  incrementKolicina() {
    this.kolicina++;
  }

  decrementKolicina() {
    if (this.kolicina > 1) {
      this.kolicina--;
    }
  }

  addToCart() {
    if (this.proizvod && this.kolicina <= this.proizvod.kolicina) {
      this.cartService.dodajUKorpu(this.proizvod, this.kolicina);
      this.dozvoljenaKolicina = true;
    } else {
      this.dozvoljenaKolicina = false;
    }
  }

  obrisiProizvod() {
    if (this.proizvod?.proizvodID) {
      this.shopService.deleteProizvod(this.proizvod.proizvodID).subscribe({
        next: () => {
          this.snackBar.open('Proizvod je uspešno obrisan!', 'Zatvori', {
            duration: 3000,
          });
          this.router.navigate(['/shop']);  // Navigirajte korisnika nazad na listu proizvoda
        },
        error: error => {
          this.snackBar.open('Došlo je do greške prilikom brisanja proizvoda.', 'Zatvori', {
            duration: 3000,
          });
          console.log(error);
        }
      });
    }
  }

  izmeniProizvod() {
    if (this.proizvod) {
      const dialogRef = this.dialog.open(ProductDialogComponent, {
        width: '600px',
        data: { ...this.proizvod, flag: 2 }
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.loadProizvod();
        }
      });
    }
  }
  
}
