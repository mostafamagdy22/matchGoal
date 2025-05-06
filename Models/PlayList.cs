namespace MatchGoalAPI.Models
{
	public class PlayList
	{
		public int ID { get; set; }
		public string Title { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		[ForeignKey("User")]
		public string UserID { get; set; }
		public ApplicationUser User { get; set; }
	}
}
