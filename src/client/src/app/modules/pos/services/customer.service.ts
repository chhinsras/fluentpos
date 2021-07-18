import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CustomerApiService } from 'src/app/core/api/people/customer-api.service';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { Result } from 'src/app/core/models/wrappers/Result';
import { Customer } from '../models/customer';
import { CustomerParams } from '../models/customerParams';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private api: CustomerApiService) { }
  getCustomers(CustomerParams: CustomerParams): Observable<PaginatedResult<Customer>> {
    let params = new HttpParams();
    if (CustomerParams.searchString) params = params.append('searchString', CustomerParams.searchString);
    if (CustomerParams.pageSize) params = params.append('pageSize', CustomerParams.pageSize);
    return this.api.getAlls(params)
      .pipe(map((response: PaginatedResult<Customer>) => response));
  }
  getCustomerById(id: string): Observable<Result<Customer>> {
    return this.api.getById(id).pipe(
      map((response:Result<Customer>) => response)
    );
  }

}
