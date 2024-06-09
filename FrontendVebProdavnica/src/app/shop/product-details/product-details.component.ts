import { Component, OnInit } from '@angular/core';
import { Proizvod, ProizvodUpdate } from 'src/app/shared/models/proizvod';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { CartService } from 'src/app/cart/cart.service';
import { MatDialog } from '@angular/material/dialog';
import { ProductUpdateFormComponent } from 'src/app/product-update-form/product-update-form.component';
import { MatSnackBar } from '@angular/material/snack-bar';

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
              private dialog: MatDialog,
              private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.loadProizvod();
  }

  /*openEditDialog(): void {
    const dialogRef = this.dialog.open(ProductUpdateFormComponent, {
      width: '400px',
      data: { ...this.proizvod } as ProizvodUpdate
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed', result);
      if (result) {
        this.updateProizvod(result);
      }
    });
  }*/

  /*updateProizvod(proizvod: ProizvodUpdate): void {
    this.shopService.updateProizvod(proizvod).subscribe(
      updatedProizvod => {
        this.proizvod = updatedProizvod;
        this.snackBar.open('Proizvod uspješno ažuriran', 'Zatvori', {
          duration: 2000,
        });
      },
      error => {
        console.error('Greška prilikom ažuriranja proizvoda:', error);
        this.snackBar.open('Došlo je do greške prilikom ažuriranja proizvoda', 'Zatvori', {
          duration: 2000,
        });
      }
    );
  }*/

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
}
