import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Proizvod, ProizvodCreation, ProizvodUpdate } from '../shared/models/proizvod';
import { Pagination } from '../shared/models/pagination';
import { Kategorija } from '../shared/models/kategorija';
import { ShopParams } from '../shared/models/shopParams';
import { Observable } from 'rxjs';
import { AdministratorLozinka } from '../shared/models/administrator';

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
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.put<ProizvodUpdate>(this.baseUrl + 'proizvod', proizvod, {headers});
  }

  createProizvod(proizvod: ProizvodCreation): Observable<ProizvodCreation> {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.post<ProizvodCreation>(this.baseUrl + 'proizvod', proizvod, {headers});
  }

  deleteProizvod(id: number): Observable<any> {
    const token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.delete<any>(this.baseUrl + 'proizvod/' + id, {headers});
  }

  getAdministratorLozinkaById(administratorId: number): Observable<AdministratorLozinka> { 
    return this.http.get<AdministratorLozinka>(`${this.baseUrl}administrator/lozinka/${administratorId}`);
  }
  
}
