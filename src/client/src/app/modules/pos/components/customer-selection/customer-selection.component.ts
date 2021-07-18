import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { Customer } from '../../models/customer';
import { CustomerParams } from '../../models/customerParams';
import { CustomerService } from '../../services/customer.service';
@Component({
  selector: 'app-customer-selection',
  templateUrl: './customer-selection.component.html',
  styleUrls: ['./customer-selection.component.scss']
})
export class CustomerSelectionComponent implements OnInit {
  formTitle: string;
  customers: PaginatedResult<Customer>;
  customerParams = new CustomerParams();
  searchString: string;
  constructor(private customerService: CustomerService, public dialogRef: MatDialogRef<CustomerSelectionComponent>) { }

  ngOnInit(): void {
    this.formTitle = 'Customer Selection';
    this.customerParams.pageSize = 10;
    this.getCustomers();
  }
  getCustomers(): void {
    this.customerService.getCustomers(this.customerParams).subscribe((result) => {
      this.customers = result;
    });
  }
  public doFilter(): void {
    this.customerParams.searchString = this.searchString.trim().toLocaleLowerCase();
    this.getCustomers();
  }
  selectCustomer(customerId) {
    this.dialogRef.close(customerId);
  }
}
