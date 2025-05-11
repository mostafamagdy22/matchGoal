import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 
import { Observable } from 'rxjs';
import { competition } from '../../models/competition';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CompetitionService {
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getAllCompetitions(): Observable<competition[]>
  {
    return this.http.get<competition[]>(`${this.apiUrl}/Competetions`, { withCredentials: true });
  }
}
