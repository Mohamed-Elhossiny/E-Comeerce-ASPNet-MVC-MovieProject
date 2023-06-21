using Microsoft.EntityFrameworkCore;
using MovieProject.Models;

namespace MovieProject.Data.Base
{
	public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
	{
		private readonly AppDbContext context;

		public EntityBaseRepository(AppDbContext _context)
		{
			context = _context;
		}

		public async Task AddAsync(T entity) => await context.Set<T>().AddAsync(entity);

		public async Task<IEnumerable<T>> GetAllAsync() => await context.Set<T>().ToListAsync();

		public async Task<T> GetByIdAsync(int id) => await context.Set<T>().FirstOrDefaultAsync(a => a.Id == id);

		public async Task updateAsync(T entity) => context.Entry<T>(entity).State = EntityState.Modified;
		public async Task DeleteAsync(int id)
		{
			T? entity = await context.Set<T>().FirstOrDefaultAsync(i => i.Id == id);
			context.Set<T>().Remove(entity);
		}

		public async Task SaveAsync() => await context.SaveChangesAsync();








	}
}
