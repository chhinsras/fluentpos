import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSidenav } from '@angular/material/sidenav';
import { Customer } from '../../models/customer';
import { PosService } from '../../services/pos.service';
import { CustomerSelectionComponent } from '../customer-selection/customer-selection.component';

@Component({
  selector: 'app-pos-toolbar',
  templateUrl: './pos-toolbar.component.html',
  styleUrls: ['./pos-toolbar.component.scss']
})
export class PosToolbarComponent implements OnInit {
  customer: Customer;
  constructor(public dialog: MatDialog,private posService:PosService) { }
  @Input() cart: MatSidenav;
  ngOnInit(): void {
    this.cart.toggle();
  }
  loadCustomer(customerId) {
    this.posService.getCustomerById(customerId).subscribe((res) => {
      this.customer = res.data;
    }
    )
  }
  openCustomerSelectionForm()
  {
    const dialogRef = this.dialog.open(CustomerSelectionComponent);
    dialogRef.afterClosed().subscribe((customer: Customer) => {
      if (customer) {
        this.loadCustomer(customer.id);
      }
    });
  }
}
