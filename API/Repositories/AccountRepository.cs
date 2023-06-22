using API.Contexts;
using API.Contracts;
using API.Models;
using API.ViewModels.Account;

namespace API.Repositories
{
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        public AccountRepository(SparkDbContext context) : base(context)
        {

        }

        public int ChangePasswordAccount(Guid? userId, ChangePasswordVM changePasswordVM)
        {
            var account = new Account();
            account = _context.Set<Account>().FirstOrDefault(a => a.Guid == userId);
            if (account == null || account.OTP != changePasswordVM.OTP)
            {
                return 2;
            }
            // Cek apakah OTP sudah digunakan
            if (account.IsUsed)
            {
                return 3;
            }
            // Cek apakah OTP sudah expired
            if (account.ExpiredTime < DateTime.Now)
            {
                return 4;
            }
            // Cek apakah NewPassword dan ConfirmPassword sesuai
            if (changePasswordVM.NewPassword != changePasswordVM.ConfirmPassword)
            {
                return 5;
            }
            // Update password
            account.Password = changePasswordVM.NewPassword;
            account.IsUsed = true;
            try
            {
                var updatePassword = Update(account);
                if (!updatePassword)
                {
                    return 0;
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public IEnumerable<string> GetRoles(Guid guid)
        {
            var getAccount = GetByGuid(guid);
            if (getAccount == null) return Enumerable.Empty<string>();
            var getAccountRoles = from accountRoles in _context.AccountRoles
                                  join roles in _context.Roles on accountRoles.RoleGuid equals roles.Guid
                                  where accountRoles.AccountGuid == guid
                                  select roles.Name;

            return getAccountRoles.ToList();
        }

        public LoginVM Login(LoginVM loginVM)
        {
            var account = GetAll();
            var user = _context.Users.ToList();

            var query = from us in user
                        join acc in account
                        on us.Guid equals acc.Guid
                        where us.Email == loginVM.Username
                        select new LoginVM
                        {
                            Username = us.Username,
                            Password = acc.Password
                        };
            var data = query.FirstOrDefault();

            if (data != null && Hashing.ValidatePassword(loginVM.Password, data.Password))
            {
                loginVM.Password = data.Password;
            }

            return data;
        }

        public int UpdateOTP(Guid? userId)
        {
            var account = new Account();
            account = _context.Set<Account>().FirstOrDefault(a => a.Guid == userId);
            //Generate OTP
            Random rnd = new Random();
            var getOtp = rnd.Next(100000, 999999);
            account.OTP = getOtp;

            //Add 5 minutes to expired time
            account.ExpiredTime = DateTime.Now.AddMinutes(5);
            account.IsUsed = false;
            try
            {
                var check = Update(account);


                if (!check)
                {
                    return 0;
                }
                return getOtp;
            }
            catch
            {
                return 0;
            }
        }
    }
}
