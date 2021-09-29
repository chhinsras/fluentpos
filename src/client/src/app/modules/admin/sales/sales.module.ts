import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SalesComponent } from './sales.component';
import { SalesListComponent } from './components/sales-list/sales-list.component';
import { SalesRoutingModule } from './sales-routing.module';
import { MaterialModule } from 'src/app/core/material/material.module';
import { SharedModule } from 'src/app/core/shared/shared.module';

@NgModule({
  declarations: [
    SalesComponent,
    SalesListComponent
  ],
  imports: [
    CommonModule,
    SalesRoutingModule,
    MaterialModule,
    SharedModule
  ]
})
export class SalesModule { }
