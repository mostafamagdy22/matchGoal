import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlayListService {
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }
  addToPlaylist(matchId: number, userId: string) {
    return this.http.post(`${this.apiUrl}/PlayList/addToPlaylist`, { matchId, userId }, { withCredentials: true, headers: {'Content-type': 'Application/json'} });
  }

  getPlayList(userId: string): Observable<any>
  {
    return this.http.get(`${this.apiUrl}/playlist/getPlaylist?userID=${userId}`, { withCredentials: true });
  }
}
