using API.Models;
using API.ViewModels.Account;

namespace API.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account>
    {
        LoginVM Login(LoginVM loginVM);
        /*int Register(RegisterVM registerVM);*/
        int ChangePasswordAccount(Guid? userId, ChangePasswordVM changePasswordVM);
        int UpdateOTP(Guid? userId);

        IEnumerable<string> GetRoles(Guid guid);
    }
}
