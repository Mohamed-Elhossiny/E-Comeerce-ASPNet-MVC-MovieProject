using Microsoft.EntityFrameworkCore;
using MovieProject.Data;
using MovieProject.Data.Base;
using MovieProject.Models;

namespace MovieProject.Repository.CinemasRepository
{
	public class CinemasRepository : EntityBaseRepository<Cinema>, ICinemasRepository
	{
		private readonly AppDbContext context;

		public CinemasRepository(AppDbContext _context) : base(_context)
		{
			context = _context;
		}

	}
}
