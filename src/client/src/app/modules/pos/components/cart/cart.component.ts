import { Component, Input, OnInit } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { ToastrService } from 'ngx-toastr';
import { CartItem } from '../../models/cart';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  @Input() cart: MatSidenav;
  cartItems: CartItem[];
  total: number = 0;
  hasCartLoaded: boolean = false;
  constructor(private cartService: CartService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadCurrentCart();
    this.cartService.get().subscribe((data) => {
      this.total = 0;
      this.cartItems = data;
      this.hasCartLoaded = true;
      data.forEach(arg => {
        this.total += arg.total;
      });
    })
    this.cartService.getCartStatus().subscribe((data) => {

      if (data === 'loading') {
        this.hasCartLoaded = false;
        console.log(data);

      }
      else {
        this.hasCartLoaded = true;
      }
      console.log(this.hasCartLoaded);
    });
  }
  loadCurrentCart() {
    this.cartItems = this.cartService.loadCurrentCart();
    this.cartItems.forEach(arg => {
      this.total += arg.total;
    });
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
  isCustomerSelected() {
    const currentCustomer = this.cartService.getCurrentCustomer();
    if (!currentCustomer) {
      this.toastr.info('Select a customer first');
      return false;
    }
    return true;
  }
  saveOrUpdateCart() {
    if (this.isCustomerSelected()) {
      const customerId = this.cartService.getCurrentCustomer();
      const cart = this.cartService.loadCurrentCart();
    }
  }
}
