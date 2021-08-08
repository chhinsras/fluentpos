import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ProductApiService } from 'src/app/core/api/catalog/product-api.service';
import { IResult } from 'src/app/core/models/wrappers/IResult';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { Result } from 'src/app/core/models/wrappers/Result';
import { Product } from '../models/product';
import { ProductParams } from '../models/productParams';

@Injectable()
export class ProductService {
  constructor(private api: ProductApiService) {}

  getProducts(
    productParams: ProductParams
  ): Observable<PaginatedResult<Product>> {
    let params = new HttpParams();
    if (productParams.searchString) {
      params = params.append('searchString', productParams.searchString);
    }
    if (productParams.pageNumber) {
      params = params.append('pageNumber', productParams.pageNumber.toString());
    }
    if (productParams.pageSize) {
      params = params.append('pageSize', productParams.pageSize.toString());
    }
    if (productParams.orderBy) {
      params = params.append('orderBy', productParams.orderBy.toString());
    }
    return this.api
      .getAlls(params)
      .pipe(map((response: PaginatedResult<Product>) => response));
  }

  getProductById(id: string): Observable<Result<Product>> {
    return this.api.getById(id).pipe(map((response: Result<Product>) => response));
  }

  createProduct(product: Product): Observable<IResult<Product>> {
    return this.api
      .create(product)
      .pipe(map((response: IResult<Product>) => response));
  }

  updateProduct(product: Product): Observable<IResult<Product>> {
    return this.api
      .update(product)
      .pipe(map((response: IResult<Product>) => response));
  }

  deleteProduct(id: string): Observable<IResult<string>> {
    return this.api
      .delete(id)
      .pipe(map((response: IResult<string>) => response));
  }
}
