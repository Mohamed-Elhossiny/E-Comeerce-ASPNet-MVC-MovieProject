
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Email { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }

        public virtual List<OrderItem>? OrderItems { get; set; }
    }
}
