import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSidenav } from '@angular/material/sidenav';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';
import { ThemeService } from 'src/app/core/services/theme.service';
import { CustomerService } from 'src/app/modules/admin/people/services/customer.service';
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
  constructor(private localStorageService: LocalStorageService, public dialog: MatDialog, private posService: PosService, private cartService: CartService, private themeService: ThemeService, private customerService: CustomerService) { }
  @Input() cart: MatSidenav;
  @Input() darkModeIcon: string;
  @Input() isDarkMode: boolean;
  ngOnInit(): void {
    this.cart.toggle();
    this.cartService.get().subscribe(res => this.cartItemCount = res.length);
    this.customer = this.cartService.getCurrentCustomer();
    let themeVariant = this.localStorageService.getItem('themeVariant');
    this.darkModeIcon = themeVariant === 'dark-theme' ? 'bedtime' : 'wb_sunny';
    this.isDarkMode = themeVariant === 'dark-theme';
    //Test
    this.customerService.getCustomerById('01c8801c-e0a0-4d45-8e0a-d14d3f88eb27').subscribe((result) => {
      if (result) {
        this.customer = result.data;
        this.cartService.getCustomerCart('01c8801c-e0a0-4d45-8e0a-d14d3f88eb27');
      }
    })

  }
  toggleDarkMode() {
    this.isDarkMode = this.themeService.toggleDarkMode();
    this.darkModeIcon = this.isDarkMode ? 'bedtime' : 'wb_sunny'
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
        this.customer = customer;
        this.cartService.setCurrentCustomer(customer);
        this.cartService.getCustomerCart(customer.id);
      }
    });
  }
}
