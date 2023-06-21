using Microsoft.AspNetCore.Mvc;
using MovieProject.Models;
using MovieProject.Repository.ProducersRepository;
using MovieProject.ViewModel;
using MovieProject.ViewModel.ProducerViewModel;

namespace MovieProject.Controllers
{
	public class ProducersController : Controller
    {
		private readonly IProducersRepository producersRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProducersController(IProducersRepository producersRepository, IWebHostEnvironment webHostEnvironment)
        {
			this.producersRepository = producersRepository;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var allProducers = await producersRepository.GetAllAsync();
            return View(allProducers);
        }

        public async Task<IActionResult> Details(int id)
        {
			var producerDetails = await producersRepository.GetByIdAsync(id);
			var producerVM = new ProducersDetailsViewModel();
			if (producerDetails == null) { return View("Not Found"); }
			else
			{
				producerVM.Bio = producerDetails.Bio;
				producerVM.PictureURl = producerDetails.ProfilrPictureURl;
				producerVM.FullName = producerDetails.FullName;
				producerVM.Id = producerDetails.Id;
			};
			return View(producerVM);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(ProducersImageViewModel producerVM)
		{
			if (ModelState.IsValid==true)
			{
				var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "images", "Producers");
				var pictureName = Guid.NewGuid().ToString() + "-" + producerVM.PictureURl.FileName;
				var filePath = Path.Combine(uploadPath, pictureName);
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					producerVM.PictureURl.CopyTo(stream);
				}
				Producer producer = new Producer();
				producer.ProfilrPictureURl = pictureName;
				producer.FullName = producerVM.FullName;
				producer.Bio = producerVM.Bio;

				await producersRepository.AddAsync(producer);
				await producersRepository.SaveAsync();
				return RedirectToAction("Index");
			}
			return View(producerVM);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var producerDetails = await producersRepository.GetByIdAsync(id);
			var producerVM = new ProducersDetailsViewModel();
			if (producerDetails == null) { return View("Not Found"); }
			else
			{
				producerVM.Bio = producerDetails.Bio;
				producerVM.PictureURl = producerDetails.ProfilrPictureURl;
				producerVM.FullName = producerDetails.FullName;
				producerVM.Id = producerDetails.Id;
			};
			return View(producerVM);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(ProducersImageViewModel producerVM)
		{

			if (ModelState.IsValid == true)
			{
				string uploadpath = Path.Combine(webHostEnvironment.WebRootPath, "images", "Producers");
				string imageName = Guid.NewGuid().ToString() + "_" + producerVM.PictureURl.FileName;
				string filepath = Path.Combine(uploadpath, imageName);
				using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
				{
					producerVM.PictureURl.CopyTo(fileStream);
				}
				Producer producer = new Producer();
				producer.Id = producerVM.Id;
				producer.FullName = producerVM.FullName;
				producer.ProfilrPictureURl = imageName;
				producer.Bio = producerVM.Bio;

				await producersRepository.updateAsync(producer);
				await producersRepository.SaveAsync();
				return RedirectToAction("Index");
			}
			return View(producerVM);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var producerDetails = await producersRepository.GetByIdAsync(id);
			var producerVM = new ProducersDetailsViewModel();
			if (producerDetails == null) { return View("Not Found"); }
			else
			{
				producerVM.Bio = producerDetails.Bio;
				producerVM.PictureURl = producerDetails.ProfilrPictureURl;
				producerVM.FullName = producerDetails.FullName;
				producerVM.Id = producerDetails.Id;
			};
			return View(producerVM);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, ProducersDetailsViewModel actorvm)
		{
			await producersRepository.DeleteAsync(id);
			await producersRepository.SaveAsync();
			return RedirectToAction("Index");

		}
	}
}
