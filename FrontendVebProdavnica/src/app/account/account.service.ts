import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, throwError, catchError } from 'rxjs';
import { KorisniBezLozinke, KorisnikCreation, KorisnikDTO, KorisnikUpdate, User } from '../shared/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Administrator } from '../shared/models/administrator';
import { PorudzbinaDTO } from '../shared/models/porudzbina';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:7295/api'

  private currentUserSource = new BehaviorSubject<User | Administrator | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  loadCurrentUser(token: string): Observable<User | Administrator> {
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
  
    return this.http.get<User | Administrator>(`${this.baseUrl}/autentifikacija/current`, { headers }).pipe(
      map((response: any) => {
        console.log('Response from /autentifikacija/current:', response); // Ispis odgovora
        if (response) {
          // Prvo ukloni postojeće vrednosti iz local storage
          localStorage.removeItem('userId');
          localStorage.removeItem('adminId');
  
          if (response.korisnikID) {
            console.log('Loaded user:', response);
            localStorage.setItem('userId', response.korisnikID.toString()); 
            localStorage.setItem('isAdmin', "false");
          } else if (response.adminID) {
            console.log('Loaded administrator:', response);
            localStorage.setItem('adminId', response.adminID.toString()); 
            localStorage.setItem('isAdmin', "true");
          } else {
            console.log('No korisnikID or adminID found in response');
          }
        }
        return response;
      }),
      catchError(error => {
        console.error('Error loading current user:', error);
        return throwError(error);
      })
    );
  }

  login(values: any): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.baseUrl}/autentifikacija`, values).pipe(
      map(response => {
        console.log('Received response from backend:', response); // Dodato za proveru odgovora
        if (response && response.token) {
          console.log('Received token:', response.token);
          const user: Partial<User | Administrator> = {
            korisnickoIme: values.korisnickoIme,
            token: response.token
          };
          localStorage.setItem('token', response.token);
          this.currentUserSource.next(user as User | Administrator);

          // Load current user after login
          this.loadCurrentUser(response.token).subscribe();

          return response; 
        } else {
          console.log('No token received');
          throw new Error('No token received'); 
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userId'); 
    localStorage.removeItem('adminId'); 
    localStorage.removeItem('isAdmin');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  getCurrentUser() {
    return this.currentUserSource.value;
  }

  registerKorisnik(korisnik: KorisnikCreation) {
    return this.http.post(`${this.baseUrl}/korisnik`, korisnik).pipe(
      catchError((error: any) => {
        let errorMessage = 'Došlo je do greške tokom registracije';
        if (error.status === 400 && error.error) {
          errorMessage = error.error; // Backend vraća korisnički definisanu grešku kao string
        }
        return throwError(() => new Error(errorMessage));
      })
    );
  }
  

  updateKorisnik(korisnik: KorisnikUpdate){
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.put(`${this.baseUrl}/korisnik`, korisnik);
  }

    getKorisnikById(): Observable<KorisnikDTO> {
      const korisnikId = Number(localStorage.getItem('userId'));
      console.log('Fetching korisnik with ID:', korisnikId);  
      return this.http.get<KorisnikDTO>(`${this.baseUrl}/korisnik/${korisnikId}`);
    }
    
  getKorisnici(){
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get<KorisniBezLozinke[]>(`${this.baseUrl}/korisnik`,{headers} );
  }

  deleteKorisnik(korisnikID: number): Observable<void>{
    return this.http.delete<void>(`${this.baseUrl}/korisnik/${korisnikID}`);
  }

  getPorudzbineZaKorisnika(korisnikId: number) {
    const token = localStorage.getItem('token');
    let headers= new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get<PorudzbinaDTO[]>(`${this.baseUrl}/porudzbina/korisnik/${korisnikId}`, { headers });
  }
  
  getPlacenePorudzbine(): Observable<PorudzbinaDTO[]> {
    return this.http.get<PorudzbinaDTO[]>(`${this.baseUrl}/porudzbina/placene`).pipe(
      catchError(error => {
        console.error('Error fetching paid orders:', error);
        return throwError(error);
      })
    );
  }
}
