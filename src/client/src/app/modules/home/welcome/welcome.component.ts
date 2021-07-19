import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LogoutDialogComponent } from 'src/app/core/shared/components/logout-dialog/logout-dialog.component';
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
  constructor(private authService: AuthService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getUserDetails();
    setInterval(() => {
      this.date = new Date()
    }, 1000)
  }

  getUserDetails() {
    this.fullName = this.authService.getFullName;
    this.email = this.authService.getEmail;
  }
  onClickLogout() {
    const dialogRef = this.dialog.open(LogoutDialogComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.authService.logout();
    });
  }
}
