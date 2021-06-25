import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ToolbarComponent } from 'src/app/layouts/admin-layout/toolbar/toolbar.component';
import { SideNavigationComponent } from 'src/app/layouts/admin-layout/side-navigation/side-navigation.component';
import { AdminLayoutComponent } from 'src/app/layouts/admin-layout/admin-layout.component';
import { MaterialModule } from 'src/app/core/material/material.module';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { CatalogModule } from './catalog/catalog.module';


@NgModule({
  declarations: [
    DashboardComponent
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
