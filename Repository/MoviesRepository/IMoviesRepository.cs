using MovieProject.Data.Base;
using MovieProject.Models;
using MovieProject.ViewModel.MovieViewModel;

namespace MovieProject.Repository.MoviesRepository
{
	public interface IMoviesRepository : IEntityBaseRepository<Movie>
	{
		Task<Movie> GetMovieByIdAsync(int id);
		Task AddActorMvie(Actor_Movie actor_Movie);

		Task UpdateMovieAsync(MovieImageViewModel movieVm);
	}
}
