import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {PaginatedFilter} from 'src/app/core/models/Filters/PaginatedFilter';
import {PaginatedResult} from 'src/app/core/models/wrappers/PaginatedResult';
import {DeleteDialogComponent} from '../../../shared/components/delete-dialog/delete-dialog.component';
import {Customer} from '../../models/customer';
import {CustomerParams} from '../../models/customerParams';
import {CustomerService} from '../../services/customer.service';
import {CustomerFormComponent} from './customer-form/customer-form.component';
import {ToastrService} from 'ngx-toastr';
import {MatTableDataSource} from '@angular/material/table';
import {MatSort, Sort} from '@angular/material/sort';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit, AfterViewInit {

  customers: PaginatedResult<Customer>;
  customerColumns: string[] = ['id', 'name', 'phone', 'email',  'type', 'action'];
  customerParams = new CustomerParams();
  dataSource = new MatTableDataSource<Customer>([]);
  searchString: string;
  @ViewChild(MatSort) sort: MatSort;

  constructor(public customerService: CustomerService, public dialog: MatDialog, public toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.getCustomers();
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  getCustomers(): void {
    this.customerService.getCustomers(this.customerParams).subscribe((result) => {
      this.customers = result;
      this.dataSource.data = this.customers.data;
    });
  }

  handlePageChange(event: PaginatedFilter): void {
    this.customerParams.pageNumber = event.pageNumber;
    this.customerParams.pageSize = event.pageSize;
    this.getCustomers();
  }

  openCustomerForm(customer?: Customer): void {
    const dialogRef = this.dialog.open(CustomerFormComponent, {
      data: customer
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getCustomers();
      }
    });
  }

  openDeleteConfirmationDialog(id: string): void {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: 'Do you confirm the removal of this customer?'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.removeCustomer(id);
      }
    });
  }

  removeCustomer(id: string): void {
    this.customerService.deleteCustomer(id).subscribe(() => {
      this.getCustomers();
      this.toastr.info('Customer Removed');
    });
  }

  doSort(sort: Sort): void {
    this.customerParams.orderBy = sort.active + ' ' + sort.direction;
    this.getCustomers();
  }

  public doFilter(): void {
    this.customerParams.searchString = this.searchString.trim().toLocaleLowerCase();
    this.customerParams.pageNumber = 0;
    this.customerParams.pageSize = 0;
    this.getCustomers();
  }

  public reload(): void {
    this.searchString = this.customerParams.searchString = '';
    this.customerParams.pageNumber = 0;
    this.customerParams.pageSize = 0;
    this.getCustomers();
  }
}
