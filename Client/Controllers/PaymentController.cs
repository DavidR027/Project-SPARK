using Client.Models;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Client.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository repository;
        private readonly IUserRepository userRepository;
        private readonly IEventRepository eventRepository;
        private readonly IEmailService emailService;

        public PaymentController(IPaymentRepository repository, IUserRepository userRepository, IEventRepository eventRepository, IEmailService emailService)
        {
            this.repository = repository;
            this.userRepository = userRepository;
            this.eventRepository = eventRepository;
            this.emailService = emailService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var events = new List<Payment>();

            if (result.Data != null)
            {
                events = result.Data.Select(e => new Payment
                {
                    Guid = e.Guid,
                    UserGuid = e.UserGuid,
                    EventGuid = e.EventGuid,
                    Invoice = e.Invoice
                }).ToList();
            }

            return View(events);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Payment payment)
        {

            var result = await repository.Post(payment);
            if (result.Code == 200)
            {
                return Redirect($"/Event/Detail/{payment.EventGuid}");
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "EventMaker")]
        public async Task<IActionResult> Edit(Payment payment, Guid guid)
        {
            var user = await userRepository.Get(payment.UserGuid);
            var acara = await eventRepository.Get(payment.EventGuid);
            var email = user.Data.Email;
            var result = await repository.Put(payment);
            if (result.Code == 200)
            {
                emailService.SetEmail(email)
                     .SetSubject($"SPARK: '{acara.Data.Name}' Payment Status")
                     .SetHtmlMessage($"Hello {user.Data.Username}! We would like to inform you that your payment for event '{acara.Data.Name}' has been approved by Organizer {acara.Data.Organizer}.")
                     .SendEmailAsync();
                return RedirectToAction("WaitingList", "Event", new { Guid = payment.EventGuid});
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "EventMaker")]
        public async Task<IActionResult> Edit(Guid guid)
        {
            var result = await repository.Get(guid);
            var payment = new Payment();
            if (result.Data?.Guid is null)
            {
                return View(payment);
            }
            else
            {
                payment.Guid = result.Data.Guid;
                payment.UserGuid = result.Data.UserGuid;
                payment.EventGuid = result.Data.EventGuid;
                payment.Invoice = result.Data.Invoice;
                payment.IsValid = result.Data.IsValid;
            }

            return View(payment);
        }


        [Authorize(Roles = "EventMaker")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var result = await repository.Get(guid);
            var payment = new Payment();
            if (result.Data?.Guid is null)
            {
                return View(payment);
            }
            else
            {
                payment.Guid = result.Data.Guid;
                payment.EventGuid = result.Data.EventGuid;
                payment.UserGuid = result.Data.UserGuid;
                payment.Invoice = result.Data.Invoice;
                payment.IsValid = result.Data.IsValid;

            }
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, EventMaker")]
        public async Task<IActionResult> Remove(Guid guid, Guid userGuid, Guid eventGuid)
        {
            var user = await userRepository.Get(userGuid);
            var acara = await eventRepository.Get(eventGuid);
            var eventmaker = await userRepository.Get(acara.Data.CreatedBy);
            var result = await repository.Delete(guid);
            var email = user.Data.Email;
            if (result.Code == 200)
            {
                emailService.SetEmail(email)
                     .SetSubject($"SPARK: '{acara.Data.Name}' Payment Status")
                     .SetHtmlMessage($"Hello {user.Data.Username}! We regret to inform you that your payment for event '{acara.Data.Name}' has been declined by Organizer {eventmaker.Data.Username}. Please contact {eventmaker.Data.Email} for further information.")
                     .SendEmailAsync();
                return RedirectToAction("Index", "Event");
            }
            return View();
        }


    }
}
