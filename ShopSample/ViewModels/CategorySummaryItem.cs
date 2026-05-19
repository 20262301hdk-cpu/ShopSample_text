namespace ShopSample.ViewModels
{
    public class CategorySummaryItem
    {
        public string CategoryName { get; set; } = string.Empty;
        public int ProductCount { get; set; }
        public int AveragePrice { get; set; }
        public int TotalPrice { get; set; }
    }
}
