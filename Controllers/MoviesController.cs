using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieProject.Data;
using MovieProject.Models;
using MovieProject.Repository.CinemasRepository;
using MovieProject.Repository.MoviesRepository;
using MovieProject.Repository.ProducersRepository;
using MovieProject.Services.ActorsServies;
using MovieProject.ViewModel.MovieViewModel;
using MovieProject.ViewModel.ProducerViewModel;

namespace MovieProject.Controllers
{
	public class MoviesController : Controller
	{
		private readonly IMoviesRepository moviesRepository;
		private readonly ICinemasRepository cinemas;
		private readonly IProducersRepository producers;
		private readonly IActorsRepository actors;
		private readonly IWebHostEnvironment webHostEnvironment;

		public MoviesController(
			IMoviesRepository moviesRepository,
			ICinemasRepository cinemas,
			IProducersRepository producers,
			IActorsRepository actors,
			IWebHostEnvironment webHostEnvironment)
		{
			this.moviesRepository = moviesRepository;
			this.cinemas = cinemas;
			this.producers = producers;
			this.actors = actors;
			this.webHostEnvironment = webHostEnvironment;
		}

        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await moviesRepository.GetAllAsync();
			if (!string.IsNullOrEmpty(searchString))
			{
				var filterResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString) || n.Description.ToLower().Contains(searchString)).ToList();
				return View("Index", filterResult);
			}
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
		{
			var allMovies = await moviesRepository.GetAllAsync();
			return View(allMovies);
		}

		public async Task<IActionResult> Details(int id)
		{
			var movieDetails = await moviesRepository.GetMovieByIdAsync(id);
			return View(movieDetails);
		}

		public async Task<IActionResult> Create()
		{
			ViewData["cinmeaList"] = await cinemas.GetAllAsync();
			ViewData["producersList"] = await producers.GetAllAsync();
			ViewData["actorsList"] = await actors.GetAllAsync();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(MovieImageViewModel movieVM)
		{
			if (ModelState.IsValid == true)
			{
				var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "images", "Movies");
				var pictureName = Guid.NewGuid().ToString() + "-" + movieVM.ImageURL.FileName;
				var filePath = Path.Combine(uploadPath, pictureName);
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					movieVM.ImageURL.CopyTo(stream);
				}
				Movie movie = new Movie();
				movie.Name = movieVM.Name;
				movie.ImageURL = pictureName;
				movie.StartDate = movieVM.StartDate;
				movie.EndData = movieVM.EndData;
				movie.ProducerId = movieVM.ProducerId;
				movie.Description = movieVM.Description;
				movie.CinemaId = movieVM.CinemaId;
				movie.MovieCategory = movieVM.MovieCategory;
				movie.Price = movieVM.Price;

				await moviesRepository.AddAsync(movie);
				await moviesRepository.SaveAsync();

				foreach (var actorId in movieVM.ActorsIds)
				{
					var newActorMovie = new Actor_Movie()
					{
						MovieId = movie.Id,
						ActorId = actorId
					};
					await moviesRepository.AddActorMvie(newActorMovie);
				}

				await moviesRepository.SaveAsync();

				return RedirectToAction("Index");
			}
			return View(movieVM);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var movieDetails = await moviesRepository.GetMovieByIdAsync(id);
			var movieVM = new MovieViewModel();
			if (movieDetails == null) { return View("Not Found"); }
			else
			{
				movieVM.Name = movieDetails.Name;
				movieVM.ImageURL = movieDetails.ImageURL;
				movieVM.StartDate = movieDetails.StartDate;
				movieVM.EndData = movieDetails.EndData;
				movieVM.ProducerId = movieDetails.ProducerId;
				movieVM.Description = movieDetails.Description;
				movieVM.CinemaId = movieDetails.CinemaId;
				movieVM.MovieCategory = movieDetails.MovieCategory;
				movieVM.Price = movieDetails.Price;
				movieVM.ActorsIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList();

			};
			ViewData["cinmeaList"] = await cinemas.GetAllAsync();
			ViewData["producersList"] = await producers.GetAllAsync();
			ViewData["actorsList"] = await actors.GetAllAsync();
			return View(movieVM);
		}

		[HttpPost]
        public async Task<IActionResult> Edit(MovieImageViewModel movieVM)
        {
            if (ModelState.IsValid == true)
            {
				await moviesRepository.UpdateMovieAsync(movieVM);
                return RedirectToAction("Index");
            }
            ViewData["cinmeaList"] = await cinemas.GetAllAsync();
            ViewData["producersList"] = await producers.GetAllAsync();
            ViewData["actorsList"] = await actors.GetAllAsync();
            return View(movieVM);
        }



    }
}
