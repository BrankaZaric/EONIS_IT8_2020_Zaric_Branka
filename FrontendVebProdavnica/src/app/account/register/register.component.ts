import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, ValidationErrors, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { KorisnikCreation } from 'src/app/shared/models/user';
import { MatSnackBar } from '@angular/material/snack-bar';

// Funkcija koja proverava ispravnost formata email adrese
function emailValidator(control: AbstractControl): ValidationErrors | null {
  const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
  const valid = emailRegex.test(control.value);
  return valid ? null : { invalidEmail: true };
}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router, private snackBar: MatSnackBar){}

  complexPassword = "^(?=.*[0-9]+.*)(?=.*[a-zA-Z]+.*)[0-9a-zA-Z]{6,}$"

  registerForm = this.fb.group({
    ime: ['', [Validators.required]],
    prezime: ['', [Validators.required]],
    email: ['', [Validators.required, emailValidator]],
    korisnickoIme: ['', Validators.required],
    lozinka: ['', [Validators.required, Validators.pattern(this.complexPassword)]],
    adresa: [''], 
    telefon: ['']
  })

  onSubmit() {
    const korIme = this.registerForm.get('korisnickoIme')!.value as string;
    const lozinka = this.registerForm.get('lozinka')!.value as string;
    const email = this.registerForm.get('email')!.value as string;
    const ime = this.registerForm.get('ime')!.value as string;
    const prezime = this.registerForm.get('prezime')!.value as string;
    const adresa = this.registerForm.get('adresa')!.value as string;
    const telefon = this.registerForm.get('telefon')!.value as string;
  
    const korisnik = new KorisnikCreation(ime, prezime, email, korIme, lozinka, adresa, telefon);
  
    this.accountService.registerKorisnik(korisnik).subscribe({
      next: () => {
        this.snackBar.open('Registracija uspešno izvršena', 'Zatvori', {
          duration: 3000,
        });
        this.router.navigateByUrl('account/login');
      },
      error: (err) => {
        console.error('Registration error: ', err.message);
        this.snackBar.open(err.message, 'Zatvori', {
          duration: 3000,
        });
      }
    });
  }
  
}
