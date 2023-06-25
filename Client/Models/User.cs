using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models
{
    public class User
    {
        public Guid? Guid { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }

    public enum GenderLevel
    {
        Female,
        Male
    }
}
