using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_event_forms")]
    public class EventForm : BaseEntity
    {
        [Column("user_guid")]
        public Guid UserGuid { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }

        public ICollection<Event>? Events { get; set;}
        public Event? Event { get; set; }
        public User? User { get; set; }
    }
}
