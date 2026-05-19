namespace ShopSample.ViewModels
{
    public class UserInfometionViewModel
    {
        public string? UserName { get; set; }
        public int Age { get; set; }
        public bool IsAdmin { get; set; }

        public List<PurchaseItem>? PurchaseHistory { get; set; }
    }
}
