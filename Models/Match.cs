
namespace MatchGoalAPI.Models
{
	public class Match
	{
		public int ID { get; set; }
		public DateTime MatchDate { get; set; }
		public MatchStatusEnum Status { get; set; }
		public int? FirstTeamScore { get; set; }
		public int? SecondTeamScore { get; set; }
		[ForeignKey("HomeTeam")]
		public int? HomeTeamID { get; set; }
		[ForeignKey("AwayTeam")]
		public int? AwayTeamID { get; set; }
		[ForeignKey("Competition")]
		public int? CompetitionID { get; set; }
		public Team? HomeTeam { get; set; }
		public Team? AwayTeam { get; set; }
		[ForeignKey("Winner")]
		public int? WinnerID { get; set; }
		public Team? Winner { get; set; }
		public Competition? Competition { get; set; }
		public DateTime? LastScoreUpdate { get; set; }
		public string Stadium { get; set; }

		/// <summary>
		/// Gets the winner of the match based on the scores.
		/// </summary>
		[Newtonsoft.Json.JsonIgnore]
		public string? WhoWin => Status == MatchStatusEnum.Finished ? FirstTeamScore == SecondTeamScore ? "Draw" : FirstTeamScore > SecondTeamScore ? HomeTeam?.Title : AwayTeam?.Title : null;
	}
}
