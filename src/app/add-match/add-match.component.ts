import { Component,OnInit } from '@angular/core';
import { MatchService } from '../Services/Matchservice/match.service';
import { CompetitionService } from '../Services/CompetitionService/competition.service'; 
import { TeamService } from '../Services/TeamService/team.service';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { addMatch } from '../models/addMatch';
import { team } from '../models/team';
import { competition } from '../models/competition';
import { MatchStatusList } from '../models/match.status.list';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-match',
  imports: [FormsModule, CommonModule],
  templateUrl: './add-match.component.html',
  styleUrl: './add-match.component.scss'
})
export class AddMatchComponent {
  newMatch: addMatch = {
    id: null,
    HomeTeamID: 0,
    AwayTeamID: 0,
    CompetitionID: 0,
    matchDate: new Date(),
    HomeTeamScore: null,
    AwayTeamScore: null,
    winner: null,
    Stadium: '',
    Status: 0
  };

  constructor(
    private matchService: MatchService,
    private teamService: TeamService,
    private competitionService: CompetitionService
  ) { }

  teams: team[] = [];
  competitions: competition[] = [];
  statuses = MatchStatusList;

  ngOnInit(): void {
    this.teamService.getAllTeams().subscribe(data => this.teams = data);
    this.competitionService.getAllCompetitions().subscribe(data => this.competitions = data);
  }

  addMatch(): void {
    this.matchService.addMatch(this.newMatch).subscribe({
      next: res => console.log('Match added successfully', res),
      error: err => console.error('Error adding match:', err)
    });
  }
}
