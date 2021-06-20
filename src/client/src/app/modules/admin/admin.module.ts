import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ToolbarComponent } from 'src/app/layouts/admin-layout/toolbar/toolbar.component';
import { SideNavigationComponent } from 'src/app/layouts/admin-layout/side-navigation/side-navigation.component';
import { AdminLayoutComponent } from 'src/app/layouts/admin-layout/admin-layout.component';


@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
