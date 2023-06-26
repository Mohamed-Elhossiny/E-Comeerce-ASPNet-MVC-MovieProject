using MovieProject.Data.Enums;
using MovieProject.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieProject.ViewModel.MovieViewModel
{
	public class MovieViewModel
	{
		[Required(ErrorMessage ="Name Is Required")]
		[Display(Name="Movie Name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Name Is Required")]
		[Display(Name = "Description")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Price Is Required")]
		[Display(Name = "Price in $")]
		public double? Price { get; set; }

		[Required(ErrorMessage = "Image Is Required")]
		[Display(Name = "Image URl")]
		public string ImageURL { get; set; }

		[Required(ErrorMessage = "Start Date Is Required")]
		[Display(Name = " Movie Start date")]
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "End Date Is Required")]
		[Display(Name = "Movie End date")]
		public DateTime EndData { get; set; }

		[Required(ErrorMessage = "Category Is Required")]
		[Display(Name = "Select Category")]
		public MovieCategory MovieCategory { get; set; }

		[Required(ErrorMessage = "Movies Actor(s) is Required")]
		[Display(Name = "Select Actor(s)")]
		public List<int> ActorsIds { get; set; }

		[Required(ErrorMessage = "Movies Cinema is Required")]
		[Display(Name = "Select Cinema")]
		public int CinemaId { get; set; }

		[Required(ErrorMessage = "Movies Producer is Required")]
		[Display(Name = "Select Producer")]
		public int ProducerId { get; set; }
	
	}
}
