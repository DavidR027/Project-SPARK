using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class AccountResetPasswordVM
    {
        [Required(ErrorMessage = "OTP is required")]
        public int OTP { get; set; }

        public string Email { get; set; }
    }
}
