namespace MatchGoalAPI.Services.CustomValidtion
{
	public class CheckThatTwoTeamsdiff : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			AddUpdateMatchDto dto = (AddUpdateMatchDto)validationContext.ObjectInstance;

			int HomeTeamID = dto.HomeTeamID;
			int AwayTeamID = dto.AwayTeamID;

			if (HomeTeamID == AwayTeamID)
			{
				return new ValidationResult("Home team and away team cannot be the same");
			}

			return ValidationResult.Success;
		}
	}
}
