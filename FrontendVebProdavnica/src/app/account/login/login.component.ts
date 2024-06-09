import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm = new FormGroup({
    korisnickoIme: new FormControl('', Validators.required),
    lozinka: new FormControl('', [Validators.required, Validators.minLength(6)])
  })

  constructor(private accountService: AccountService, private router: Router){}

  onSubmit() {
    if (this.loginForm.valid) {
      console.log('Submitting form with values:', this.loginForm.value);
      this.accountService.login(this.loginForm.value).subscribe({
        next: () => this.router.navigateByUrl('/shop')
      });
    } else {
      console.error('Form is invalid');
    }
  }
  
}
