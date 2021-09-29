import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PaginatedFilter } from 'src/app/core/models/Filters/PaginatedFilter';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { Order } from '../../models/order';
import { OrderParams } from '../../models/orderParams';
import { ToastrService } from 'ngx-toastr';
import { Sort } from '@angular/material/sort';
import { TableColumn } from 'src/app/core/shared/components/table/table-column';
import { SalesService } from '../../services/sales.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
})
export class OrderComponent implements OnInit {
  orders: PaginatedResult<Order>;
  orderColumns: TableColumn[];
  orderParams = new OrderParams();
  searchString: string;

  constructor(
    public saleService: SalesService,
    public dialog: MatDialog,
    public toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.getOrders();
    this.initColumns();
  }

  getOrders(): void {
    this.saleService.getSales(this.orderParams).subscribe((result) => {
      this.orders = result;
    });
  }

  initColumns(): void {
    this.orderColumns = [
      { name: 'Id', dataKey: 'id', isSortable: true, isShowable: true },
      { name: 'ReferenceNumber', dataKey: 'referenceNumber', isSortable: true, isShowable: true },
      { name: 'TimeStamp', dataKey: 'timeStamp', isSortable: true, isShowable: true },
      { name: 'CustomerName', dataKey: 'customerName', isSortable: true, isShowable: true },
      { name: 'Total', dataKey: 'total', isSortable: true, isShowable: true },
      { name: 'IsPaid', dataKey: 'isPaid', isSortable: true, isShowable: true },
      { name: 'Action', dataKey: 'action', position: 'right' },
    ];
  }

  pageChanged(event: PaginatedFilter): void {
    this.orderParams.pageNumber = event.pageNumber;
    this.orderParams.pageSize = event.pageSize;
    this.getOrders();
  }

  sort($event: Sort): void {
    this.orderParams.orderBy = $event.active + ' ' + $event.direction;
    console.log(this.orderParams.orderBy);
    this.getOrders();
  }

  filter($event: string): void {
    this.orderParams.searchString = $event.trim().toLocaleLowerCase();
    this.orderParams.pageNumber = 0;
    this.orderParams.pageSize = 0;
    this.getOrders();
  }

  reload(): void {
    this.orderParams.searchString = '';
    this.orderParams.pageNumber = 0;
    this.orderParams.pageSize = 0;
    this.getOrders();
  }
}
