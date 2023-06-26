namespace Client.Models
{
    public class Payment
    {
        public Guid Guid { get; set; }
        public Guid UserGuid { get; set; }
        public Guid EventGuid { get; set; }
        public byte[]? Invoice { get; set; }
    }
}
