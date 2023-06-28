using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.ViewModels
{
    public class RegisterVM
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase, 1 number, 1 symbol, and a minimum of 6 characters")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

    public enum GenderLevel
    {
        Female,
        Male
    }
}
