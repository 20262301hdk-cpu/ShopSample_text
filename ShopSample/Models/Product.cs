using System.ComponentModel.DataAnnotations;

namespace ShopSample.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "商品名")]
        public string Name { get; set; } = string.Empty;

        [Range(1, 9999999)]
        [Display(Name = "価格")]
        public int Price { get; set; }

        // Foreign key
        [Display(Name = "カテゴリ")]
        public int CategoryId { get; set; }

        // Navigation property
        public Category? Category { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    }

}
