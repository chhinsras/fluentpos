import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IdentityApiService } from 'src/app/core/api/identity/identity-api.service';
import { IResult } from 'src/app/core/models/wrappers/IResult';
import { ConfirmEmailParams, ConfirmPhoneNumberParams } from '../models/identityParams';
import { ResetPassword } from '../models/resetPassword';
import { User } from '../models/user';

@Injectable()
export class IdentityService {

  constructor(private api: IdentityApiService) { }

  RegisterUser(user: User): Observable<IResult<string>> {
    return this.api
      .Register(user)
      .pipe(map((response: IResult<string>) => response));
  }

  ConfirmEmail(confirmEmailParams: ConfirmEmailParams): Observable<IResult<string>> {
    let params = new HttpParams();
    if (confirmEmailParams.userId)
      params = params.append('userId', confirmEmailParams.userId);
    if (confirmEmailParams.code)
      params = params.append('code', confirmEmailParams.code);
    
    return this.api
      .ConfirmEmail(params)
      .pipe(map((response: IResult<string>) => response));
  }

  ConfirmPhoneNumber(confirmPhoneNumber: ConfirmPhoneNumberParams): Observable<IResult<string>> {
    let params = new HttpParams();
    if (confirmPhoneNumber.userId)
      params = params.append('userId', confirmPhoneNumber.userId);
    if (confirmPhoneNumber.code)
      params = params.append('code', confirmPhoneNumber.code);
    
    return this.api
      .ConfirmEmail(params)
      .pipe(map((response: IResult<string>) => response));
  }

  ForgotPassword(email: string) {
    return this.api
    .ForgotPassword(email)
    .pipe(map((response: IResult<string>) => response));
  }

  ResetPassword(resetPassword: ResetPassword) {
    return this.api
        .ResetPassword(resetPassword)
        .pipe(map((response: IResult<string>) => response));
  }
}
