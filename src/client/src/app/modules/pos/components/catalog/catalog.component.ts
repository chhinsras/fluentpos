import { CategoryParams } from './../../models/categoryParams';
import { Category } from './../../models/category';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { BusyService } from 'src/app/core/services/busy.service';
import { Brand } from '../../models/brand';
import { BrandParams } from '../../models/brandParams';
import { Product } from '../../models/product';
import { ProductParams } from '../../models/productParams';
import { CartService } from '../../services/cart.service';
import { PosService } from '../../services/pos.service';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.scss']
})
export class CatalogComponent implements OnInit {
  brands: PaginatedResult<Brand>;
  brandParams = new BrandParams();
  categories: PaginatedResult<Category>;
  categoryParams = new CategoryParams();
  products: PaginatedResult<Product>;
  productParams = new ProductParams();
  searchString: string;
  brandAutoComplete = new FormControl();
  categoryAutoComplete = new FormControl();
  showImage: boolean = false;
  hasProductsLoaded: boolean = false;
  invalidCart: boolean = true;
  constructor(private posService: PosService, private toastr: ToastrService, public cartService: CartService, public busyService: BusyService) { }

  ngOnInit(): void {
    this.productParams.brandIds = [];
    this.productParams.categoryIds = [];
    this.productParams.pageSize = 16;
    this.brandParams.pageSize = 5;
    this.getProducts();
    this.getBrands();
    this.getCategories();
    this.brandAutoComplete.valueChanges.subscribe((value) => this._filterBrand(value));
    this.categoryAutoComplete.valueChanges.subscribe((value) => this._filterCategory(value));
  }

  getProducts() {
    this.hasProductsLoaded = false;
    this.posService.getProducts(this.productParams).subscribe((res) => { this.products = res, this.hasProductsLoaded = true });
  }

  getBrands() {
    this.posService.getBrands(this.brandParams).subscribe((res) => { this.brands = res; });
  }

  getCategories() {
    this.posService.getCategories(this.categoryParams).subscribe((res) => { this.categories = res; });
  }

  isCheckedBrand(brand: Brand): boolean {
    if (this.productParams.brandIds.includes(brand.id)) return true;
    return false;
  }

  isCheckedCategory(category: Category): boolean {
    if (this.productParams.categoryIds.includes(category.id)) return true;
    return false;
  }

  toggleBrandSelection($event, brand: Brand) {
    if ($event.checked) {
      if (!this.productParams.brandIds.includes(brand.id)) this.productParams.brandIds.push(brand.id);
    } else {
      if (this.productParams.brandIds.includes(brand.id)) this.productParams.brandIds = this.productParams.brandIds.filter(item => item !== brand.id);
    }
    this.getProducts();
  }

  toggleCategorySelection($event, category: Category) {
    if ($event.checked) {
      if (!this.productParams.categoryIds.includes(category.id)) this.productParams.categoryIds.push(category.id);
    } else {
      if (this.productParams.categoryIds.includes(category.id)) this.productParams.categoryIds = this.productParams.categoryIds.filter(item => item !== category.id);
    }
    this.getProducts();
  }

  private _filterBrand(value: string) {
    const filterValue = value.toLowerCase();
    this.brandParams.searchString = filterValue;
    this.getBrands();
  }

  private _filterCategory(value: string) {
    const filterValue = value.toLowerCase();
    this.categoryParams.searchString = filterValue;
    this.getCategories();
  }

  public doFilter(): void {
    this.productParams.searchString = this.searchString.trim().toLocaleLowerCase();
    this.getProducts();
  }

  getBrandName(brandId: string) {
    if (this.brands && this.brands.data && this.brands.data.find(item => item.id === brandId)) {
      return this.brands.data.find(brand => brand.id === brandId).name;
    }
  }

  getCategoryName(categoryId: string) {
    if (this.categories && this.categories.data && this.categories.data.find(item => item.id === categoryId)) {
      return this.categories.data.find(category => category.id === categoryId).name;
    }
  }

  isCustomerSelected() {
    const currentCustomer = this.cartService.getCurrentCustomer();
    if (!currentCustomer) {
      this.toastr.error('Select a customer');
      return false;
    }
    return true;
  }
  addToCart(product: Product) {
    if (this.isCustomerSelected()) {
      this.cartService.add(product);
    }

  }
  toggleImage() {
    this.showImage = !this.showImage;
  }

}
