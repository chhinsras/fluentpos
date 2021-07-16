import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PeopleRoutingModule } from './people-routing.module';
import { CustomerComponent } from './components/customer/customer.component';
import { SupplierComponent } from './components/supplier/supplier.component';


@NgModule({
  declarations: [
    CustomerComponent,
    SupplierComponent
  ],
  imports: [
    CommonModule,
    PeopleRoutingModule
  ]
})
export class PeopleModule { }
