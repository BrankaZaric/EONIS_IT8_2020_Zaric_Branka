import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { PorudzbinaDTO } from 'src/app/shared/models/porudzbina';

@Component({
  selector: 'app-users-orders',
  templateUrl: './users-orders.component.html',
  styleUrls: ['./users-orders.component.scss']
})
export class UsersOrdersComponent implements OnInit{
  placenePorudzbine: PorudzbinaDTO[] = [];

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.accountService.getPlacenePorudzbine().subscribe(res => {
      this.placenePorudzbine = res;
      console.log('Plaćene porudžbine:', res);
    }, error => {
      console.error('Greška prilikom dohvaćanja plaćenih porudžbina:', error);
    });
  }

}
