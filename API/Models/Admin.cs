using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_admins")]
    public class Admin : BaseEntity
    {
        [Column("username")]
        public string username { get; set; }

        public Account? Account { get; set; }
    }
}
