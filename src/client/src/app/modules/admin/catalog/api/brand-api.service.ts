import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { environment } from 'src/environments/environment';
import { Brand } from '../models/brand';

@Injectable({
  providedIn: 'root'
})
export class BrandApiService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAlls(params: HttpParams) {
    return this.http.get(this.baseUrl + 'catalog/brands', { params: params });
  }

  getById(id: string) {
    return this.http.get<Brand>(this.baseUrl + `catalog/brands/${id}`);
  }

  update(brand: Brand) {
    return this.http.put(this.baseUrl + 'catalog/brands', brand);
  }

  delete(id: string) {
    return this.http.delete(this.baseUrl + `catalog/brands/${id}`).subscribe(() => console.log('Delete successful'));
  }
}
