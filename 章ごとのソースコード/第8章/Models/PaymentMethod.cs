using System.ComponentModel.DataAnnotations;

namespace ShopSample.Models
{
    public enum PaymentMethod
    {
        [Display(Name = "クレジットカード")]
        CreditCard,

        [Display(Name = "PoyPol")]
        PoyPol,

        [Display(Name = "コンビニ払い")]
        ConvenienceStore,

        [Display(Name = "PoyPoy")]
        PoyPoy
    }
}
