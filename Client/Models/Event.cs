using Microsoft.Identity.Client;

namespace Client.Models
{
    public class Event
    {
        public Guid? Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Poster { get; set; }
        public EventStatus Status { get; set; }
        public int Quota { get; set; }
        public bool IsPaid { get; set; }
        public string? Price { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Organizer { get; set; }
        public bool IsValid { get; set; }
    }
    public enum EventStatus
    {
        Offline,
        Online
    }
}
