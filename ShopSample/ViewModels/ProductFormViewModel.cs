using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ShopSample.ViewModels
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "商品名")]
        public string Name { get; set; } = string.Empty;

        [Range(1, 9999999)]
        [Display(Name = "価格")]
        public int Price { get; set; }

        [Display(Name = "カテゴリ")]
        [Required(ErrorMessage = "カテゴリを選択してください。")]
        public int CategoryId { get; set; }

        // Dropdown list items
        public List<SelectListItem> CategoryList { get; set; } = new();
    }

}
