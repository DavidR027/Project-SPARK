using API.Utility;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.ViewModels.Account
{
    public class RegisterVM
    {
        [EmailPhoneUsernameValidation("Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        [EmailAddress]
        [EmailPhoneUsernameValidation("Email")]
        public string Email { get; set; }
        [Phone]
        [EmailPhoneUsernameValidation("Phone Number")]
        public string PhoneNumber { get; set; }
        [PasswordValidation(ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase, 1 number, 1 symbol, and a minimum of 6 characters")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
