import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Token } from 'src/app/core/models/identity/token';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Result } from '../models/wrappers/Result';
import { BehaviorSubject, of, ReplaySubject } from 'rxjs';
import { JwtService } from './jwt.service';
import { ToastrService } from 'ngx-toastr';
import { RefreshTokenRequest } from '../models/identity/refreshTokenRequest';

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
    // TODO: var decodedToken = this.jwtService.DecodeToken(token);
    // TODO: check if token is expired / invalid

    this.currentUserTokenSource.next(token);
    return of(null);
  }

  login(values: any) {
    return this.http.post(this.baseUrl + 'identity/tokens', values).pipe(
      map((result: Result<Token>) => {
        if (result.succeeded) {
          this.localStorage.setItem('token', result.data.token);
          this.localStorage.setItem('refreshToken', result.data.refreshToken);
          this.currentUserTokenSource.next(result.data.token);
          this.toastr.clear();
          this.toastr.success('User Logged In');
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
    this.toastr.warning('User Logged Out');
    this.router.navigateByUrl('/login');
  }
  tryRefreshingToken() {
    var jwtToken = this.localStorage.getItem('token');
    var refreshToken = this.localStorage.getItem('refreshToken');
    this.http.post(this.baseUrl + 'identity/tokens/refresh', {
      "refreshToken": "mSmcD3c5adXehpqdJOMGQdxlgOdBW5wJQbSdE3jo1bQ=",
      "token": "1233"
    }).subscribe(
      (result: Result<Token>) => {
        if (result.succeeded) {
          this.localStorage.setItem('token', result.data.token);
          this.localStorage.setItem('refreshToken', result.data.refreshToken);
          this.currentUserTokenSource.next(result.data.token);
          this.toastr.clear();
          this.toastr.success('User Logged In');
        }
        else {
          this.router.navigate(['login']);
          this.toastr.error("Something went wrong!");
        }
      },
      (error: Result<Token>) => {
        this.router.navigate(['login']); 
      }

    );
    return true;
  }
}
