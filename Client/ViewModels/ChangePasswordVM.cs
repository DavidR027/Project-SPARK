using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class ChangePasswordVM
    {
        public string Email { get; set; }
        public int OTP { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase, 1 number, 1 symbol, and a minimum of 6 characters")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
