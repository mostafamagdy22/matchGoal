import { team } from "./team";
import { competition } from "./competition";
import { MatchStatus } from "./status.enum";
export interface addMatch
{
  id: null;
  HomeTeamID: number;
  AwayTeamID: number;
  CompetitionID: number;
  matchDate: Date;
  HomeTeamScore: number | null;
  AwayTeamScore: number | null;
  winner: string | null;
  Stadium: string;
  Status: MatchStatus;
}
