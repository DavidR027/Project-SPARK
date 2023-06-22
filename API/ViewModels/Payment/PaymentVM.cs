namespace API.ViewModels.Payment
{
    public class PaymentVM
    {
        public Guid? Guid { get; set; }
        public Guid UserGuid { get; set; }
        public Guid EventGuid { get; set; }
        public byte[]? Invoice { get; set; }
    }
}
