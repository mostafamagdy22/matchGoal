namespace MatchGoalAPI.Dto
{
	public class AddToRoleDto
	{
		[Required]
		public string UserID { get; set; }
		[Required]
		public string Role { get; set; }
	}
}
