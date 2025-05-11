import { Component, OnInit } from '@angular/core';
import { PlayListService } from '../Services/PlayListService/play-list.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-play-list',
  imports: [CommonModule],
  templateUrl: './play-list.component.html',
  styleUrl: './play-list.component.scss'
})
export class PlayListComponent {
  userId: string | null = null;
  matches: any[] = [];  
  isLoading: boolean = true;
  errorMessage: string = '';
  constructor(private playListService: PlayListService, private router: Router) { }


  ngOnInit(): void {
    this.userId = localStorage.getItem('userId');
    if (this.userId) {
      this.loadPlayList(this.userId);
    } else {
      this.errorMessage = 'User not logged in.';
    }
  }

  loadPlayList(userId: string): void {
    this.playListService.getPlayList(userId).subscribe({
      next: (response) => {
        if (response && response.length > 0) {
          this.matches = response;
          this.isLoading = false;
        } else {
          console.error('No matches found');
        }
        
      },
      error: (error) => {
        this.errorMessage = 'Error fetching playlist.';
        this.isLoading = false;
      }
    });
  }

}
