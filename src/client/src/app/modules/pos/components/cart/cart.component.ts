import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSidenav } from '@angular/material/sidenav';
import { ToastrService } from 'ngx-toastr';
import { CartItem } from '../../models/cart';
import { CartService } from '../../services/cart.service';
import { CheckoutComponent } from '../checkout/checkout.component';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  @Input() cart: MatSidenav;
  cartItems: CartItem[];
  total: number = 0;
  constructor(public cartService: CartService, private toastr: ToastrService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadCurrentCart();
    this.cartService.get().subscribe((data) => {
      this.total = 0;
      this.cartItems = data;
      data.forEach(arg => {
        this.total += arg.total;
      });
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
  openCheckoutDialog() {
    const dialogRef = this.dialog.open(CheckoutComponent,{
      data: this.cartService.cartId,
    });
    dialogRef.afterClosed().subscribe(result => {
    });
  }
}
