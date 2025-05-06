namespace MatchGoalAPI.Models
{
	public class Competition
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required]
		public string Title { get; set; }
		public string Code { get; set; }
		public string Type { get; set; }
		public int? CurrentSeasonId { get; set; }
		public Season? CurrentSeason { get; set; }
		public List<Season>? Seasons { get; set; } = new List<Season>();
		[Newtonsoft.Json.JsonIgnore]
		public virtual List<Match>? Matches { get; set; } = new List<Match>();
	}
}
