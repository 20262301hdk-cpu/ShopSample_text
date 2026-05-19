using System.ComponentModel.DataAnnotations;

namespace ShopSample.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "メールアドレスを入力してください。")]
        [EmailAddress]
        [Display(Name = "メールアドレス")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "パスワードを入力してください。")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = 
            "パスワードは6文字以上で入力してください。")]
        [Display(Name = "パスワード")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "確認用パスワードを入力してください。")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "パスワードが一致しません。")]
        [Display(Name = "パスワード（確認）")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

}
