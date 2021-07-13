import {HttpClient, HttpParams} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {environment} from 'src/environments/environment';
import {Product} from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductApiService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getAlls(params: HttpParams) {
    return this.http.get(this.baseUrl + 'catalog/products', {params: params});
  }

  getById(id: string) {
    return this.http.get<Product>(this.baseUrl + `catalog/products/${id}`);
  }

  create(product: Product) {
    return this.http.post(this.baseUrl + 'catalog/products', product);
  }

  update(product: Product) {
    return this.http.put(this.baseUrl + 'catalog/products', product);
  }

  delete(id: string) {
    return this.http.delete(this.baseUrl + `catalog/products/${id}`);
  }
}
