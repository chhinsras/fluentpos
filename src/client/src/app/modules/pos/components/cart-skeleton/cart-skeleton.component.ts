import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cart-skeleton',
  templateUrl: './cart-skeleton.component.html',
  styleUrls: ['./cart-skeleton.component.scss']
})
export class CartSkeletonComponent implements OnInit {
  cartItems = new Array(6);
  constructor() { }

  ngOnInit(): void {
  }

}
