namespace MatchGoalAPI.Models
{
	public class Season
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int? CurrentMatchDay { get; set; }
		public string? Winner { get; set; }
		public int? CompetitionId { get; set; }
	}
}
