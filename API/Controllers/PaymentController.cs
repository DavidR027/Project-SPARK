using API.Contracts;
using API.Models;
using API.ViewModels.AccountRole;
using API.ViewModels.Payment;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : GeneralController<Payment, PaymentVM>
    {
        public PaymentController(IPaymentRepository paymentRepository, IMapper<Payment, PaymentVM> mapper) : base(paymentRepository, mapper)
        {
        }
    }
}
