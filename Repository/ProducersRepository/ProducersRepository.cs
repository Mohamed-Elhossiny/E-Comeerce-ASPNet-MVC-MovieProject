using MovieProject.Data;
using MovieProject.Data.Base;
using MovieProject.Models;

namespace MovieProject.Repository.ProducersRepository
{
	public class ProducersRepository : EntityBaseRepository<Producer>, IProducersRepository
	{
		public ProducersRepository(AppDbContext _context) : base(_context) { }
	}
}
