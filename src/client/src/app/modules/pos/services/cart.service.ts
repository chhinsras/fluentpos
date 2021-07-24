import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Cart } from '../models/cart';
import { PosService } from './pos.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItems$ = new Subject<Cart[]>();
  private cartItems: Cart[] = [];
  constructor(private posService: PosService) { }
  add(productId: string, quantity: number = 1) {
    var foundItem = this.cartItems.find(a => a.productId == productId);
    if (foundItem) {
      foundItem.quantity = foundItem.quantity + quantity;
    }
    else {
      this.posService.getProductById(productId).subscribe((result) => {
        this.cartItems.push(new Cart(productId, quantity ?? 1, result.data.name, result.data.detail));
      })
    }
    this.cartItems$.next(this.cartItems);
  }
  reduce(productId: string, quantity: number = 1) {
    var foundItem = this.cartItems.find(a => a.productId == productId);
    if (foundItem) {
      if(foundItem.quantity > 1)
      {
        foundItem.quantity = foundItem.quantity - quantity;        
      }
      else
      {
        this.cartItems.splice(this.cartItems.indexOf(foundItem))
      }
    }
    this.cartItems$.next(this.cartItems);
  }
  get(): Observable<Cart[]> {
    return this.cartItems$.asObservable();
  }
}
