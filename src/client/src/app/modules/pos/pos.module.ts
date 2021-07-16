import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PosRoutingModule } from './pos-routing.module';
import { MaterialModule } from 'src/app/core/material/material.module';
import { PosComponent } from './pos.component';
import { CustomerComponent } from './customer/customer.component';
import { CatalogComponent } from './catalog/catalog.component';


@NgModule({
  declarations: [
    PosComponent,
    CustomerComponent,
    CatalogComponent
  ],
  imports: [
    CommonModule,
    PosRoutingModule,
    MaterialModule
  ]
})
export class PosModule { }
