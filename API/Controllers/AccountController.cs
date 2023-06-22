using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : GeneralController<Account, Account>
    {
        public AccountController(IGeneralRepository<Account> repository, IMapper<Account, Account> mapper) : base(repository, mapper)
        {
        }
    }
}
