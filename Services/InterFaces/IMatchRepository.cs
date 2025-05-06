namespace MatchGoalAPI.Services.InterFaces
{
	public interface IMatchRepository : IBaseRepository<Match>
	{
		public Task<List<Match>> GetMatchesByFilter(MatchFilterDto matchFilterDto);
		public Task<ICollection<Match>> GetMatchesFromCahche(string source);
	}
}
