import { Component, Input,inject } from '@angular/core';
import { MatchService } from '../Services/Matchservice/match.service';
import { Injectable } from '@angular/core';
import { Match } from '../models/match';
import { CommonModule } from '@angular/common';
import { PaginatedResponse } from '../models/PaginatedResponse';
import { UserService } from '../Services/UserService/user.service';
import { Router } from '@angular/router';
import { PlayListService } from '../Services/PlayListService/play-list.service';
import { BehaviorSubject } from 'rxjs';
@Component({
  selector: 'app-match-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './match-list.component.html',
  styleUrl: './match-list.component.scss'
})
export class MatchListComponent {
  isAdmin$ = new BehaviorSubject<boolean>(false);
  isAuth$ = new BehaviorSubject<boolean>(false);
  matches: Match[] = [];
  totalPages: number = 0;
  totalItems: number = 0;
  currentPage: number = 1;
  pageSize: number = 10;
  isLoading: boolean = false;

  constructor(private playListService: PlayListService, private router: Router, private userService: UserService, private matchService: MatchService) { }
  ngOnInit(): void
  {
    this.userService.getUserDetails().subscribe(data => {
      this.isAdmin$.next(data.isAdmin);
      this.isAuth$.next(!!localStorage.getItem('userId'));
    });
    this.loadMatches(this.currentPage);
  }

  getMatchByID(id: number) {
    this.matchService.getMatchByID(id).subscribe({
      next: (match) => {
        console.log('Match:', match);
      },
      error: (err) => {
        console.error('Error fetching match:', err);
      }
    });
  }

  loadMatches(page: number): void {
    console.log("fetch matches");
    this.isLoading = true;
    this.matchService.getMatches(this.currentPage, this.pageSize).subscribe({
      next: (response: PaginatedResponse<Match>) => {
        this.matches = response.data;
        this.isLoading = false;
        console.log(`testttt: `, this.matches);
        this.totalItems = response.totalItems;
        this.totalPages = response.totalPages;
        this.currentPage = response.page;
        console.log(response);
      },
      error: (error) => {
        console.error('Error loading matches ...', error);
      }
    });
  }
  goToPlayList() {
    this.router.navigate(['/playlist']);
  }
  getPageNumbers(): number[] {
    console.log('getPageNumbers called, totalPages:', this.totalPages);
    const pages: number[] = [];
    for (let i = 1; i <= this.totalPages; i++) {
      pages.push(i);
    }
    console.log('Returning pages:', pages);
    return pages;
  }

  goToPage(page: number): void
  {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadMatches(page);
    }
  }

  goToAddMatch(): void {
    const url = 'matches/addmatch'; 
    window.location.href = url;
  }

  nextPage(): void
  {
    this.goToPage(this.currentPage + 1);
  }

  previousPage(): void {
    this.goToPage(this.currentPage - 1);
  }
  goToUpdateMatch(id: number): void {
    this.router.navigate(['/matches/updatematch',id])
  }
  addToPlaylist(match: any) {
    const userID = localStorage.getItem("userId");

    if (userID === null || userID === undefined) {
      console.error('User ID is null or undefined');
      return; 
    }

    this.playListService.addToPlaylist(match.id, userID).subscribe(response => {
      console.log('Match added to playlist!', response);
    },
      error => {
        console.error('Error adding to playlist',error);
      })
    console.log('Added to playlist:', match);
  }
}
