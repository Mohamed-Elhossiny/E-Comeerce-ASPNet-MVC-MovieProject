using Microsoft.EntityFrameworkCore;
using MovieProject.Data;
using MovieProject.Repository.ActorsServies;
using MovieProject.Repository.CinemasRepository;
using MovieProject.Repository.MoviesRepository;
using MovieProject.Repository.ProducersRepository;
using MovieProject.Services.ActorsServies;

namespace MovieProject
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<AppDbContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));
			
			builder.Services.AddScoped<IActorsRepository,ActorsRepository>();
			builder.Services.AddScoped<IProducersRepository, ProducersRepository>();
			builder.Services.AddScoped<ICinemasRepository, CinemasRepository>();
			builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			AppDbInitilizer.Seed(app);

			app.Run();
		}
	}
}