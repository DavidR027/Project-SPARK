using API.Contracts;
using API.Models;
using API.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : GeneralController<Account, AccountVM>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper<Account, AccountVM> _mapper;
        public AccountController(IAccountRepository accountRepository, IMapper<Account, AccountVM> mapper) : base(accountRepository, mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

    }
}
