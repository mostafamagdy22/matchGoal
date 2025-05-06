namespace MatchGoalAPI.Dto
{
	public class AuthDto
	{
		public string? Message { get; set; }
		public bool IsAuthenticated { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public List<string>? Roles { get; set; }
		public string? Token { get; set; }
		[System.Text.Json.Serialization.JsonIgnore]
		public string? RefreshToken { get; set; }
		public DateTime RefreshTokenExpiretion { get; set; }
	}
}
