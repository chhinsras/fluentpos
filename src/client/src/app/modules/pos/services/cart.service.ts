import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Cart } from '../models/cart';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItems$ = new Subject<Cart[]>();
  private cartItems: Cart[] = [];
  constructor() { }
  add(productId: string, quantity: number = 1) {
    this.cartItems.push(new Cart(productId, quantity ?? 1));
    this.cartItems$.next(this.cartItems);
  }
  get(): Observable<Cart[]> {
    return this.cartItems$.asObservable();
  }
}
