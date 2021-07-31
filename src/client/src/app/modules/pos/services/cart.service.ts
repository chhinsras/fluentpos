import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Cart } from '../models/cart';
import { Customer } from '../models/customer';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItems$ = new Subject<Cart[]>();
  private cartItems: Cart[] = [];
  private currentCustomer: Customer;
  constructor() { }
  add(product: Product, quantity: number = 1) {
    var foundItem = this.cartItems.find(a => a.productId == product.id);
    if (foundItem) {
      foundItem.quantity = foundItem.quantity + quantity;
    }
    else {
      this.cartItems.push(new Cart(product.id, quantity ?? 1, product.name, product.detail, product.price));
    }
    this.cartItems$.next(this.calculate(this.cartItems));
  }
  increase(productId: string, quantity: number = 1) {
    var foundItem = this.cartItems.find(a => a.productId == productId);
    if (foundItem) {
      foundItem.quantity = foundItem.quantity + quantity;
    }
    this.cartItems$.next(this.calculate(this.cartItems));
  }
  reduce(productId: string, quantity: number = 1) {
    var foundItem = this.cartItems.find(a => a.productId == productId);
    if (foundItem) {
      if (foundItem.quantity > 1) {
        foundItem.quantity = foundItem.quantity - quantity;
      }
      else {
        this.cartItems.splice(this.cartItems.indexOf(foundItem), 1)
      }
    }
    this.cartItems$.next(this.calculate(this.cartItems));
  }
  remove(productId: string) {
    var foundItem = this.cartItems.find(a => a.productId == productId);
    if (foundItem) {
      this.cartItems.splice(this.cartItems.indexOf(foundItem), 1)
    }
    this.cartItems$.next(this.cartItems);
  }
  get(): Observable<Cart[]> {
    return this.cartItems$.asObservable();
  }
  loadCurrentCart(): Cart[] {
    return this.calculate(this.cartItems);
  }
  setCurrentCustomer(customer : Customer) {
    this.currentCustomer = customer;
    console.log('customerId : ' + customer.id);
  }
  getCurrentCustomer() {
    return this.currentCustomer;
  }
  private calculate(cartItems: Cart[]): Cart[] {
    cartItems.forEach(function (part, index, theArray) {
      theArray[index].total = cartItems[index].quantity * cartItems[index].rate;
    });
    return cartItems;
  }
}
