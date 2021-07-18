import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Customer } from '../../../admin/people/models/customer';
import { CustomerService } from '../../services/customer.service';
import { CustomerSelectionComponent } from '../customer-selection/customer-selection.component';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {
  customer: Customer;
  constructor(public dialog: MatDialog, private customerService: CustomerService) { }

  ngOnInit(): void {
  }
  openForm(): void {
    const dialogRef = this.dialog.open(CustomerSelectionComponent);
    dialogRef.afterClosed().subscribe((customerId) => {
      this.loadCustomer(customerId);
    });
  }
  removeCustomer() {
    this.customer = null;
  }
  loadCustomer(customerId) {
    this.customerService.getCustomerById(customerId).subscribe((res) => {
      this.customer = res.data;
    }
    )
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
  }
}
