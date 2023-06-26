namespace API.ViewModels.Event
{
    public class WaitingListVM
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid PaymentGuid { get; set; }
        public byte[]? Invoice { get; set; }
    }
}
