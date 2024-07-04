import { Component, Inject, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Proizvod, ProizvodCreation, ProizvodUpdate } from 'src/app/shared/models/proizvod';
import { Kategorija } from 'src/app/shared/models/kategorija';
import { Administrator, AdministratorLozinka } from 'src/app/shared/models/administrator'; 
import { ShopService } from 'src/app/shop/shop.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-product-dialog',
  templateUrl: './product-dialog.component.html',
  styleUrls: ['./product-dialog.component.scss']
})
export class ProductDialogComponent implements OnInit {

  proizvodID: number;
  nazivProizvoda: string | undefined;
  opis: string | undefined;
  cena: number | undefined;
  kolicina: number | undefined;
  slikaURL: string | undefined;
  kategorijaID: number | undefined;
  adminID: number | undefined;
  flag: number | undefined;

  kategorije: Kategorija[] = [];
  administrator: AdministratorLozinka | undefined;

    constructor(
      public dialogRef: MatDialogRef<ProductDialogComponent>,
      private shopService: ShopService,
      @Inject(MAT_DIALOG_DATA) public data: any
    ) {
      this.proizvodID = data.proizvodID;
      this.nazivProizvoda = data.nazivProizvoda;
      this.opis = data.opis;
      this.cena = data.cena;
      this.kolicina = data.kolicina;
      this.slikaURL = data.slikaURL;
      this.kategorijaID = data.kategorijaID;
      this.adminID = data.adminID;
      this.flag = data.flag;
    }

  ngOnInit(): void {
    this.shopService.getKategorije().subscribe(kategorije => {
      this.kategorije = kategorije;
    });
  }

  closeModal() {
    this.dialogRef.close();
  }

  saveModal() {
    if (this.flag === 1) { // Add new product
      if (this.nazivProizvoda && this.opis && this.cena !== undefined && this.kolicina !== undefined && this.slikaURL && this.kategorijaID !== undefined && this.adminID !== undefined) {
        if (this.cena < 0 || this.kolicina < 0) {
          alert("Cena i količina moraju biti veće od 0");
        } else {
          const newProizvod = new ProizvodCreation(this.nazivProizvoda, this.opis, this.cena, this.kolicina, this.slikaURL, this.kategorijaID, Number(localStorage.getItem('adminId')));
          console.log("Podaci koji se šalju na backend:", newProizvod); 
          this.shopService.createProizvod(newProizvod).subscribe(res => {
            alert("Proizvod uspešno dodat!");
            this.dialogRef.close();
          });
        }
      } else {
        alert('Neki od obaveznih podataka nisu uneti.');
      }
    } else if (this.flag === 2 && this.proizvodID !== undefined) { // Ažuriranje postojećeg proizvoda
      const adminID = Number(localStorage.getItem('adminId'));
      if (!adminID) {
        alert('Administrator ID nije pronađen u localStorage.');
        return;
      }
      this.shopService.getAdministratorLozinkaById(adminID).subscribe(
        admin => {
          this.administrator = admin;
          if (this.nazivProizvoda && this.opis && this.cena !== undefined && this.kolicina !== undefined && this.slikaURL && this.kategorijaID !== undefined && this.administrator) {
            if (this.cena < 0 || this.kolicina < 0) {
              alert("Cena i količina moraju biti veće od 0");
            } else {
              const updateProizvod = new ProizvodUpdate(
                this.proizvodID,
                this.nazivProizvoda,
                this.opis,
                this.cena,
                this.kolicina,
                this.slikaURL,
                this.kategorijaID,
                this.kategorije.find(k => k.kategorijaID === this.kategorijaID)!,
                adminID,
                this.administrator
              );
              console.log("Podaci koji se šalju na backend za ažuriranje:", updateProizvod);
              this.shopService.updateProizvod(updateProizvod).subscribe(res => {
                alert("Proizvod uspešno ažuriran!");
                this.dialogRef.close(true);
              });
            }
          } else {
            alert('Neki od obaveznih podataka nisu uneti.');
          }
        },
        error => {
          alert('Greška prilikom preuzimanja podataka administratora.');
        }
      );
    }
  }

  deleteModal() {
    if (this.proizvodID !== undefined) {
      this.shopService.deleteProizvod(this.proizvodID).subscribe(res => {
        alert("Proizvod uspešno obrisan!");
        this.dialogRef.close();
      });
    }
  }
}
