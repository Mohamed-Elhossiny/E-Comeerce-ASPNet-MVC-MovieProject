using System.ComponentModel.DataAnnotations;

namespace MovieProject.ViewModel.AccountViewModel
{
	public class LoginViewModel
	{
        [Required(ErrorMessage ="Email Address is Required")]
        [Display(Name ="Email Address")]
        public string EmailAddress { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }
    }
}
