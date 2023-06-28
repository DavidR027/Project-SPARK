using Client.Models;
using Client.Repositories;

namespace Client.Repositories.Interface
{
    public interface IUserRepository : IRepository<User, Guid>
    {

    }
}
