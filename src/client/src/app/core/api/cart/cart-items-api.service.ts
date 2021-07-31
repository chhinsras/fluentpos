import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CartItemApiModel } from '../../models/cart/cart-item';

@Injectable({
  providedIn: 'root'
})
export class CartItemsApiService {
  baseUrl = environment.apiUrl + 'people/cartitems/';
  constructor(private http: HttpClient) {
  }
  create(cartItem: CartItemApiModel) {
    return this.http.post(this.baseUrl, cartItem);
  }

  update(cartItem: CartItemApiModel) {
    return this.http.put(this.baseUrl, cartItem);
  }
}
