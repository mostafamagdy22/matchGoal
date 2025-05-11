import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Match } from '../../models/match';
import { addMatch } from '../../models/addMatch';
import { Observable } from 'rxjs';
import { PaginatedResponse } from '../../models/PaginatedResponse';
import { environment } from '../../../environments/environment';
import { updatematchform } from '../../models/updatematchform';

@Injectable({
  providedIn: 'root'
})
export class MatchService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }
  getMatches(page:number,pageSize:number): Observable<PaginatedResponse<Match>>
  {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString())
      .set('include',"homeTeam,awayTeam,competition");
    return this.http.get<PaginatedResponse<Match>>(`${this.apiUrl}/matches`, { params, withCredentials: true });
  }

  getMatchByID(id: number): Observable<Match> {
    return this.http.get<Match>(`${this.apiUrl}/matches/match/${id}`);
  }

  addMatch(match: addMatch): Observable<any> {
    return this.http.post(`${this.apiUrl}/matches/AddMatch`, match, { withCredentials:true });
  }

  updateMatch(match: updatematchform): Observable<updatematchform> {
    console.log("update ressss",match);
    return this.http.put<updatematchform>(`${this.apiUrl}/matches/UpdateMatch/${match.id}`, match, {
      withCredentials: true,
      headers: { 'Content-Type': 'application/json'}
    });
  }
}
