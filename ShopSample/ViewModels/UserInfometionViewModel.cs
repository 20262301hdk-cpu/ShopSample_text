using System.ComponentModel.DataAnnotations;

namespace ShopSample.ViewModels
{
    public class UserInfometionViewModel
    {
        [Display(Name = "ユーザー名")]
        public string? UserName { get; set; }
        [Display(Name = "年齢")]
        public int Age { get; set; }
        [Display(Name = "管理者")]
        public bool IsAdmin { get; set; }

        public List<PurchaseItem>? PurchaseHistory { get; set; }
    }
}
