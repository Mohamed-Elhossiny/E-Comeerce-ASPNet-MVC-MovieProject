using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieProject.Data;

namespace MovieProject.Controllers
{
    public class CinemasController : Controller
    {
        private readonly AppDbContext appcontext;

        public CinemasController(AppDbContext _appcontext)
        {
            appcontext = _appcontext;
        }
        public async Task<IActionResult> Index()
        {
            var allCinemas = await appcontext.Cinemas.ToListAsync();
            return View(allCinemas);
        }
    }
}
