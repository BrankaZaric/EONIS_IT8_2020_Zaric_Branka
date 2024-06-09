import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AdminRoutingModule } from './admin-routing.module';
import { UsersComponent } from './users/users.component';
import { UsersOrdersComponent } from './users-orders/users-orders.component';


@NgModule({
  declarations: [
    UsersComponent,
    UsersOrdersComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    ModalModule.forRoot()
  ]
})
export class AdminModule { }
