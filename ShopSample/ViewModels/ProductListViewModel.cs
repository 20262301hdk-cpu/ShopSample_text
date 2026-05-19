using ShopSample.Models;

namespace ShopSample.ViewModels
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; } = new();
        public string PageTitle { get; set; } = "商品一覧";
        public string? QueryDescription { get; set; }

        // Count / Any 用
        public int? TotalCount { get; set; }
        public int? FilteredCount { get; set; }
        public bool? HasExpensiveProducts { get; set; }

        // Sum 用
        public int? TotalPriceSum { get; set; }

        // GroupBy 用
        public List<CategorySummaryItem>? CategorySummaries { get; set; }
    }

}
