export interface updatematchform {
  id: number;
  matchDate: Date;
  homeTeamScore: number | null;
  awayTeamScore: number | null;
  status: number;
  stadium: string;
  homeTeamID: number;
  awayTeamID: number;
  competitionID: number;
  winnerTeamID: number | null;
}
