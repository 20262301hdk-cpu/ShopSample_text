using ShopSample.Models;

namespace ShopSample.ViewModels
{
    public class OrderHistoryViewModel
    {
        public string PageTitle { get; set; } = "購入履歴";
        public string? QueryDescription { get; set; }
        public List<Order> Orders { get; set; } = new();
        public Dictionary<int, int>? OrderTotals { get; set; }  // orderId → 合計金額 
        public int? GrandTotal { get; set; }
        public string? CustomerName { get; set; }
    }
}
