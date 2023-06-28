using Client.Models;
using Client.Repositories;
using Client.Repositories.Interface;

namespace Client.Repositories.Data
{
    public class UserRepository : GeneralRepository<User, Guid>, IUserRepository
    {
        public UserRepository(string request = "User/") : base(request)
        {

        }

    }
}
