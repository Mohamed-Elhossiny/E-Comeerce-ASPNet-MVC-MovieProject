using System.ComponentModel.DataAnnotations.Schema;

namespace MovieProject.Models
{
    public class Actor_Movie
    {
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        
        [ForeignKey("Actor")]
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
