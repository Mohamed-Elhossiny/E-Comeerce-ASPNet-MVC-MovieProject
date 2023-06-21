using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieProject.Data;

namespace MovieProject.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext appcontext;

        public MoviesController(AppDbContext _appcontext)
        {
            appcontext = _appcontext;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await appcontext.Movies.Include(n=>n.Cinema).ToListAsync();
            return View(allMovies);
        }
    }
}
