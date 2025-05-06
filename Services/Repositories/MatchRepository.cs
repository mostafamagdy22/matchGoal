
using System.Threading.Tasks;

namespace MatchGoalAPI.Services.Repositories
{
	public class MatchRepository : IMatchRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;
		public MatchRepository(ApplicationDbContext context,IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}
		/// <summary>
		/// Add a new match to the database
		/// </summary>
		/// <param name="obj">A <see cref="Match"/></param>
		/// <returns><see cref="bool"/> describe if match added successfully or not</returns>
		public async Task<bool> Add(Match obj)
		{
			await _context.Matches.AddAsync(obj);
			bool result = _context.SaveChangesAsync().IsCompletedSuccessfully;
			return result;
		}
		public async Task<List<Match>> GetAll(int pageSize, int page,string filter)
		{
				if (pageSize <= 0)
					pageSize = 10;
				if (page <= 0)
					page = 1;

			return await _context.Matches
				.Include(m => m.HomeTeam)
				.Include(m => m.AwayTeam)
				.Include(m => m.Competition)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}
		public async Task<Match> GetByID(int id)
		{
			return await _context.Matches.Include(m => m.Competition)
				.Include(m => m.HomeTeam)
				.Include(m => m.AwayTeam)
				.FirstOrDefaultAsync(m => m.ID == id);
		}
		public async Task<List<Match>> GetMatchesByFilter(MatchFilterDto matchFilterDto)
		{
			IQueryable<Match> query = _context.Matches
				.Include(m => m.HomeTeam)
				.Include(m => m.AwayTeam)
				.Include(m => m.Competition)
				.AsQueryable();

			if (!string.IsNullOrEmpty(matchFilterDto.HomeTeamName))
				query = query.Where(m => m.HomeTeam.Title.Contains(matchFilterDto.HomeTeamName));

			if (!string.IsNullOrEmpty(matchFilterDto.AwayTeamName))
				query = query.Where(m => m.AwayTeam.Title.Contains(matchFilterDto.AwayTeamName));

			if (!string.IsNullOrEmpty(matchFilterDto.CompetitionName))
				query = query.Where(m => m.Competition.Title.Contains(matchFilterDto.CompetitionName));

			if (!string.IsNullOrEmpty(matchFilterDto.Stadium))
				query = query.Where(m => m.Stadium.Contains(matchFilterDto.Stadium));

			if (!string.IsNullOrEmpty(matchFilterDto.Winner))
				query = query.Where(m => m.WhoWin.Contains(matchFilterDto.Winner));

			if (!string.IsNullOrEmpty(matchFilterDto.HomeTeamShortName))
				query = query.Where(m => m.HomeTeam.ShortName.Contains(matchFilterDto.HomeTeamShortName));

			if (!string.IsNullOrEmpty(matchFilterDto.AwayTeamShortName))
				query = query.Where(m => m.AwayTeam.ShortName.Contains(matchFilterDto.AwayTeamShortName));

			if (matchFilterDto.Status != null)
				query = query.Where(m => m.Status == matchFilterDto.Status);

			if (matchFilterDto.MatchDate != null)
				query = query.Where(m => m.MatchDate.Date == matchFilterDto.MatchDate.Value.Date);

			return await query.ToListAsync();
		}

		public Task<ICollection<Match>> GetMatchesFromCahche(string source)
		{
			throw new NotImplementedException();
		}
		public async Task<bool> Remove(int id)
		{
			Match match = await _context.Matches.FindAsync(id);
			if (match == null)
				return false;

			_context.Matches.Remove(match);
			await _context.SaveChangesAsync();
			throw new NotImplementedException();
		}

		public Task<bool> Update(Match obj)
		{
			throw new NotImplementedException();
		}
	}
}
