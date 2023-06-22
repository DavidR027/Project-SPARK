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
        [Column("status")]
        public string Status { get; set; }
        [Column("location")]
        public string Location { get; set; }
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }
        [Column("create_by")]
        public Guid CreateBy { get; set; }

        public Account? Account { get; set; }
        public Payment? Payment { get; set; }
        public EventForm? EventForm { get; set; }
        public User? User { get; set; }
        public ICollection<EventForm>? EventForms { get; set; }
        public ICollection<User>? Users { get; set; }

    }
}
