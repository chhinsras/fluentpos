import { HttpClient } from '@angular/common/http';
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

  getAlls() {
    return this.http.get(this.baseUrl + 'catalog/brands');
  }

  getById(id: string) {
    return this.http.get<Brand>(this.baseUrl + `catalog/brands/${id}`);
  }
}
