import { Component, Input, OnInit } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Cart } from '../../models/cart';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  @Input() cart: MatSidenav;
  cartItems: Cart[];
  total: number = 0;
  constructor(private cartService: CartService) { }

  ngOnInit(): void {
    this.cartService.get().subscribe((data) => {
      this.total = 0;
      this.cartItems = data;
      data.forEach(arg => {
        this.total += arg.total;
      });
    })
  }
  increaseQuantity(productId) {
    this.cartService.increase(productId);
  }
  reduceQuantity(productId) {
    this.cartService.reduce(productId);
  }
  removeItem(productId) {
    this.cartService.remove(productId);
  }
}
