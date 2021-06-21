import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Token } from 'src/app/core/models/identity/token';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Result } from '../models/wrappers/Result';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private localStorage: LocalStorageService, private router: Router) { }

  login(values: any)
  {
    return this.http.post(this.baseUrl + 'identity/tokens', values).pipe(
      map((result: Result<Token>) => {
        if (result.succeeded)
        {
          this.localStorage.setItem('token', result.data.token);
        }
        return result;
      })
    );
  }

  logout()
  {
    this.localStorage.removeItem('token');
    this.router.navigateByUrl('/login');
  }
}
