import { team } from "./team";
import { competition } from "./competition";
import { MatchStatus } from './status.enum';

export interface Match
{
  id: number;
  homeTeam: team;
  awayTeam: team;
  competition: competition;
  matchDate: Date;
  homeTeamScore: number | null;
  awayTeamScore: number | null;
  winner: team | null;
  stadium: string;
  status: MatchStatus
}
