import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatchService } from '../Services/Matchservice/match.service';
import { Match } from '../models/match';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { updatematchform } from '../models/updatematchform';
import { TeamService } from '../Services/TeamService/team.service';
import { CompetitionService } from '../Services/CompetitionService/competition.service';
import { MatchStatusList, MatchStatusListType } from '../models/match.status.list';

@Component({
  selector: 'app-update-match',
  imports: [CommonModule, FormsModule],
  templateUrl: './update-match.component.html',
  styleUrl: './update-match.component.scss'
})
export class UpdatematchComponent {
  match: Match | null = null;
  isLoading: boolean = false;
  teams: any[] = [];
  competitions: any[] = [];
  statusEnum = MatchStatusList;
  updateMatchForm: updatematchform = {
    id: 0,
    matchDate: new Date(),
    homeTeamScore: 0,
    awayTeamScore: 0,
    status: 0,
    stadium: '',
    homeTeamID: 0,
    awayTeamID: 0,
    competitionID: 0,
    winnerTeamID: null,
  };
  constructor(
    private route: ActivatedRoute,
    private matchService: MatchService,
    private router: Router,
    private teamService: TeamService,
    private competitionService: CompetitionService,
  ) { }

  ngOnInit(): void {
    const matchId = this.route.snapshot.paramMap.get('id');
    if (matchId) {
      const matchIdNumber = +matchId;
      this.isLoading = true;
      this.matchService.getMatchByID(matchIdNumber).subscribe(
        (data) => {
          this.updateMatchForm = {
            id: data.id,
            matchDate: data.matchDate,
            homeTeamScore: data.homeTeamScore,
            awayTeamScore: data.awayTeamScore,
            status: data.status,
            stadium: data.stadium,
            homeTeamID: data.homeTeam.id, 
            awayTeamID: data.awayTeam.id,
            competitionID: data.competition.id, 
            winnerTeamID: data.winner ? data.winner.id : null,
          };
          this.isLoading = false;
        },
        (error) => {
          console.error('Error fetching match data', error);
          this.isLoading = false;
        }
      );
    }

    this.teamService.getAllTeams().subscribe(
      (teamsData) => {
        this.teams = teamsData;
      },
      (error) => {
        console.error('Error fetching teams', error);
      }
    );
    this.competitionService.getAllCompetitions().subscribe(
      (competitionsData) => {
        this.competitions = competitionsData;
      },
      (error) => {
        console.error('Error fetching competitions', error);
      }
    );

  }
  updateMatch() {
    if (this.match) {
      this.isLoading = true;
      const updatedMatch: updatematchform = {
        id: this.match.id,
        matchDate: this.match.matchDate,
        homeTeamScore: this.match.homeTeamScore,
        awayTeamScore: this.match.awayTeamScore,
        status: this.match.status,
        stadium: this.match.stadium,
        homeTeamID: this.match.homeTeam.id,
        awayTeamID: this.match.awayTeam.id,
        competitionID: this.match.competition.id,
        winnerTeamID: this.match.winner ? this.match.winner.id : null
      };

      if (this.match) {
        this.isLoading = true;
        this.matchService.updateMatch(updatedMatch).subscribe(
          (response) => {
            console.log("update test", response);
            this.router.navigate(['/matches']);
          },
          (error) => {
            console.error('Error updating match', error);
            this.isLoading = false;
          }
        );
      }
    }

  }
}
