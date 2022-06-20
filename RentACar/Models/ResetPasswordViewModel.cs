using System.ComponentModel.DataAnnotations;

namespace RentACar.UI.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
