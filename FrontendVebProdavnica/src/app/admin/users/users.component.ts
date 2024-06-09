import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AccountService } from 'src/app/account/account.service';
import { PorudzbinaDTO } from 'src/app/shared/models/porudzbina';
import { KorisniBezLozinke, KorisnikDTO } from 'src/app/shared/models/user';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit{
  korisnici: KorisniBezLozinke[] = [];
  porudzbine: PorudzbinaDTO[] = [];
  selectedKorisnikId: number | null = null;

  constructor(private accountService: AccountService){}

  ngOnInit(): void {
    this.accountService.getKorisnici().subscribe(res => {
      this.korisnici = res;
      console.log(res);
    })
  }

  onKorisnikClick(korisnikId: number): void {
    this.selectedKorisnikId = korisnikId;
    this.porudzbine = []; // Resetovanje porudzbina pre nove pretrage

    this.accountService.getPorudzbineZaKorisnika(korisnikId).subscribe(res => {
      this.porudzbine = res;
      console.log(`Porudžbine za korisnika ${korisnikId}:`, res);
    }, error => {
      console.error(`Greška prilikom dobijanja porudžbina za korisnika ${korisnikId}:`, error);
    });
  }

}
