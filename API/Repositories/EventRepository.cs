using API.Contexts;
using API.Contracts;
using API.Models;
using API.ViewModels.Event;
using API.Contexts;
using API.Contexts;

namespace API.Repositories
{
    public class EventRepository : GeneralRepository<Event>, IEventRepository
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserRepository _userRepository;

        public EventRepository(SparkDbContext context, IPaymentRepository paymentRepository, IUserRepository userRepository) : base(context)
        {
            _paymentRepository = paymentRepository;
            _userRepository = userRepository;
        }

    }
}
