using API.Contracts;
using API.Models;
using API.ViewModels.AccountRole;
using API.ViewModels.Event;
using API.ViewModels.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /*[Authorize]*/
    public class PaymentController : GeneralController<Payment, PaymentVM>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        public PaymentController(IPaymentRepository paymentRepository, IMapper<Payment, PaymentVM> mapper, IEventRepository eventRepository, IEmailService emailService, IUserRepository userRepository) : base(paymentRepository, mapper)
        {
            _paymentRepository = paymentRepository;
            _eventRepository = eventRepository;
            _emailService = emailService;
            _userRepository = userRepository;
        }

        [HttpPut("ValidatePayment")]
        public IActionResult Validate(Payment payment)
        {
            var user = _userRepository.GetByGuid(payment.UserGuid);
            var acara = _eventRepository.GetByGuid(payment.EventGuid);
            var email = user.Email;
            var result = _paymentRepository.Update(payment);

            if (!result)
            {
                return BadRequest();
            }

            if (payment.IsValid)
            {
                _emailService.SetEmail(email)
                    .SetSubject($"SPARK: '{acara.Name}' Payment Status")
                    .SetHtmlMessage($"Hello {user.Username}! We would like to inform you that your payment for event '{acara.Name}' has been approved by Organizer {acara.Organizer}.")
                    .SendEmailAsync();
            }
            return Ok();
        }

        [HttpDelete("DeletePayment/{guid}/{userGuid}/{eventGuid}")]
        public IActionResult DeletePayment(Guid guid, Guid userGuid, Guid eventGuid)
        {
            var user = _userRepository.GetByGuid(userGuid);
            var acara = _eventRepository.GetByGuid(eventGuid);
            var eventmaker = _userRepository.GetByGuid(acara.CreatedBy);
            var result = _paymentRepository.Delete(guid);
            var email = user.Email;

            if (!result)
            {
                return BadRequest();
            }

            _emailService.SetEmail(email)
                    .SetSubject($"SPARK: '{acara.Name}' Payment Status")
                    .SetHtmlMessage($"Hello {user.Username}! We regret to inform you that your payment for event '{acara.Name}' has been declined by Organizer {eventmaker.Username}. Please contact {eventmaker.Email} for further information.")
                    .SendEmailAsync();

            return Ok();
        }
    }
}
