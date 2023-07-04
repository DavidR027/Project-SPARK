using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class ChangePasswordVM
    {
        public string Email { get; set; }

        [RegularExpression("^[0-9]{6}$", ErrorMessage = "OTP must be a 6-digit number")]
        public int OTP { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase, 1 number, 1 symbol, and a minimum of 6 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$", ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase, 1 number, 1 symbol, and a minimum of 6 characters")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
