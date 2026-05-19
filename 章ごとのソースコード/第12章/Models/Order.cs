using System.ComponentModel.DataAnnotations;

namespace ShopSample.Models
{
    public class Order
    {
        public int Id { get; set; }

        // Foreign key
        [Display(Name = "顧客")]
        public int CustomerId { get; set; }

        [Display(Name = "注文日")]
        public DateTime OrderDate { get; set; }

        // Navigation properties
        public Customer? Customer { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }

}
