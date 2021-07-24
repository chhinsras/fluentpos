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
  constructor(private cartService: CartService) { }

  ngOnInit(): void {
    this.cartService.get().subscribe((data) => {
      this.cartItems = data;
      console.log(data);
    })
  }
  increaseQuantity(productId)
  {
   this.cartService.add(productId); 
  }
  reduceQuantity(productId)
  {
    this.cartService.reduce(productId); 
  }
}
