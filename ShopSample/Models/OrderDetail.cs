using System.ComponentModel.DataAnnotations;

namespace ShopSample.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        // Foreign keys
        public int OrderId { get; set; }

        [Display(Name = "商品")]
        public int ProductId { get; set; }

        [Display(Name = "数量")]
        public int Quantity { get; set; }

        [Display(Name = "単価")]
        public int UnitPrice { get; set; }

        // Navigation properties
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }

}
