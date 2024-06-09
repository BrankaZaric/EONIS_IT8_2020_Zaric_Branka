import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Proizvod } from '../shared/models/proizvod';
import { ShopService } from './shop.service';
import { Kategorija } from '../shared/models/kategorija';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm?: ElementRef;
  proizvodi: Proizvod[] = [];
  kategorije: Kategorija[] = [];
  shopParams = new ShopParams();
  sortOptions = [
    {name: 'Po nazivu', value:'name'},
    {name: 'Cena: najjeftinije do najskuplje', value:'cenaAsc'},
    {name: 'Cena: najskuplje do najjeftinije', value:'cenaDesc'},
  ];
  totalCount = 0;

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProizvodi();
    this.getKategorije();
  }

  getProizvodi(){
    this.shopService.getProizvodi(this.shopParams).subscribe({
      next: response => {
        this.proizvodi = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
        //console.log(response.data);
      },
      error: error => console.log(error)
    })
  }

  getKategorije(){
    this.shopService.getKategorije().subscribe({
      next: response => this.kategorije = [{kategorijaID: 0, nazivKategorije:'Sve'}, ...response],
      error: error => console.log(error)
    })
  }

  onKategorijaSelected(kategorijaId: number){
    this.shopParams.kategorijaId = kategorijaId;
    this.shopParams.pageNumber = 1;
    this.getProizvodi();
    //console.log('KategorijaId:' + kategorijaId);
  }

  onSortSelected(event: any){
    this.shopParams.sort = event.target.value;
    this.getProizvodi();
  }

  onPageChanged(event: any){
    if(this.shopParams.pageNumber !== event){
      this.shopParams.pageNumber = event;
      this.getProizvodi();
    }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm?.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProizvodi();
  }

  onReset() {
    if(this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProizvodi();
  }
}
