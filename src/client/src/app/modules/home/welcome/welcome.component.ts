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
  date:Date=new Date();
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.getUserDetails();
    setInterval(() => {
      this.date = new Date()
    }, 1000)
  }

  getUserDetails() {
    this.fullName = this.authService.getFullName();
    this.email = this.authService.getEmail();
  }
  onClickLogout() {
    this.authService.logout();
  }
}
