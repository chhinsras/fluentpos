import { Component, Output, EventEmitter, Input } from '@angular/core';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');
    this.authService.loadCurrentUser(token).subscribe(() => {
      console.log('User Loaded...');
    }, error => {
      console.log(error);
    })
  }

}

