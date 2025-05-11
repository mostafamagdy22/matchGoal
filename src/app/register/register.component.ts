import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { registerUser } from '../models/registerUser';
import { emailExistsValidator } from '../validators/email-exists.validator';
import { AuthService } from '../Services/AuthService/auth.service';
import { Router } from '@angular/router';
import { loginUser } from '../models/loginUser';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  registerForm: FormGroup;
  registerUser: registerUser = {
    userName: '',
    email: '',
    password: '',
    confirmPassword: '',
  };
  loginUser: loginUser = {
    email: '',
    password: '',
  };


  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.registerForm = this.fb.group({
      userName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email], [emailExistsValidator(this.authService)]],
      password: ['', [
        Validators.required,
        Validators.minLength(6),
        Validators.pattern('^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$')
      ]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  get f() {
    return this.registerForm.controls;
  }

  onSubmit() {
    if (this.registerForm.invalid) {
      return;
    }

    // Check if passwords match
    if (this.registerForm.value.password !== this.registerForm.value.confirmPassword) {
      alert('Passwords do not match');
      return;
    }

    // Now registerUser object is populated from the form values using bracket notation
    this.registerUser = {
      userName: this.registerForm.value['userName'],
      email: this.registerForm.value['email'],
      password: this.registerForm.value['password'],
      confirmPassword: this.registerForm.value['confirmPassword'],
    };

    this.authService.register(this.registerUser).subscribe(
      response => {
        this.router.navigate(['/matches']);
        // After successful registration, you can log in the user automatically
        this.loginUser = {
          email: this.registerForm.value['email'],
          password: this.registerForm.value['password'],
        };
        this.authService.login(this.loginUser).subscribe(response => console.log("loged in"));
        console.log('Registration Successful', response);
      },
      error => {
        console.error('Registration Failed', error);
      }
    );

  }
}
