import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Token} from 'src/app/core/models/identity/token';
import {LocalStorageService} from 'src/app/core/services/local-storage.service';
import {environment} from 'src/environments/environment';
import {catchError, map, tap} from 'rxjs/operators';
import {Router} from '@angular/router';
import {Result} from '../models/wrappers/Result';
import {BehaviorSubject, of} from 'rxjs';
import {ToastrService} from 'ngx-toastr';
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable()
export class AuthService {

  baseUrl = environment.apiUrl;
  private currentUserTokenSource = new BehaviorSubject<string>(this.getLocalToken);
  public currentUserToken$ = this.currentUserTokenSource.asObservable();

  constructor(private http: HttpClient, private localStorage: LocalStorageService, private router: Router, private toastr: ToastrService) {
  }

  get getToken(): string {
    return this.currentUserTokenSource.getValue();
  }

  get getLocalToken(): string {
    return localStorage.getItem('token') ?? null;
  }

  loadCurrentUser(token: string) {
    const currentUserToken = !!(token) ? token : null;
    this.currentUserTokenSource.next(currentUserToken);
    return of(currentUserToken);
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    if (!!(token)) {
      const jwtService = new JwtHelperService();
      return !jwtService.isTokenExpired(token);
    }
    return false;
  }

  getFullName() {
    const decodedToken = this.getDecodedToken();
    return decodedToken?.fullName ?? '';
  }

  getEmail() {
    const decodedToken = this.getDecodedToken();
    return !!(decodedToken) ? decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'] : '';
  }

  login(values: any) {
    return this.http.post(this.baseUrl + 'identity/tokens', values)
      .pipe(
        tap((result: Result<Token>) => {
          if (result?.succeeded === true) {
            this.localStorage.setItem('token', result.data.token);
            this.localStorage.setItem('refreshToken', result.data.refreshToken);
            this.currentUserTokenSource.next(result.data.token);
            this.toastr.clear();
            this.toastr.info('User Logged In');
          }
        }),
        map((result: Result<Token>) => result ?? undefined)
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
    const jwtToken = this.localStorage.getItem('token');
    const refreshToken = this.localStorage.getItem('refreshToken');

    this.http.post(this.baseUrl + 'identity/tokens/refresh', {
      'refreshToken': refreshToken,
      'token': jwtToken
    })
      .pipe(
        tap((result: Result<Token>) => {
          if (result.succeeded) {
            this.localStorage.setItem('token', result.data.token);
            this.localStorage.setItem('refreshToken', result.data.refreshToken);
            this.currentUserTokenSource.next(result.data.token);
            this.toastr.clear();
            this.toastr.info('Refreshed Token');
          } else {
            this.logout();
            this.toastr.error('Something went wrong!');
          }
        }),
        catchError((error) => {
          console.error(error);
          this.logout();
          return of(null);
        }))
      .subscribe();
  }

  private getDecodedToken() {
    let token = this.localStorage.getItem('token');
    // if token is undefined, avoid exception
    if (!(token)) {
      return undefined;
    }

    const jwtService = new JwtHelperService();
    const decodedToken = jwtService.decodeToken(token);
    console.log(decodedToken);
    return decodedToken;
  }

}
