using Microsoft.EntityFrameworkCore;
using MovieProject.Data;
using MovieProject.Data.Base;
using MovieProject.Models;

namespace MovieProject.Repository.MoviesRepository
{
	public class MoviesRepository : EntityBaseRepository<Movie>, IMoviesRepository
	{
		private readonly AppDbContext context;

		public MoviesRepository(AppDbContext _context) : base(_context)
		{
			context = _context;
		}

		public override async Task<IEnumerable<Movie>> GetAllAsync()
		{
			return await context.Movies.Include(m=>m.Cinema).ToListAsync();
		}
	}
}
