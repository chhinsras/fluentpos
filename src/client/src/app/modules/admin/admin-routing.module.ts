import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CatalogComponent } from './catalog/catalog.component';
import { BrandComponent } from './catalog/components/brand/brand.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CategoryComponent } from "./catalog/components/category/category.component";
import { SettingsComponent } from './settings/settings.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent
  },
  {
    path: 'dashboard',
    component: DashboardComponent
  },
  {
    path: 'settings',
    component: SettingsComponent
  },
  {
    path: 'catalog/brands',
    component: BrandComponent
  },
  {
    path: 'catalog/categories',
    component: CategoryComponent
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
