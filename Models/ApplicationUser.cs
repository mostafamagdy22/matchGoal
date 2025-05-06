using Microsoft.AspNetCore.Identity;

namespace MatchGoalAPI.Models
{
	public class ApplicationUser : IdentityUser
	{
		public List<PlayList> PlayLists { get; set; }
		public List<RefreshToken> RefreshTokens { get; set; }
	}
}