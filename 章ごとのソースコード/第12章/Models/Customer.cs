using System.ComponentModel.DataAnnotations;

namespace ShopSample.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "氏名")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Display(Name = "メールアドレス")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "登録日")]
        public DateTime RegisteredDate { get; set; }

        // Navigation property: 1 Customer has many Orders
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }

}
