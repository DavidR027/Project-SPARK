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

        public IEnumerable<ParticipantListVM>? GetParticipantListByGuid(Guid guid)
        {
            var participants = _context.Payments
            .Where(payment => payment.EventGuid == guid && payment.IsValid == true)
            .Select(payment => new ParticipantListVM
            {
                UserGuid = payment.UserGuid,
                FullName = payment.User.FirstName + " " + payment.User.LastName,
                Email = payment.User.Email,
                PhoneNumber = payment.User.PhoneNumber
            })
            .ToList();

            return participants;
        }

        public IEnumerable<EventVM>? GetMyEvent(Guid guid)
        {
            var acara = _context.Events
                .Where(acara => acara.CreatedBy == guid)
                .Select(acara => new EventVM
                {
                    Guid = acara.Guid,
                    Name = acara.Name,
                    Description = acara.Description,
                    Poster = acara.Poster,
                    Status = acara.Status,
                    Quota = acara.Quota,
                    IsPaid = acara.IsPaid,
                    Price = acara.Price,
                    Location = acara.Location,
                    StartDate = acara.StartDate,
                    EndDate = acara.EndDate,
                    Organizer = acara.Organizer,
                    IsValid = acara.IsValid,
                    CreatedBy = acara.CreatedBy,
                }).ToList();

            return acara;
        }

        public IEnumerable<EventVM>? GetMyEventUser(Guid guid)
        {
            var events = from e in _context.Events
                         join p in _context.Payments on e.Guid equals p.EventGuid
                         where p.UserGuid == guid
                         select new EventVM
                         {
                             Guid = e.Guid,
                             Name = e.Name,
                             Description = e.Description,
                             Poster = e.Poster,
                             Status = e.Status,
                             Quota = e.Quota,
                             IsPaid = e.IsPaid,
                             Price = e.Price,
                             Location = e.Location,
                             StartDate = e.StartDate,
                             EndDate = e.EndDate,
                             Organizer = e.Organizer,
                             IsValid = e.IsValid,
                             CreatedBy = e.CreatedBy
                         };

            return events.ToList();
        }

        public IEnumerable<WaitingListVM>? GetWaitingListByGuid(Guid guid)
        {
            var waitingList = _context.Payments
            .Where(payment => payment.EventGuid == guid && payment.IsValid == false)
            .Select(payment => new WaitingListVM
            {
                UserGuid = payment.UserGuid,
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
