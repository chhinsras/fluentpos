import {Injectable} from '@angular/core';
import {environment} from '../../../../../environments/environment';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Category} from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryApiService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getAlls(params: HttpParams) {
    return this.http.get(this.baseUrl + 'catalog/categories', {params: params});
  }

  getById(id: string) {
    return this.http.get<Category>(this.baseUrl + `catalog/categories/${id}`);
  }

  create(category: Category) {
    return this.http.post(this.baseUrl + 'catalog/categories', category);
  }

  update(category: Category) {
    return this.http.put(this.baseUrl + 'catalog/categories', category);
  }

  delete(id: string) {
    return this.http.delete(this.baseUrl + `catalog/categories/${id}`);
  }
}
