using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class PaymentRepository : GeneralRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(SparkDbContext context) : base(context)
        {

        }
    }
}
