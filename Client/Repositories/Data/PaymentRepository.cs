using Client.Models;
using Client.Repositories.Interface;

namespace Client.Repositories.Data
{
    public class PaymentRepository : GeneralRepository<Payment, Guid>, IPaymentRepository
    {
        public PaymentRepository(string request = "Payment/") : base(request)
        {

        }
    }
}
