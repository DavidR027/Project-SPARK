using Client.Models;
using Client.ViewModels;

namespace Client.Repositories.Interface
{
    public interface IAccountRepository : IRepository<Account, string>
    {
        public Task<ResponseViewModel<string>> Logins(LoginVM entity);
        public Task<ResponseMessageVM> Registers(RegisterVM entity);

        public Task<ResponseMessageVM> RegisterEM(RegisterVM entity);

        public Task<ResponseMessageVM> ForgotPassword(AccountResetPasswordVM entity, string email);
        public Task<ResponseMessageVM> ChangePassword(ChangePasswordVM entity);


    }
}
