using System.ComponentModel.DataAnnotations;

namespace MovieProject.ViewModel.AccountViewModel
{
	public class RegisterViewModel
	{
        [Required(ErrorMessage = "Full Name is Required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Email Address is Required")]
        [Display(Name ="Email Address")]
        public string EmailAddress { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

        [DataType(DataType.Password),Compare("Password",ErrorMessage ="Passwords do not Match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
