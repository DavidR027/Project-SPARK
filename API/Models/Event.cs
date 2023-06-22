using API.Utility;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_events")]
    public class Event : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("poster")]
        public byte[]? Poster { get; set; }
        [Column("status")]
        public EventStatus Status { get; set; }
        [Column("quota")]
        public int Quota { get; set; }
        [Column("is_paid")]
        public bool IsPaid { get; set; }
        [Column("price")]
        public string Price { get; set; }
        [Column("location")]
        public string Location { get; set; }
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }
        [Column("organizer")]
        public string Organizer { get; set; }

        public ICollection<Payment>? Payments { get; set; }

    }
}
