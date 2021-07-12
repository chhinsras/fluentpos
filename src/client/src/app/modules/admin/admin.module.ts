import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MaterialModule } from 'src/app/core/material/material.module';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { CatalogModule } from './catalog/catalog.module';
import { DeleteDialogComponent } from './shared/components/delete-dialog/delete-dialog.component';
import { LogoutDialogComponent } from './shared/components/logout-dialog/logout-dialog.component';


@NgModule({
  declarations: [
    DashboardComponent,
    DeleteDialogComponent,
    LogoutDialogComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    MaterialModule,
    SharedModule,
    CatalogModule
  ]
})
export class AdminModule { }
