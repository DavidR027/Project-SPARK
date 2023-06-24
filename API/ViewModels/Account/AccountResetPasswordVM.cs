using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Account
{
    public class AccountResetPasswordVM
    {
        // Kelompok 5

        [Required(ErrorMessage = "OTP is required")]
        public int OTP { get; set; }

        /*   [Required(ErrorMessage = "OTP is required")]*/
        public string Email { get; set; }
    }
}
