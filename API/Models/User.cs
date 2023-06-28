using API.Utility;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_users")]
    public class User : BaseEntity
    {
        [Column("username")]
        public string Username { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string? LastName { get; set; }
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }
        [Column("gender")]
        public GenderLevel Gender { get; set; }
        [Column("email", TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        [Column("phone_number", TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }

        public Account? Account { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}
