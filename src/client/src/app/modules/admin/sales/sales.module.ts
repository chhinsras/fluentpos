import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SalesComponent } from './sales.component';
import { SalesRoutingModule } from './sales-routing.module';
import { MaterialModule } from 'src/app/core/material/material.module';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { OrderComponent } from './components/order/order.component';

@NgModule({
  declarations: [
    SalesComponent,
    OrderComponent
  ],
  imports: [
    CommonModule,
    SalesRoutingModule,
    MaterialModule,
    SharedModule
  ]
})
export class SalesModule { }
