using API.Utility;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Account
{
    public class ChangePasswordVM
    {
        public string Email { get; set; }
        public int OTP { get; set; }
        [PasswordValidation(ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase, 1 number, 1 symbol, and a minimum of 6 characters")]
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
