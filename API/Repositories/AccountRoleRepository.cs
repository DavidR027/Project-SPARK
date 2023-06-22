using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(SparkDbContext context) : base(context)
        {

        }
    }
}
