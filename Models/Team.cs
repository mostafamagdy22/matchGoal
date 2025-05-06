namespace MatchGoalAPI.Models
{
	public class Team
	{
		public int ID { get; set; }
		public string Title { get; set; }
		public string? Country { get; set; }
		public int Founded { get; set; }
		public string ClubColors { get; set; }
		public string ShortName { get; set; }
		[Newtonsoft.Json.JsonIgnore]
		public List<Match> HomeMatches { get; set; }
		[Newtonsoft.Json.JsonIgnore]
		public List<Match> AwayMatches { get; set; }
	}
}
