using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        public AccountRepository(SparkDbContext context) : base(context)
        {

        }
    }
}
