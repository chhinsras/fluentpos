import { Component, OnInit } from '@angular/core';
import { ThemeService } from 'src/app/core/services/theme.service';

@Component({
  selector: 'app-pos',
  templateUrl: './pos.component.html',
  styleUrls: ['./pos.component.scss']
})
export class PosComponent implements OnInit {

  constructor(private themeService: ThemeService) { }

  ngOnInit(): void {
  }
  toggleDarkMode() {
    this.themeService.toggleDarkMode();
  }
}
