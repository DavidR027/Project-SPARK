namespace Client.ViewModels
{
    public class WaitingList
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid PaymentGuid { get; set; }
        public byte[]? Invoice { get; set; }
    }
}
