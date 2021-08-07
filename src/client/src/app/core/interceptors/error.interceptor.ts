import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService, private authService: AuthService) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((response) => {
        switch (response.error.errorCode) {
          case 400:
            if (response.error.messages) {
              this.toastr.error(response.error.exception);
            }
            else {
              this.toastr.error(response.error.exception);
            }
            break;
          case 401:
            this.authService.tryRefreshingToken();
            break;
          case 403:
            this.toastr.error(response.error.exception);
            break;
          case 404:
            this.router.navigateByUrl('/not-found');
            break;
          case 500:
            console.log(response.error.exception);
            this.toastr.error('Something Went Wrong');
            break;
          default:
            this.toastr.error('Something Went Wrong');
            break;
        }
        return throwError(response.error);
      })
    );
  }
}
