import { Component } from '@angular/core';
import { AuthService } from '../Services/AuthService/auth.service';
import { loginUser } from '../models/loginUser';
import { FormBuilder, FormGroup, Validators,ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
    loginForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService,private router: Router) {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }
  onLogin() {
    if (this.loginForm.valid) {
      const user: loginUser = this.loginForm.value;
      this.authService.login(user).subscribe(
        (response) => {
          console.log('Login Successful', response);
          this.authService.setAuthData(response);
          if (response.isAuthenticated)
               this.router.navigate(['matches']);
        },
        (error) => {
          console.error('Login Error', error);
        }
      );
    }
}
}
