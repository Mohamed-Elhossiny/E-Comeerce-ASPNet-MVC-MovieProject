using MovieProject.Models;

namespace MovieProject.Data.Base
{
	public interface IEntityBaseRepository<T> where T : class, IEntityBase,new()
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task AddAsync(T actor);
		Task updateAsync(T entity);
		Task DeleteAsync(int id);
		Task SaveAsync();
	}
}
