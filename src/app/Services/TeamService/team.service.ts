import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { team } from '../../models/team';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TeamService {
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getAllTeams(): Observable<team[]>
  {
    return this.http.get<team[]>(`${this.apiUrl}/teams`, { withCredentials: true });
  }
}
