using Microsoft.AspNetCore.Identity;
using MovieProject.Data.Static;
using MovieProject.Models;

namespace MovieProject.Data
{
	public class AppDbInitilizer
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public AppDbInitilizer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
		}
		public static void Seed(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
				context.Database.EnsureCreated();

				if (!context.Cinemas.Any())
				{
					context.Cinemas.AddRange(new List<Cinema>()
					{
						new Cinema() {
						Name = "Cinema 1",
						Logo = "~/images/Cinemas/cinema-1.jpeg",
						Description = "This is the description of the first cinema"
						},
						new Cinema() {
						Name = "Cinema 2",
						Logo = "~/images/Cinemas/cinema-2.jpeg",
						Description = "This is the description of the Second cinema"
						},
						new Cinema() {
						Name = "Cinema 3",
						Logo = "~/images/Cinemas/cinema-3.jpeg",
						Description = "This is the description of the Third cinema"
						},
						new Cinema() {
						Name = "Cinema 4",
						Logo = "~/images/Cinemas/cinema-4.jpeg",
						Description = "This is the description of the Forth cinema"
						},
						new Cinema() {
						Name = "Cinema 5",
						Logo = "~/images/Cinemas/cinema-5.jpeg",
						Description = "This is the description of the Fifth cinema"
						},
					});
					context.SaveChanges();
				}
				if (!context.Actors.Any())
				{
					context.Actors.AddRange(new List<Actor>()
					{
						new Actor()
						{
							FullName="Actor 1",
							Bio="This is the Bio Of the First Actor",
							ProfilrPictureURl="https://dotnethow.net/images/actors/actor-1.jpeg"
						},
						new Actor()
						{
							FullName="Actor 2",
							Bio="This is the Bio Of the Second Actor",
							ProfilrPictureURl="https://dotnethow.net/images/actors/actor-2.jpeg"
						},
						new Actor()
						{
							FullName="Actor 3",
							Bio="This is the Bio Of the Third Actor",
							ProfilrPictureURl="https://dotnethow.net/images/actors/actor-3.jpeg"
						},
						new Actor()
						{
							FullName="Actor 4",
							Bio="This is the Bio Of the Forth Actor",
							ProfilrPictureURl="https://dotnethow.net/images/actors/actor-4.jpeg"
						},
						new Actor()
						{
							FullName="Actor 5",
							Bio="This is the Bio Of the Fifth Actor",
							ProfilrPictureURl="https://dotnethow.net/images/actors/actor-5.jpeg"
						}
					});
					context.SaveChanges();
				}
				if (!context.Producers.Any())
				{
					context.Producers.AddRange(new List<Producer>()
					{
						new Producer()
						{
							FullName="Producer 1",
							Bio = "This is the Bio Of the First Producer",
							ProfilrPictureURl="https://dotnethow.net/images/producers/producer-1.jpeg"
						},
						new Producer()
						{
							FullName="Producer 2",
							Bio = "This is the Bio Of the Second Producer",
							ProfilrPictureURl="https://dotnethow.net/images/producers/producer-2.jpeg"
						},
						new Producer()
						{
							FullName="Producer 3",
							Bio = "This is the Bio Of the Third Producer",
							ProfilrPictureURl="https://dotnethow.net/images/producers/producer-3.jpeg"
						},
						new Producer()
						{
							FullName="Producer 4",
							Bio = "This is the Bio Of the Forth Producer",
							ProfilrPictureURl="https://dotnethow.net/images/producers/producer-4.jpeg"
						},
						new Producer()
						{
							FullName="Producer 5",
							Bio = "This is the Bio Of the Fifth Producer",
							ProfilrPictureURl="https://dotnethow.net/images/producers/producer-5.jpeg"
						}
					});
					context.SaveChanges();
				}
				if (!context.Movies.Any())
				{
					context.Movies.AddRange(new List<Movie>()
					{
						new Movie()
						{
							Name="Scoob",
							Description = "This is Description for Scoob Movie",
							Price = 39.40,
							ImageURL = "https://dotnethow.net/images/movies/movie-1.jpeg",
							StartDate = DateTime.Now.AddDays(-10),
							EndData = DateTime.Now.AddDays(-2),
							CinemaId = 1,
							ProducerId = 3,
							MovieCategory = Enums.MovieCategory.Cartoon
						},
						new Movie()
						{
							Name="Ghost",
							Description = "This is Description for Ghost Movie",
							Price = 39.50,
							ImageURL = "https://dotnethow.net/images/movies/movie-4.jpeg",
							StartDate = DateTime.Now,
							EndData = DateTime.Now.AddDays(7),
							CinemaId = 4,
							ProducerId = 3,
							MovieCategory = Enums.MovieCategory.Horror
						},
						new Movie()
						{
							Name="Race",
							Description = "This is Description for Race Movie",
							Price = 39.50,
							ImageURL = "https://dotnethow.net/images/movies/movie-6.jpeg",
							StartDate = DateTime.Now.AddDays(-10),
							EndData = DateTime.Now.AddDays(-5),
							CinemaId = 1,
							ProducerId = 2,
							MovieCategory = Enums.MovieCategory.Documentry
						},
						new Movie()
						{
							Name="Cold Soles",
							Description = "This is Description for Cold Soles Movie",
							Price = 39.50,
							ImageURL = "https://dotnethow.net/images/movies/movie-8.jpeg",
							StartDate = DateTime.Now.AddDays(3),
							EndData = DateTime.Now.AddDays(20),
							CinemaId = 1,
							ProducerId = 5,
							MovieCategory = Enums.MovieCategory.Drama
						},
						new Movie()
						{
							Name="The Lite",
							Description = "This is Description for The Lite Movie",
							Price = 40.50,
							ImageURL = "https://dotnethow.net/images/movies/movie-3.jpeg",
							StartDate = DateTime.Now.AddDays(-10),
							EndData = DateTime.Now.AddDays(10),
							CinemaId = 3,
							ProducerId = 3,
							MovieCategory = Enums.MovieCategory.Documentry
						},
						 new Movie()
						{
							Name="Race",
							Description = "This is Description for Race Movie",
							Price = 39.50,
							ImageURL = "https://dotnethow.net/images/movies/movie-6.jpeg",
							StartDate = DateTime.Now.AddDays(-10),
							EndData = DateTime.Now.AddDays(-5),
							CinemaId = 1,
							ProducerId = 2,
							MovieCategory = Enums.MovieCategory.Documentry
						},
						  new Movie()
						{
							Name="Cold Soles",
							Description = "This is Description for Cold Soles Movie",
							Price = 39.50,
							ImageURL = "https://dotnethow.net/images/movies/movie-8.jpeg",
							StartDate = DateTime.Now.AddDays(3),
							EndData = DateTime.Now.AddDays(20),
							CinemaId = 1,
							ProducerId = 5,
							MovieCategory = Enums.MovieCategory.Drama
						},
						  new Movie()
						{
							Name="Scoob",
							Description = "This is Description for Scoob Movie",
							Price = 39.40,
							ImageURL = "https://dotnethow.net/images/movies/movie-1.jpeg",
							StartDate = DateTime.Now.AddDays(-10),
							EndData = DateTime.Now.AddDays(-2),
							CinemaId = 1,
							ProducerId = 3,
							MovieCategory = Enums.MovieCategory.Cartoon
						},
						  new Movie()
						{
							Name="Ghost",
							Description = "This is Description for Ghost Movie",
							Price = 39.50,
							ImageURL = "https://dotnethow.net/images/movies/movie-4.jpeg",
							StartDate = DateTime.Now,
							EndData = DateTime.Now.AddDays(7),
							CinemaId = 4,
							ProducerId = 3,
							MovieCategory = Enums.MovieCategory.Horror
						},
					});

					context.SaveChanges();

				}
				if (!context.Actors_Movies.Any())
				{
					context.Actors_Movies.AddRange(new List<Actor_Movie>()
					{
						new Actor_Movie()
						{
							ActorId= 1,
							MovieId=2
						},
						new Actor_Movie()
						{
							ActorId= 4,
							MovieId=2
						},
						new Actor_Movie()
						{
							ActorId= 2,
							MovieId=5
						},
						new Actor_Movie()
						{
							ActorId= 3,
							MovieId=2
						},
						new Actor_Movie()
						{
							ActorId= 3,
							MovieId=5
						},
						new Actor_Movie()
						{
							ActorId= 4,
							MovieId=5
						},
						new Actor_Movie()
						{
							ActorId= 5,
							MovieId=4
						},
						new Actor_Movie()
						{
							ActorId= 2,
							MovieId=4
						},
					});
					context.SaveChanges();
				}
			}
		}

		public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
				{
					await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
				}
				if (!await roleManager.RoleExistsAsync(UserRoles.User))
				{
					await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
				}

				var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

				string adminUserEmail = "admin@movie.com";

				var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
				if (adminUser == null)
				{
					var newAdminUser = new ApplicationUser()
					{
						FullName = "Admin User",
						UserName = "admin-user",
						Email = adminUserEmail,
						EmailConfirmed = true
					};
					await userManager.CreateAsync(newAdminUser, "Admin@123");

					await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
				}

				string appUserEmail = "mohamed@gmail.com";
				var appUser = await userManager.FindByEmailAsync(appUserEmail);
				if (appUser == null)
				{
					var newAppUser = new ApplicationUser()
					{
						FullName = "Application User",
						UserName = "app-user",
						Email = appUserEmail,
						EmailConfirmed = true
					};
					await userManager.CreateAsync(newAppUser, "Mohamed@123");

					await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
				}
			}
		}
	}
}
