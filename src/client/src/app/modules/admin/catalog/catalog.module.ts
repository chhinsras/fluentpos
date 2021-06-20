import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CatalogRoutingModule } from './catalog-routing.module';
import { CatalogComponent } from './catalog.component';
import { BrandComponent } from './components/brand/brand.component';
import { ProductComponent } from './components/product/product.component';
import { CategoryComponent } from './components/category/category.component';


@NgModule({
  declarations: [
    CatalogComponent,
    BrandComponent,
    ProductComponent,
    CategoryComponent
  ],
  imports: [
    CommonModule,
    CatalogRoutingModule
  ]
})
export class CatalogModule { }
