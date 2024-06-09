import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Proizvod, ProizvodUpdate } from '../shared/models/proizvod';
import { Pagination } from '../shared/models/pagination';
import { Kategorija } from '../shared/models/kategorija';
import { ShopParams } from '../shared/models/shopParams';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7295/api/'

  constructor(private http: HttpClient) { }

  getProizvodi(shopParams: ShopParams){
    let params = new HttpParams();

    if (shopParams.kategorijaId > 0) params = params.append('kategorijaId', shopParams.kategorijaId);
    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber);
    params = params.append('pageSize', shopParams.pageSize);
    if(shopParams.search) params = params.append('search', shopParams.search);

    return this.http.get<Pagination<Proizvod[]>>(this.baseUrl + 'proizvod', {params});
  }

  getKategorije(){
    return this.http.get<Kategorija[]>(this.baseUrl + 'proizvod/kategorija');
  }

  getProizvod(id: number){
    return this.http.get<Proizvod>(this.baseUrl + 'proizvod/' + id);
  }

  updateProizvod(proizvod: ProizvodUpdate): Observable<ProizvodUpdate> {
    return this.http.put<ProizvodUpdate>(this.baseUrl + 'proizvod', proizvod);
  }
}
