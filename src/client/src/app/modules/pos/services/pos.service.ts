import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { BrandApiService } from 'src/app/core/api/catalog/brand-api.service';
import { ProductApiService } from 'src/app/core/api/catalog/product-api.service';
import { CustomerApiService } from 'src/app/core/api/people/customer-api.service';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { Result } from 'src/app/core/models/wrappers/Result';
import { Brand } from '../models/brand';
import { BrandParams } from '../models/brandParams';
import { Customer } from '../models/customer';
import { CustomerParams } from '../models/customerParams';
import { Product } from '../models/product';
import { ProductParams } from '../models/productParams';

@Injectable({
  providedIn: 'root'
})
export class PosService {

  public isCustomerLoading: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  constructor(private customerApi: CustomerApiService,private productApi :ProductApiService, private brandService:BrandApiService ) { }

  getCustomers(CustomerParams: CustomerParams): Observable<PaginatedResult<Customer>> {
    let params = new HttpParams();
    if (CustomerParams.searchString) params = params.append('searchString', CustomerParams.searchString);
    if (CustomerParams.pageSize) params = params.append('pageSize', CustomerParams.pageSize);
    this.isCustomerLoading.next(true);
    return this.customerApi.getAlls(params)
      .pipe(map((response: PaginatedResult<Customer>) => {
        this.isCustomerLoading.next(false);
        return response;
      }));
  }
  getCustomerById(id: string): Observable<Result<Customer>> {
    return this.customerApi.getById(id).pipe(
      map((response: Result<Customer>) => response)
    );
  }
  getProducts(
    productParams: ProductParams
  ): Observable<PaginatedResult<Product>> {
    let params = new HttpParams();
    if (productParams.searchString) {
      params = params.append('searchString', productParams.searchString);
    }
    if (productParams.pageSize) {
      params = params.append('pageSize', productParams.pageSize.toString());
    }
    return this.productApi
      .getAlls(params)
      .pipe(map((response: PaginatedResult<Product>) => response));
  }
  getProductById(id: string): Observable<Result<Product>> {
    return this.productApi.getById(id).pipe(
      map((response: Result<Product>) => response)
    );
  }
  getBrands(brandParams: BrandParams): Observable<PaginatedResult<Brand>> {
    let params = new HttpParams();
    if (brandParams.searchString)
      params = params.append('searchString', brandParams.searchString);
    if (brandParams.pageNumber)
      params = params.append('pageNumber', brandParams.pageNumber.toString());
    if (brandParams.pageSize)
      params = params.append('pageSize', brandParams.pageSize.toString());
    if (brandParams.orderBy)
      params = params.append('orderBy', brandParams.orderBy.toString());
    return this.brandService
      .getAlls(params)
      .pipe(map((response: PaginatedResult<Brand>) => response));
  }

}
