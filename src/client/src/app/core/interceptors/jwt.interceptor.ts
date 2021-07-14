import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {AuthService} from '../services/auth.service';
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {
  }

  private static addToken(request: HttpRequest<any>, token: string) {
    return request.clone({setHeaders: {'Authorization': `Bearer ${token}`}});
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    if (this.authService.isAuthenticated()) {
      const localToken = this.authService.getToken;
      request = JwtInterceptor.addToken(request, localToken);
    }

    if (!request.url.includes('token')) {
      const localToken = this.authService.getToken;
      const jwtService = new JwtHelperService();
      const expiringSoon =
        // has not expired
        !jwtService.isTokenExpired(localToken) &&
        // will expire in 10 min
        jwtService.isTokenExpired(localToken, -10 * 60);
      if (expiringSoon) {
        this.authService.tryRefreshingToken();
      }
    }

    return next.handle(request);
  }
}
