import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { loginUser } from '../../models/loginUser';
import { registerUser } from '../../models/registerUser';
import { authResponse } from '../../models/authResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl;
  private authData: authResponse | null = null;

  private isLoggedInSubject = new BehaviorSubject<boolean>(this.hasToken());
  isLoggedIn$ = this.isLoggedInSubject.asObservable();

  private authStatusSubject = new BehaviorSubject<boolean>(this.hasToken());
  authStatus$ = this.authStatusSubject.asObservable();

  constructor(private http: HttpClient) { }

  private hasToken(): boolean {
    return !!localStorage.getItem('token');
  }

  setAuthData(data: authResponse) {
    this.authData = data;
  }

  getAuthData(): authResponse | null {
    return this.authData;
  }

  login(user: loginUser): Observable<any> {
    return this.http.post<authResponse>(`${this.apiUrl}/account/login`, user, { withCredentials: true }).pipe(
      tap(response => {
        if (response.token)
          localStorage.setItem('token', response.token);
        if (response.userID)
          localStorage.setItem('userId', response.userID);
        this.authData = response;
        this.isLoggedInSubject.next(true);
        this.authStatusSubject.next(true);
      }),
      catchError(error => {
        this.authStatusSubject.next(false);
        throw error;
      })
    );
  }

  register(user: registerUser): Observable<any> {
    return this.http.post(`${this.apiUrl}/account/register`, user, { withCredentials: true });
  }

  logout(): Observable<any> {
    return this.http.post(`${this.apiUrl}/account/logout`, {}, { withCredentials: true }).pipe(
      tap(() => {
        localStorage.removeItem('token');
        localStorage.removeItem('userId');
        this.authData = null;
        this.isLoggedInSubject.next(false);
        this.authStatusSubject.next(false);
      }),
      catchError(() => {
        console.log('Logout failed, clearing auth data anyway');
        this.authData = null;
        this.isLoggedInSubject.next(false);
        this.authStatusSubject.next(false);
        return of(null);
      })
    );
  }

  checkEmailExists(email: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.apiUrl}/account/checkemailexists?email=${email}`);
  }
}
