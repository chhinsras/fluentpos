import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CartItem } from '../../models/cart';
import { CheckOut } from '../../models/checkOut';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: CheckOut) { }
  cartItems: CartItem[];
  cartId: string;
  ngOnInit(): void {
    this.cartId = this.data.cartId;
    this.cartItems = this.data.cartItems;
  }

}
