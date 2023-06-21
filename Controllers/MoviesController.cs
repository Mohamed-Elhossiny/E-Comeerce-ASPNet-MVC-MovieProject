using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieProject.Data;
using MovieProject.Repository.MoviesRepository;

namespace MovieProject.Controllers
{
    public class MoviesController : Controller
    {
		private readonly IMoviesRepository moviesRepository;

		public MoviesController(IMoviesRepository moviesRepository)
        {
			this.moviesRepository = moviesRepository;
		}
        public async Task<IActionResult> Index()
        {
            var allMovies = await moviesRepository.GetAllAsync();
            return View(allMovies);
        }
    }
}
