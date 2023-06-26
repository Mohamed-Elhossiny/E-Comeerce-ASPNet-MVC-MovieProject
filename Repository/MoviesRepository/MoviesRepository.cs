using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MovieProject.Data;
using MovieProject.Data.Base;
using MovieProject.Models;
using MovieProject.ViewModel.MovieViewModel;

namespace MovieProject.Repository.MoviesRepository
{
	public class MoviesRepository : EntityBaseRepository<Movie>, IMoviesRepository
	{
		private readonly AppDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MoviesRepository(AppDbContext _context,IWebHostEnvironment webHostEnvironment) : base(_context)
		{
			context = _context;
            this.webHostEnvironment = webHostEnvironment;
        }

		public async Task AddActorMvie(Actor_Movie actor_Movie)
		{
			await context.Actors_Movies.AddAsync(actor_Movie);
		}

		public override async Task<IEnumerable<Movie>> GetAllAsync()
		{
			return await context.Movies.Include(m=>m.Cinema).ToListAsync();
		}

		public async Task<Movie> GetMovieByIdAsync(int id)
		{
			var movieDetails = await context.Movies
				.Include(c=>c.Cinema).Include(c=>c.Producer)
				.Include(c=>c.Actors_Movies).ThenInclude(a=>a.Actor)
				.FirstOrDefaultAsync(m=>m.Id==id);
			return  movieDetails;
		}

        public async Task UpdateMovieAsync(MovieImageViewModel movieVM)
        {
			var movieDb =await context.Movies.FirstOrDefaultAsync(n => n.Id == movieVM.Id);
            if (movieDb != null)
			{
                var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "images", "Movies");
                var pictureName = Guid.NewGuid().ToString() + "-" + movieVM.ImageURL.FileName;
                var filePath = Path.Combine(uploadPath, pictureName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    movieVM.ImageURL.CopyTo(stream);
                }
                movieDb.Name = movieVM.Name;
                movieDb.ImageURL = pictureName;
                movieDb.StartDate = movieVM.StartDate;
                movieDb.EndData = movieVM.EndData;
                movieDb.ProducerId = movieVM.ProducerId;
                movieDb.Description = movieVM.Description;
                movieDb.CinemaId = movieVM.CinemaId;
                movieDb.Price = movieVM.Price;
                movieDb.MovieCategory = movieVM.MovieCategory;
                await context.SaveChangesAsync();

                var actualActor = context.Actors_Movies.Where(n => n.MovieId == movieVM.Id).ToList();
                context.Actors_Movies.RemoveRange(actualActor);
                await context.SaveChangesAsync();

                foreach (var actorId in movieVM.ActorsIds)
                {
                    var newActorMovie = new Actor_Movie()
                    {
                        MovieId = movieVM.Id,
                        ActorId = actorId
                    };
                    await context.Actors_Movies.AddAsync(newActorMovie);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
