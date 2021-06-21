import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService) {
    this.initializeForm();
  }

  ngOnInit(): void {
  }

  initializeForm() {
    this.loginForm = new FormGroup({
      email: new FormControl('', Validators.required && Validators.email),
      password: new FormControl('', Validators.required)
    })
  }

  onSubmit()
  {
    this.authService.login(this.loginForm.value).subscribe((result) => {
      if (result.succeeded){
        this.toastr.success('User Logged In', "Authentincation");
        this.router.navigateByUrl('/home');
      } else {
        console.log('User login failed');
      }
    }, error => {
      console.log(error);
    });
  }
}
