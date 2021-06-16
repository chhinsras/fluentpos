import { Component, Output, EventEmitter, Input } from '@angular/core';
import { LocalStorageService } from './core/services/local-storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'angular-material-boilerplate';
  themeVariant: string = '';
  darkModeIcon: string = '';
  constructor(private localStorageService: LocalStorageService) { }
  ngOnInit() {
    this.themeVariant = this.localStorageService.getItem('themeVariant');
  }
  onDarkModeToggled(data: { isDarkMode: boolean, darkModeIcon: string }) {
    console.log(data.isDarkMode);
    this.themeVariant = data.isDarkMode ? 'dark-theme' : '';
    this.localStorageService.setItem('themeVariant', this.themeVariant);
    this.darkModeIcon = data.darkModeIcon;
  }
}

