import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSidenav } from '@angular/material/sidenav';
import { Customer } from '../../models/customer';
import { CartService } from '../../services/cart.service';
import { PosService } from '../../services/pos.service';
import { CustomerSelectionComponent } from '../customer-selection/customer-selection.component';

@Component({
  selector: 'app-pos-toolbar',
  templateUrl: './pos-toolbar.component.html',
  styleUrls: ['./pos-toolbar.component.scss']
})
export class PosToolbarComponent implements OnInit {
  customer: Customer;
  cartItemCount: number = 0;
  constructor(public dialog: MatDialog, private posService: PosService, private cartService: CartService) { }
  @Input() cart: MatSidenav;
  ngOnInit(): void {
    this.cart.toggle();
    this.cartService.get().subscribe(res => this.cartItemCount = res.length);
  }
  loadCustomer(customerId) {
    this.posService.getCustomerById(customerId).subscribe((res) => {
      if (res) {
        this.customer = res.data;
      }
    }
    )
  }
  removeCustomer() {
    if (this.customer) {
      this.customer = null;
    }
  }
  openCustomerSelectionForm() {
    const dialogRef = this.dialog.open(CustomerSelectionComponent);
    dialogRef.afterClosed().subscribe((customer: Customer) => {
      if (customer) {
        this.loadCustomer(customer.id);
      }
    });
  }
}
