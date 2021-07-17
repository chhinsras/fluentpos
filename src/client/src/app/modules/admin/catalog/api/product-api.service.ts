import {HttpClient, HttpParams} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {environment} from 'src/environments/environment';
import {Product} from '../models/product';

@Injectable()
export class ProductApiService {

  baseUrl = environment.apiUrl + 'catalog/products/';

  constructor(private http: HttpClient) {
  }

  getAlls(params: HttpParams) {
    return this.http.get(this.baseUrl, {params: params});
  }

  getById(id: string) {
    return this.http.get<Product>(this.baseUrl + id);
  }

  create(product: Product) {
    return this.http.post(this.baseUrl, product);
  }

  update(product: Product) {
    return this.http.put(this.baseUrl, product);
  }

  delete(id: string) {
    return this.http.delete(this.baseUrl + id);
  }
}
