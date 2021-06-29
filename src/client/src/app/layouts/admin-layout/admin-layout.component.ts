import { OverlayContainer } from '@angular/cdk/overlay';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {

  title = 'angular-material-boilerplate';
  themeVariant: string = '';
  darkModeIcon: string = '';
  constructor(private localStorageService: LocalStorageService,private dialog: MatDialog, private overlay: OverlayContainer) { }
  ngOnInit() {
    this.themeVariant = this.localStorageService.getItem('themeVariant');
  }
  onDarkModeToggled(data: { isDarkMode: boolean, darkModeIcon: string }) {
    console.log(data.isDarkMode);
    
    this.themeVariant = data.isDarkMode ? 'dark-theme' : '';
    if (data.isDarkMode) {
      this.overlay.getContainerElement().classList.add('dark-theme');
    } else {
      this.overlay.getContainerElement().classList.remove('dark-theme');
    }
    this.localStorageService.setItem('themeVariant', this.themeVariant);
    this.darkModeIcon = data.darkModeIcon;
  }

}
