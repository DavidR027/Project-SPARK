using API.Contracts;
using API.Models;
using API.ViewModels.AccountRole;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : GeneralController<AccountRole, AccountRoleVM>
    {
        public AccountRoleController(IAccountRoleRepository accountRoleRepository, IMapper<AccountRole, AccountRoleVM> mapper)
            : base(accountRoleRepository, mapper)
        {
        }
    }
}
