using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieProject.Data;
using MovieProject.Data.Static;
using MovieProject.Models;
using MovieProject.ViewModel.AccountViewModel;

namespace MovieProject.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly AppDbContext context;

		public AccountController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			AppDbContext context)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.context = context;
		}

		[Authorize(Roles ="Admin")]
		public async Task<IActionResult> Users()
		{
			var users = await context.Users.ToListAsync();
			return View(users);
		}

		public IActionResult Login() => View(new LoginViewModel());

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVM)
		{
			if (!ModelState.IsValid)
			{
				return View(loginVM);
			}
			else
			{
				var user = await userManager.FindByEmailAsync(loginVM.EmailAddress);
				if (user != null)
				{
					var passwordCheck = await userManager.CheckPasswordAsync(user, loginVM.Password);
					if (passwordCheck == true)
					{
						var result = await signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
						if (result.Succeeded)
						{
							return RedirectToAction("Index", "Movies");
						}
					}
				}
				TempData["Error"] = "Please, Try Again";
				return View(loginVM);
			}
		}

		public IActionResult Register() => View(new RegisterViewModel());

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel registerVM)
		{
			if (!ModelState.IsValid)
			{
				return View(registerVM);
			}
			var user = await userManager.FindByEmailAsync(registerVM.EmailAddress);
			if (user != null)
			{
				TempData["Error"] = "This Email Address is Already in use";
				return View(registerVM);
			}
			var newUser = new ApplicationUser()
			{
				FullName = registerVM.FullName,
				Email = registerVM.EmailAddress,
				UserName = registerVM.FullName,
			};
			var newUserResponse = await userManager.CreateAsync(newUser, registerVM.Password);
			if (newUserResponse.Succeeded)
			{
				await userManager.AddToRoleAsync(newUser, UserRoles.User);
				return View("RegisterCompleted");
			}
			TempData["Error"] = "This Email Address is Already in use";
			return View(registerVM);
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Movies");
		}

	}
}
