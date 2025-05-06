
namespace MatchGoalAPI.Services.Repositories
{
	public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		protected readonly ApplicationDbContext _context;
		protected readonly DbSet<T> _dbSet;
		protected BaseRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<bool> Add(T obj)
		{
			await _dbSet.AddAsync(obj);
			return true;
		}

		public async Task<List<T>> GetAll(int pageSize,int page,string filter)
		{
			throw new NotImplementedException();
		}

		public Task<T> GetByID(int id)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Remove(int id)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Update(T obj)
		{
			throw new NotImplementedException();
		}
	}
}
