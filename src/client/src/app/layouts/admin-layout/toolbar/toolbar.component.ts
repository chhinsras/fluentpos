import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSidenav } from '@angular/material/sidenav';
import { AuthService } from 'src/app/core/services/auth.service';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';
import { LogoutDialogComponent } from 'src/app/modules/admin/shared/components/logout-dialog/logout-dialog.component';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit {
  @Input() darkModeIcon: string;
  @Input() inputSideNav: MatSidenav;
  @Input() isDarkMode: boolean;
  @Output('darkModelToggled') darkModelToggled = new EventEmitter<{ isDarkMode: boolean, darkModelIcon: string }>();
  
  siteLanguage: string = 'English';
  siteLocale: string;
  languageList = [
    { code: 'en', label: 'English' },
    { code: 'km', label: 'Khmer' },
    { code: 'fr', label: 'Français' }
  ];

  constructor(private localStorageService: LocalStorageService, public authService: AuthService,public dialog: MatDialog) { }
  ngOnInit() {
    let themeVariant = this.localStorageService.getItem('themeVariant');
    this.darkModeIcon = themeVariant === 'dark-theme' ? 'bedtime' : 'brightness_5';
    this.isDarkMode = themeVariant === 'dark-theme' ? true : false;
    
    this.siteLocale = window.location.pathname.split('/')[1];
    this.siteLanguage = this.languageList.find(f => f.code === this.siteLocale).label;
  }
  toggleDarkMode() {
    this.isDarkMode = !this.isDarkMode;
    this.darkModeIcon = this.isDarkMode ? 'bedtime' : 'brightness_5'
    this.darkModelToggled.emit({ isDarkMode: this.isDarkMode, darkModelIcon: this.darkModeIcon });
  }
  openLogoutDialog()
  {
    const dialogRef = this.dialog.open(LogoutDialogComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.authService.logout();
    });
  }
}
