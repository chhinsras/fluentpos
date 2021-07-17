import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Customer } from '../../admin/people/models/customer';
import { CustomerSelectionComponent } from '../customer-selection/customer-selection.component';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {
  customer: Customer;
  constructor(public dialog: MatDialog) { }

  ngOnInit(): void {
    console.log(this.customer);
  }
  openForm(): void {
    const dialogRef = this.dialog.open(CustomerSelectionComponent);
    dialogRef.afterClosed().subscribe((result) => {
    });
    this.selectCustomer();
  }
  removeCustomer() {
    this.customer = null;
    console.log(this.customer);
  }
  selectCustomer() {
    this.customer = {
      id: "13243513",
      name: "Mukesh Murugan",
      email: "iammukeshm@gmail.com",
      type: "Random",
      phone: "789456123",
      imageUrl: ""
    }
    console.log(this.customer);
  }
}
