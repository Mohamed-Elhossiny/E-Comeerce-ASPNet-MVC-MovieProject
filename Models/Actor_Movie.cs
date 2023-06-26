using System.ComponentModel.DataAnnotations.Schema;

namespace MovieProject.Models
{
    public class Actor_Movie
    {
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public virtual Movie? Movie { get; set; }

        
        [ForeignKey("Actor")]
        public int ActorId { get; set; }
        public virtual Actor? Actor { get; set; }
    }
}
