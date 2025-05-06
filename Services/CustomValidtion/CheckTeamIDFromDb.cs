namespace MatchGoalAPI.Services.CustomValidtion
{
	sealed public class CheckTeamIDFromDb : ValidationAttribute
	{
		protected override ValidationResult IsValid(object? value,ValidationContext validationContext)
		{
			ApplicationDbContext context = validationContext.GetRequiredService<ApplicationDbContext>();
			if (value == null || value is not int teamId)
			{
				return new ValidationResult($"The {validationContext.DisplayName} must be a valid integer.");
			}

			bool teamExists = context.Teams.Any(t => t.ID == (int)value);
			if (!teamExists)
			{
				return new ValidationResult("${The team ID {teamId} does not exist in the database.}");
			}
			return ValidationResult.Success;
		}
	}
}
