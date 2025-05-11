import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../Services/AuthService/auth.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  isAuthenticated$: Observable<boolean>;
  constructor(private authService: AuthService,private router: Router) {
    this.isAuthenticated$ = authService.authStatus$;
  }
  logout(): void {
    this.authService.logout().subscribe({
      next: () => {
        console.log('Logout completed, navigating to home');
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.error('Logout error:', error);
        this.router.navigate(['/']);
      }
    });
  }
}

