using System.ComponentModel.DataAnnotations;

namespace AlborzMarket.Models
{
    public class AddPhoneNumberViewModel
    {
        [Required(ErrorMessage = "پر کردن مقدار اجباری است")]
        [Phone]
        [Display(Name = "شماه تلفن")]
        public string Number { get; set; }
    }
}