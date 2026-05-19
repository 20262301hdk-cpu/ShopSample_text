using ShopSample.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopSample.ViewModels
{
    public class RegisterUserInputModel
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "RequiredError")]
        [StringLength(20, ErrorMessage = "StringLengthError")]
        public string? UserName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "RequiredError")]
        [EmailAddress(ErrorMessage = "FormatError")]
        public string? Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "RequiredError")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "StringLengthRangeError")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "ConfirmPassword")]
        [Required(ErrorMessage = "RequiredError")]
        [Compare(nameof(Password), ErrorMessage = "CompareError")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "RequiredError")]
        [Phone(ErrorMessage = "FormatError")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Bio")]
        [StringLength(500, ErrorMessage = "StringLengthError")]
        public string? Bio { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "SelectRequiredError")]
        public string? Gender { get; set; }

        [Display(Name = "PaymentMethod")]
        [Required(ErrorMessage = "SelectRequiredError")]
        public PaymentMethod? PaymentMethod { get; set; }

        [Display(Name = "Hobbies")]
        public List<string> Hobbies { get; set; } = new();

        [Display(Name = "BirthDate")]
        [Required(ErrorMessage = "InputRequiredError")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "AcceptTerms")]
        [AllowedValues(true, ErrorMessage = "{0}にチェックを入れてください。")]
        public bool AcceptTerms { get; set; }
    }
}