import {Component} from '@angular/core';
import {AuthService} from './core/services/auth.service';
import { BusyService } from './core/services/busy.service';
import {MultilingualService} from './core/services/multilingual.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private authService: AuthService, private translationService: MultilingualService, private busyService: BusyService) {
  }

  ngOnInit(): void {
    this.loadCurrentUser();
    this.loadDefaults();
  }

  loadDefaults() {
    this.translationService.loadDefaultLanguage();
  }

  loadCurrentUser() {
    this.authService.loadCurrentUser()
      .subscribe(() => {
      }, error => {
        console.log(error);
      });
  }

  isHttpRequestBusy() {
    return this.busyService.isLoading;
  }
}
