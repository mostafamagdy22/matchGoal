namespace MatchGoalAPI.Services.CustomValidtion
{
	public class CheckWinnerWithStatus : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			AddUpdateMatchDto dto = (AddUpdateMatchDto)validationContext.ObjectInstance;

			int? winnerTeamId = (int?)value;
			MatchStatusEnum status = dto.Status;
			int homeTeamId = dto.HomeTeamID;
			int awayTeamId = dto.AwayTeamID;

			if (status != MatchStatusEnum.Finished)
			{
				if (winnerTeamId.HasValue)
					return new ValidationResult("Winner can only be set when the match is finished.");

				return ValidationResult.Success;
			}

			if (status == MatchStatusEnum.Finished)
			{
				if (winnerTeamId.HasValue && winnerTeamId != homeTeamId && winnerTeamId != awayTeamId)
					return new ValidationResult("Winner must be either the home team or the away team.");
				return ValidationResult.Success;
			}

			return ValidationResult.Success;
		}
	}
}
