using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_payments")]
    public class Payment : BaseEntity
    {
        [Column("user_guid")]
        public Guid UserGuid { get; set; }
        [Column("event_guid")]
        public Guid EventGuid { get; set; }
        [Column("invoice")]
        public byte[]? Invoice { get; set; }
        [Column("is_valid")]
        public bool IsValid { get; set; }

        public User? User { get; set; }
        public Event? Event { get; set; }
    }
}
