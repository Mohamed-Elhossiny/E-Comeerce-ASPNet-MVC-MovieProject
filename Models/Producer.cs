using MovieProject.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace MovieProject.Models
{
	public class Producer :IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Profile Picture")]
        public string? ProfilrPictureURl { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        public string Bio { get; set; }

        public virtual List<Movie>? Movies { get; set; }
    }
}
