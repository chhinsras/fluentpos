import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CheckoutApiService } from 'src/app/core/api/checkout/checkout-api.service';
import { Result } from 'src/app/core/models/wrappers/Result';
import { CheckOutParams } from '../models/checkOutParams';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  constructor(public checkOutApi: CheckoutApiService) { }
  submitOrder(cartId:string): Observable<Result<string>> {
    return this.checkOutApi.submitOrder(cartId)
      .pipe(map((response: Result<string>) => {
        return response;
      }));
  }
}
