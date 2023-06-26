using API.Contexts;
using API.Contracts;
using API.Models;
using API.ViewModels.Event;
using API.Contexts;
using API.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public IEnumerable<ListParticipantVM>? GetListParticipantByGuid(Guid guid)
        {
            var participants = _context.Payments
            .Where(payment => payment.EventGuid == guid && payment.IsValid == true)
            .Select(payment => new ListParticipantVM
            {
                FullName = payment.User.FirstName+ " " +payment.User.LastName,
                Email = payment.User.Email,
                PhoneNumber = payment.User.PhoneNumber
            })
            .ToList();

            return participants;
        }

        public IEnumerable<WaitingListVM>? GetWaitingListByGuid(Guid guid)
        {
            var waitingList = _context.Payments
            .Where(payment => payment.EventGuid == guid && payment.IsValid == false)
            .Select(payment => new WaitingListVM
            {
                FullName = payment.User.FirstName + " " + payment.User.LastName,
                Email = payment.User.Email,
                PhoneNumber = payment.User.PhoneNumber,
                PaymentGuid = payment.Guid,
                Invoice = payment.Invoice
            })
            .ToList();

            return waitingList;
        }
    }
}
