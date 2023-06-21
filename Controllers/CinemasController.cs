using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieProject.Data;
using MovieProject.Models;
using MovieProject.Repository.CinemasRepository;
using MovieProject.Repository.ProducersRepository;
using MovieProject.ViewModel.CinemasViewModel;
using MovieProject.ViewModel.ProducerViewModel;

namespace MovieProject.Controllers
{
    public class CinemasController : Controller
    {
		private readonly ICinemasRepository cinemasRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CinemasController(ICinemasRepository cinemasRepository,IWebHostEnvironment webHostEnvironment)
        {
			this.cinemasRepository = cinemasRepository;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var allCinemas = await cinemasRepository.GetAllAsync();
			return View(allCinemas);
		}

		public async Task<IActionResult> Details(int id)
		{
			var cinemaDetails = await cinemasRepository.GetByIdAsync(id);
			var cinemaVM = new CinemasDetailViewModel();
			if (cinemaDetails == null) { return View("Not Found"); }
			else
			{
				cinemaVM.CinemaName = cinemaDetails.Name;
				cinemaVM.Logo = cinemaDetails.Logo;
				cinemaVM.Description = cinemaDetails.Description;
				cinemaVM.Id = cinemaDetails.Id;
			};
			return View(cinemaVM);
		}

        public IActionResult Create()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Create(CinemasImageViewModel cinemaVM)
		{
			if (ModelState.IsValid==true)
			{
				var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "images", "Cinemas");
				var logoName = Guid.NewGuid().ToString() + "_" + cinemaVM.Logo.FileName;
				var filePath = Path.Combine(uploadPath, logoName);
				using(var stream = new FileStream(filePath, FileMode.Create))
				{
					cinemaVM.Logo.CopyTo(stream);
				}
                Cinema cinema = new Cinema();
                cinema.Logo = logoName;
                cinema.Name = cinemaVM.CinemaName;
                cinema.Description = cinemaVM.Description;

                await cinemasRepository.AddAsync(cinema);
                await cinemasRepository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(cinemaVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await cinemasRepository.GetByIdAsync(id);
            var cinemaVM = new CinemasDetailViewModel();
            if (cinemaDetails == null) { return View("Not Found"); }
            else
            {
                cinemaVM.Description = cinemaDetails.Description;
                cinemaVM.Logo = cinemaDetails.Logo;
                cinemaVM.CinemaName = cinemaDetails.Name;
                cinemaVM.Id = cinemaDetails.Id;
            };
            return View(cinemaVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CinemasImageViewModel cinemaVM)
        {

            if (ModelState.IsValid == true)
            {
                string uploadpath = Path.Combine(webHostEnvironment.WebRootPath, "images", "Cinemas");
                string imageName = Guid.NewGuid().ToString() + "_" + cinemaVM.Logo.FileName;
                string filepath = Path.Combine(uploadpath, imageName);
                using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
                {
                    cinemaVM.Logo.CopyTo(fileStream);
                }
                Cinema cinema = new Cinema();
                cinema.Id = cinemaVM.Id;
                cinema.Name = cinemaVM.CinemaName;
                cinema.Logo = imageName;
                cinema.Description = cinemaVM.Description;

                await cinemasRepository.updateAsync(cinema);
                await cinemasRepository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(cinemaVM);
        }

		public async Task<IActionResult> Delete(int id)
		{
			var cinemaDetails = await cinemasRepository.GetByIdAsync(id);
			var cinemaVM = new CinemasDetailViewModel();
			if (cinemaDetails == null) { return View("Not Found"); }
			else
			{
				cinemaVM.Description = cinemaDetails.Description;
				cinemaVM.Logo = cinemaDetails.Logo;
				cinemaVM.CinemaName = cinemaDetails.Name;
				cinemaVM.Id = cinemaDetails.Id;
			};
			return View(cinemaVM);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, CinemasDetailViewModel cinemavm)
		{
			await cinemasRepository.DeleteAsync(id);
			await cinemasRepository.SaveAsync();
			return RedirectToAction("Index");

		}

	}
}
