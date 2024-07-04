import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AdminRoutingModule } from './admin-routing.module';
import { UsersComponent } from './users/users.component';
import { UsersOrdersComponent } from './users-orders/users-orders.component';
import { ProductDialogComponent } from './product-dialogs/product-dialog/product-dialog.component';
import { FormsModule } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';


@NgModule({
  declarations: [
    UsersComponent,
    UsersOrdersComponent,
    ProductDialogComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    ModalModule.forRoot()
  ]
})
export class AdminModule { }
