import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {BrandComponent} from './components/brand/brand.component';
import {CategoryComponent} from './components/category/category.component';
import {ProductComponent} from './components/product/product.component';

const routes: Routes = [
  {
    path: 'brands',
    component: BrandComponent
  },
  {
    path: 'categories',
    component: CategoryComponent
  },
  {
    path: 'products',
    component: ProductComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CatalogRoutingModule {
}
