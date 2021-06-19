import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';

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
  constructor(private localStorageService: LocalStorageService) { }
  ngOnInit() {
    let themeVariant = this.localStorageService.getItem('themeVariant');
    this.darkModeIcon = themeVariant === 'dark-theme' ? 'bedtime' : 'brightness_5';
    this.isDarkMode = themeVariant === 'dark-theme' ? true : false;
  }
  toggleDarkMode() {
    this.isDarkMode = !this.isDarkMode;
    this.darkModeIcon = this.isDarkMode ? 'bedtime' : 'brightness_5'
    this.darkModelToggled.emit({ isDarkMode: this.isDarkMode, darkModelIcon: this.darkModeIcon });
  }
}
