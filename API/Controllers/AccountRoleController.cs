using API.Contracts;
using API.Models;
using API.Utility;
using API.ViewModels.AccountRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountRoleController : GeneralController<AccountRole, AccountRoleVM>
    {
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IMapper<AccountRole, AccountRoleVM> _mapper;
        public AccountRoleController(IAccountRoleRepository accountRoleRepository, IMapper<AccountRole, AccountRoleVM> mapper)
            : base(accountRoleRepository, mapper)
        {
            _accountRoleRepository = accountRoleRepository;
            _mapper = mapper;
        }
    }
}
