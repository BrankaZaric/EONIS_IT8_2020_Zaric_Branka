import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { StepperComponent } from './components/stepper/stepper.component';
import {CdkStepperModule} from '@angular/cdk/stepper';

@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    StepperComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    CdkStepperModule
  ],
  exports:[
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    ReactiveFormsModule,
    BsDropdownModule,
    StepperComponent,
    CdkStepperModule
  ]
})
export class SharedModule { }
