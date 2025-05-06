namespace MatchGoalAPI.Dto
{
	public class LoginUserDto
	{
		[Required(ErrorMessage = "Email is Required!")]
		[EmailAddress(ErrorMessage = "Invalid email address.")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required.")]
		public string Password { get; set; }
	}
}
