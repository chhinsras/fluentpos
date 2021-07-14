import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { take } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUserToken: string;
    this.authService.currentUserToken$.pipe(take(1)).subscribe(token => currentUserToken = token);
    if (currentUserToken) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUserToken}`
        }
      });
      if (!request.url.includes("token")) {
        const jwtService = new JwtHelperService();
        const expirationDate = jwtService.getTokenExpirationDate(currentUserToken);
        const currentDate = new Date();
        const difference = expirationDate.getTime() - currentDate.getTime(); // This will give difference in milliseconds
        const resultInMinutes = Math.round(difference / 60000);
        if (resultInMinutes < 10) {
          this.authService.tryRefreshingToken();
        }
      }
    }
    return next.handle(request);
  }
}
