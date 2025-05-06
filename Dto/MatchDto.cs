namespace MatchGoalAPI.Dto
{
	public class MatchDto
	{
		public int ID { get; set; }
		public int? HomeTeamID { get; set; }
		public string? HomeTeamName { get; set; }
		public int? AwayTeamID { get; set; }
		public string? AwayTeamName { get; set; }
		public int? CompetitionID { get; set; }
		public string? CompetetionName { get; set; }
		public DateTime MatchDate { get; set; }
		public int? HomeTeamScore { get; set; }
		public int? AwayTeamScore { get; set; }
		public string? Winner { get; set; }
	}
}
