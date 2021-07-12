import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Token } from 'src/app/core/models/identity/token';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Result } from '../models/wrappers/Result';
import { BehaviorSubject, of, ReplaySubject } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { JwtHelperService } from '@auth0/angular-jwt';
import { take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = environment.apiUrl;
  private currentUserTokenSource = new ReplaySubject<string>(1);
  currentUserToken$ = this.currentUserTokenSource.asObservable();

  constructor(private http: HttpClient, private localStorage: LocalStorageService, private router: Router, private toastr: ToastrService) { }

  loadCurrentUser(token: string) {
    if (token == null) {
      this.currentUserTokenSource.next(null);
    }
    this.currentUserTokenSource.next(token);
    return of(null);
  }
  private getDecodedToken() {
    let token: string;
    token = this.localStorage.getItem('token');
    const jwtService = new JwtHelperService();
    const decodedToken = jwtService.decodeToken(token);
    console.log(decodedToken);
    return decodedToken;
  }
  getFullName() {
    var decodedToken = this.getDecodedToken();
    return decodedToken.fullName;
  }
  getEmail() {
    var decodedToken = this.getDecodedToken();
    return decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'];
  }
  login(values: any) {
    return this.http.post(this.baseUrl + 'identity/tokens', values).pipe(
      map((result: Result<Token>) => {
        if (result.succeeded) {
          this.localStorage.setItem('token', result.data.token);
          this.localStorage.setItem('refreshToken', result.data.refreshToken);
          this.currentUserTokenSource.next(result.data.token);
          this.toastr.clear();
          this.toastr.info('User Logged In');
        }
        return result;
      })
    );
  }

  logout() {
    this.localStorage.removeItem('token');
    this.localStorage.removeItem('refreshToken');
    this.currentUserTokenSource.next(null);
    this.toastr.clear();
    this.toastr.info('User Logged Out');
    this.router.navigateByUrl('/login');
  }
  tryRefreshingToken() {
    var jwtToken = this.localStorage.getItem('token');
    var refreshToken = this.localStorage.getItem('refreshToken');
    this.http.post(this.baseUrl + 'identity/tokens/refresh',
      {
        "refreshToken": refreshToken,
        "token": jwtToken
      }).subscribe(
        (result: Result<Token>) => {
          if (result.succeeded) {
            this.localStorage.setItem('token', result.data.token);
            this.localStorage.setItem('refreshToken', result.data.refreshToken);
            this.currentUserTokenSource.next(result.data.token);
            this.toastr.clear();
            this.toastr.info('Refreshed Token');
          }
          else {
            this.logout();
            this.toastr.error("Something went wrong!");
          }
        },
        (error: Result<Token>) => {
          this.logout();
        }

      );
  }
}
