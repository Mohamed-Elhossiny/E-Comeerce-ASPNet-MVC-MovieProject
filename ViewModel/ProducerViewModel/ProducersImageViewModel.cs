namespace MovieProject.ViewModel.ProducerViewModel
{
    public class ProducersImageViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public IFormFile PictureURl { get; set; }
        public string Bio { get; set; }
    }
}
