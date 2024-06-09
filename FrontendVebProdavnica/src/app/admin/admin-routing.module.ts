import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from './users/users.component';
import { UsersOrdersComponent } from './users-orders/users-orders.component';

const routes: Routes = [
  {path: 'users', component: UsersComponent},
  {path: 'users-orders', component: UsersOrdersComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
