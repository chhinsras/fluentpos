import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Customer } from 'src/app/modules/admin/people/models/customer';
import { CustomerService } from 'src/app/modules/admin/people/services/customer.service';
import { CartItem } from '../../models/cart';
import { CheckOut } from '../../models/checkOut';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: CheckOut, public customerService:CustomerService, public cartService:CartService) { }
  cartItems: CartItem[];
  cartId: string;
  customer: Customer;
  ngOnInit(): void {
    this.cartId = this.data.cartId;
    this.cartItems = this.data.cartItems;
    this.customer = this.cartService.currentCustomer;
  }

}
