using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieProject.Data;
using MovieProject.Models;
using MovieProject.Services.ActorsServies;
using MovieProject.ViewModel;
using System.Collections.Immutable;

namespace MovieProject.Controllers
{
	public class ActorsController : Controller
	{
		private readonly IActorsRepository actorRepository;
		private readonly IWebHostEnvironment webHostEnvironment;

		public ActorsController(IActorsRepository _actorRepository, IWebHostEnvironment webHostEnvironment)
		{
			actorRepository = _actorRepository;
			this.webHostEnvironment = webHostEnvironment;
		}

		public async Task<IActionResult> Index()
		{
			var data =await actorRepository.GetAllAsync();
			return View(data);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(ActorViewModel actorVM)
		{
			if (ModelState.IsValid == true)
			{
				string uploadpath = Path.Combine(webHostEnvironment.WebRootPath, "images", "Actors");
				string imageName = Guid.NewGuid().ToString() + "_" + actorVM.PictureURl.FileName;
				string filepath = Path.Combine(uploadpath, imageName);
				using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
				{
					actorVM.PictureURl.CopyTo(fileStream);
				}
				Actor actor = new Actor();
				actor.ProfilrPictureURl = imageName;
				actor.FullName = actorVM.FullName;
				actor.Bio = actorVM.Bio;

				await actorRepository.AddAsync(actor);
				await actorRepository.SaveAsync();
				return RedirectToAction("Index");
			}
			return View(actorVM);
		}

		public async Task<IActionResult> Details(int id)
		{
			var actorDetails = await actorRepository.GetByIdAsync(id);
			var actorVM = new ActorsDetailsViewModel();
			if (actorDetails == null) { return View("Not Found"); }
			else
			{
				actorVM.Bio = actorDetails.Bio;
				actorVM.PictureURl = actorDetails.ProfilrPictureURl;
				actorVM.FullName = actorDetails.FullName;
				actorVM.Id = actorDetails.Id;
			};
			return View(actorVM);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var actorDetails =await actorRepository.GetByIdAsync(id);
			var actorVM = new ActorsDetailsViewModel();
			if (actorDetails == null) { return View("Not Found"); }
			else
			{
				actorVM.Bio = actorDetails.Bio;
				actorVM.PictureURl = actorDetails.ProfilrPictureURl;
				actorVM.FullName = actorDetails.FullName;
				actorVM.Id = actorDetails.Id;
			};
			return View(actorVM);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(ActorViewModel actorVM)
		{

			if (ModelState.IsValid == true)
			{
				string uploadpath = Path.Combine(webHostEnvironment.WebRootPath, "images", "Actors");
				string imageName = Guid.NewGuid().ToString() + "_" + actorVM.PictureURl.FileName;
				string filepath = Path.Combine(uploadpath, imageName);
				using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
				{
					actorVM.PictureURl.CopyTo(fileStream);
				}
				Actor actor = new Actor();
				actor.Id = actorVM.Id;
				actor.FullName = actorVM.FullName;
				actor.ProfilrPictureURl = imageName;
				actor.Bio = actorVM.Bio;
				await actorRepository.updateAsync(actor);
				actorRepository.SaveAsync();
				return RedirectToAction("Index");
			}
			return View(actorVM);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var actorDetails =await actorRepository.GetByIdAsync(id);
			var actorVM = new ActorsDetailsViewModel();
			if (actorDetails == null) { return View("Not Found"); }
			else
			{
				actorVM.Bio = actorDetails.Bio;
				actorVM.PictureURl = actorDetails.ProfilrPictureURl;
				actorVM.FullName = actorDetails.FullName;
				actorVM.Id = actorDetails.Id;
			};
			return View(actorVM);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, ActorViewModel actorvm)
		{
			await actorRepository.DeleteAsync(id);
			await actorRepository.SaveAsync();
			return RedirectToAction("Index");

		}

	}

}

