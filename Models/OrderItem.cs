using System.ComponentModel.DataAnnotations.Schema;

namespace MovieProject.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int? Amount { get; set; }
        public double? Price { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public virtual Movie? Movie { get; set; }


        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}
