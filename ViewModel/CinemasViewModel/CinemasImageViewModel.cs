namespace MovieProject.ViewModel.CinemasViewModel
{
    public class CinemasImageViewModel
    {
        public int Id { get; set; }
        public string CinemaName { get; set; }
        public string Description { get; set; }
        public IFormFile Logo { get; set; }
    }
}

