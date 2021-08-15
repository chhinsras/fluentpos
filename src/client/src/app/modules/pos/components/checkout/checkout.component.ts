import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Customer } from 'src/app/modules/admin/people/models/customer';
import { CartItem } from '../../models/cart';
import { CheckOut } from '../../models/checkOut';
import { CartService } from '../../services/cart.service';
import { CheckoutService } from '../../services/checkout.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: CheckOut, public dialog: MatDialog, public cartService: CartService, public checkOutService: CheckoutService) { }
  cartItems: CartItem[];
  cartId: string;
  customer: Customer;
  isBeingProcessed: boolean = false;
  ngOnInit(): void {
    this.cartId = this.data.cartId;
    this.cartItems = this.data.cartItems;
    this.customer = this.cartService.currentCustomer;
  }
  submitOrder() {
    this.isBeingProcessed = true;
    this.checkOutService.submitOrder(this.cartId).subscribe((result) => {
      this.cartService.reset();
      this.dialog.closeAll();
    }).add(() => this.isBeingProcessed = false);
  }
}
