import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { Order } from '../../../models/order';
import { SalesService } from '../../../services/sales.service';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.scss']
})
export class OrderDetailComponent implements OnInit {
  order: Order;

  constructor(@Inject(MAT_DIALOG_DATA) public data: Order, private toastr: ToastrService, private saleService: SalesService) {
  }

  ngOnInit(): void {
    this.saleService.getById(this.data.id).subscribe((response => {
      this.order = response.data;
    }));
  }

}
