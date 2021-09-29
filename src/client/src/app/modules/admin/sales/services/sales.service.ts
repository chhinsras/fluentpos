import { OrderParams } from './../models/orderParams';
import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { SalesApiService } from 'src/app/core/api/sales/sales-api.service';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class SalesService {

  constructor(private api: SalesApiService){
  }

  getSales(orderParams: OrderParams): Observable<PaginatedResult<Order>> {
    let params = new HttpParams();
    if (orderParams.searchString) params = params.append('searchString', orderParams.searchString);
    if (orderParams.pageNumber) params = params.append('pageNumber', orderParams.pageNumber.toString());
    if (orderParams.pageSize) params = params.append('pageSize', orderParams.pageSize.toString());
    if (orderParams.orderBy) params = params.append('orderBy', orderParams.orderBy.toString());
    return this.api.getAlls(params)
      .pipe(map((response: PaginatedResult<Order>) => response));
  }

}
