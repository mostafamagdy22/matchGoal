namespace MatchGoalAPI.Services.InterFaces
{
	public interface IBaseRepository<T>
	{
		public Task<List<T>> GetAll(int pageSize,int page,string filter);
		public Task<T> GetByID(int id);
		public Task<bool> Remove(int id);
		public Task<bool> Update(T obj);
		public Task<bool> Add(T obj);
	}
}
