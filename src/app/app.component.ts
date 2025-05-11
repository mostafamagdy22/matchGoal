import { Component,OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatchListComponent } from './match-list/match-list.component';
import { LoginComponent } from './login/login.component';
import { HeaderComponent } from './header/header.component';
import { AuthService } from './Services/AuthService/auth.service';
import { RoleServiceService } from './Services/RoleService/role-service.service';
import { MatchService } from './Services/Matchservice/match.service';
import { TokenInterceptor } from './token.interceptor';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatchListComponent, LoginComponent, HeaderComponent, HttpClientModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  canAddMatch: boolean = false;
  canUpdateMatch: boolean = false;
  canDeleteMatch: boolean = false;

  constructor(private roleService: RoleServiceService, private matchService: MatchService) { }

  ngOnInit(): void {
    this.checkPermissions();
  }

  checkPermissions(): void {
    if (this.roleService.hasRoles(['Admin', 'Manager'])) {
      this.canAddMatch = true;
      this.canUpdateMatch = true;
    }
    if (this.roleService.isRole('Admin')) {
      this.canDeleteMatch = true;
    }
  }

  //addMatch() {
  //  if (this.canAddMatch) {
  //    this.matchService.addMatch();
  //  } else {
  //    alert('You do not have permission to add a match.');
  //  }
  //}
}
