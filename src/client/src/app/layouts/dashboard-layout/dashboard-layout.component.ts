import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';

@Component({
  selector: 'app-dashboard-layout',
  templateUrl: './dashboard-layout.component.html',
  styleUrls: ['./dashboard-layout.component.scss']
})
export class DashboardLayoutComponent implements OnInit {

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
