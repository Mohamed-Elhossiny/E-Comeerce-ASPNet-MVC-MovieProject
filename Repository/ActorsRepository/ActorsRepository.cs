using Microsoft.EntityFrameworkCore;
using MovieProject.Data;
using MovieProject.Data.Base;
using MovieProject.Models;
using MovieProject.Services.ActorsServies;

namespace MovieProject.Repository.ActorsServies
{
	public class ActorsRepository :EntityBaseRepository<Actor>, IActorsRepository
	{
		public ActorsRepository(AppDbContext _context) : base(_context) { }
	
	}
}
