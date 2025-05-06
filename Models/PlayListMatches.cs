namespace MatchGoalAPI.Models
{
	public class PlayListMatches
	{
		[ForeignKey("PlayList")]
		public int PlayListID { get; set; }
		[ForeignKey("Match")]
		public int MatchID { get; set; }
		public PlayList PlayList { get; set; }
		public Match Match { get; set; }
	}
}
