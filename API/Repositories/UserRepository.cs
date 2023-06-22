using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class UserRepository : GeneralRepository<User>, IUserRepository
    {
        public UserRepository(SparkDbContext context) : base(context)
        {

        }
    }
}
