import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((response) => {
        if (response.error.errorCode === 400) {
          if (response.error.messages) {
            throw response.error.messages;
          } 
        }
        if (response.error.errorCode === 401) {
          this.toastr.error(response.error.exception, response.error.errorCode.toString());
        }
        if (response.error.errorCode === 404) {
          this.router.navigateByUrl('/not-found');
        }
        if (response.error.errorCode === 500) {
          this.toastr.error(response.error.exception)
        }
        return throwError(response.error);
      })  
    );
  }
}
