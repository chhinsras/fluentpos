import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Customer } from '../../../admin/people/models/customer';
import { PosService } from '../../services/pos.service';
import { CustomerSelectionComponent } from '../customer-selection/customer-selection.component';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {
  customer: Customer;
  constructor(public dialog: MatDialog, private posService: PosService) { }

  ngOnInit(): void {
  }
  openForm(): void {
    const dialogRef = this.dialog.open(CustomerSelectionComponent);
    dialogRef.afterClosed().subscribe((customer: Customer) => {
      if (customer) {
        this.loadCustomer(customer.id);
      }
    });
  }
  removeCustomer() {
    this.customer = null;
  }
  loadCustomer(customerId) {
    this.posService.getCustomerById(customerId).subscribe((res) => {
      this.customer = res.data;
    }
    )
  }
}
