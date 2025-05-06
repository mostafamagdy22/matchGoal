using MatchGoalAPI.Services.CustomValidtion;

namespace MatchGoalAPI.Dto
{
	public class AddMatchDto
	{
		[Required(ErrorMessage = "Match date is required")]
		[DataType(DataType.Date)]
		public DateTime MatchDate { get; set; }

		[Required(ErrorMessage = "Home team ID is required")]
		[CheckTeamIDFromDb]
		public int HomeTeamID { get; set; }

		[Required(ErrorMessage = "Away team ID is required")]
		[CheckTeamIDFromDb]
		//[Compare("HomeTeamID", ErrorMessage = "Home team and away team cannot be the same")]
		public int AwayTeamID { get; set; }

		[Range(0, int.MaxValue, ErrorMessage = "Home team score cannot be negative")]
		public int? HomeTeamScore { get; set; }

		[Range(0, int.MaxValue, ErrorMessage = "Away team score cannot be negative")]
		public int? AwayTeamScore { get; set; }
		[Required(ErrorMessage = "Competition ID is required")]
		[CheckCompetetionIDFromDb]
		public int CompetitionID { get; set; }
		[Required(ErrorMessage = "Status of match is required")]
		public MatchStatusEnum Status { get; set; }
		[CheckWinnerWithStatus]
		public int? WinnerTeamID { get; set; }
		public string Stadium { get; set; }
	}
}