using Client.Models;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository repository;

        public PaymentController(IPaymentRepository repository)
        {
            this.repository = repository;
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
                return RedirectToAction(nameof(Index));
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }

            return View();
        }

        [Authorize(Roles = "Admin, User")]
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

            }
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Remove(Guid guid)
        {
            var result = await repository.Delete(guid);
            if (result.Code == 200)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


    }
}
