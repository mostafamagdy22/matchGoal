namespace MatchGoalAPI.Services.CustomValidtion
{
	public class CheckCompetetionIDFromDb : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			ApplicationDbContext context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
			
			bool CompetitionExisit = context.Competitions.Any(c => c.ID == (int)value);

			if (!CompetitionExisit)
				return new ValidationResult(ErrorMessage = "Competetion ID not Valid");

			return ValidationResult.Success;
		}
	}
}
