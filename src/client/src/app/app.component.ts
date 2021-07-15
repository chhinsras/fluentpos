import {Component} from '@angular/core';
import {AuthService} from './core/services/auth.service';
import {MultilingualService} from './core/services/multilingual.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private authService: AuthService, private translationService: MultilingualService) {
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
}
