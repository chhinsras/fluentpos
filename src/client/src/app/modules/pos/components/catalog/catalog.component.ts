import { Component, OnInit } from '@angular/core';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { Brand } from '../../models/brand';
import { BrandParams } from '../../models/brandParams';
import { Product } from '../../models/product';
import { ProductParams } from '../../models/productParams';
import { PosService } from '../../services/pos.service';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.scss']
})
export class CatalogComponent implements OnInit {
  brands: PaginatedResult<Brand>;
  brandParams = new BrandParams();
  products: PaginatedResult<Product>;
  productParams = new ProductParams();
  searchString: string;
  constructor(private posService: PosService) { }

  ngOnInit(): void {
    this.productParams.pageSize = 5;
    this.brandParams.pageSize = 5;
    this.getProducts();
    this.getBrands();
  }
  getProducts() {
    this.posService.getProducts(this.productParams).subscribe((res) => { this.products = res });
  }
  getBrands() {
    this.posService.getBrands(this.brandParams).subscribe((res) => { this.brands = res; console.log(res); });
  }
  public doFilter(): void {
    this.productParams.searchString = this.searchString.trim().toLocaleLowerCase();
    this.getProducts();
  }
}
