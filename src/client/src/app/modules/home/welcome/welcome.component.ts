import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../../core/services/auth.service";

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {

  fullName: string = '';
  email: string;
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.getUserDetails();
  }

  getUserDetails() {
    this.fullName = this.authService.getFullName();
    this.email = this.authService.getEmail();
  }
  onClickLogout() {
    this.authService.logout();
  }
}
