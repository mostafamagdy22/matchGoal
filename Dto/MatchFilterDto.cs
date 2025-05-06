
namespace MatchGoalAPI.Dto
{
	public class MatchFilterDto
	{
		public string? HomeTeamName { get; set; }
		public string? AwayTeamName { get; set; }
		public string? CompetitionName { get; set; }
		public string? HomeTeamShortName { get; set; }
		public string? AwayTeamShortName { get; set; }
		public DateTime? MatchDate { get; set; }
		public MatchStatusEnum? Status { get; set; }
		public string? Stadium { get; set; }
		public string? Winner { get; set; }
	}
}
