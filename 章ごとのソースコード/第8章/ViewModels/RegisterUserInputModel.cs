using ShopSample.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopSample.ViewModels
{
    public class RegisterUserInputModel
    {
        [Display(Name = "ユーザー名")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "自己紹介")]
        public string Bio { get; set; } = string.Empty;

        [Display(Name = "性別")]
        public string Gender { get; set; } = string.Empty;

        [Display(Name = "いつも使う支払い方法")]
        // Enumを使わない
        public string PaymentMethod { get; set; } = string.Empty;
        // Enumを使う
        //public PaymentMethod PaymentMethod { get; set; }

        [Display(Name = "趣味（複数選択可）")]
        public List<string> Hobbies { get; set; } = new();

        [Display(Name = "誕生日")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "利用規約への同意")]
        public bool AcceptTerms { get; set; }
    }
}
