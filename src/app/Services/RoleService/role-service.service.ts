import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoleServiceService {
  private currentRoleSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);
  public currentRole = this.currentRoleSubject.asObservable();

  constructor(private jwtHelper: JwtHelperService) {
    const storedToken = localStorage.getItem('access_token');
    if (storedToken) {
      const decodedToken = this.jwtHelper.decodeToken(storedToken);
      this.currentRoleSubject.next(decodedToken?.role); 
    }
  }

  isRole(role: string): boolean {
    return this.currentRoleSubject.value === role;
  }

  hasRoles(roles: string[]): boolean {
    return roles.includes(this.currentRoleSubject.value as string);
  }
}
