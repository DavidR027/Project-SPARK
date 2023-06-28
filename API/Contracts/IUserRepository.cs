using API.Models;

namespace API.Contracts
{
    public interface IUserRepository : IGeneralRepository<User>
    {
        bool CheckEmailAndPhoneAndUsername(string value);
        Guid? FindGuidByEmail(string email);

    }
}
