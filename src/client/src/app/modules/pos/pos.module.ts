import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PosRoutingModule } from './pos-routing.module';
import { MaterialModule } from 'src/app/core/material/material.module';
import { PosComponent } from './pos.component';
import { CustomerComponent } from './customer/customer.component';
import { CatalogComponent } from './catalog/catalog.component';
import { CustomerSelectionComponent } from './customer-selection/customer-selection.component';
import { CustomerInfoComponent } from './customer-info/customer-info.component';
import { ProductInfoComponent } from './product-info/product-info.component';


@NgModule({
  declarations: [
    PosComponent,
    CustomerComponent,
    CatalogComponent,
    CustomerSelectionComponent,
    CustomerInfoComponent,
    ProductInfoComponent
  ],
  imports: [
    CommonModule,
    PosRoutingModule,
    MaterialModule
  ]
})
export class PosModule { }
