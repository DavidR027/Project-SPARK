using API.Contexts;
using API.Contracts;
using API.Models;
using API.ViewModels.Account;

namespace API.Repositories
{
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        private readonly IUserRepository _userRepository;
        public AccountRepository(SparkDbContext context, IUserRepository userRepository) : base(context)
        {
            _userRepository = userRepository;
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
            account.Password = Hashing.HashPassword(changePasswordVM.NewPassword);
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

        public User GetUserByUsername(string username)
        {
            return _context.Set<User>().FirstOrDefault(a => a.Username == username);
        }

        public LoginVM Login(LoginVM loginVM)
        {
            var account = GetAll();
            var user = _context.Users.ToList();

            var query = from us in user
                        join acc in account
                        on us.Guid equals acc.Guid
                        where us.Username == loginVM.Username || us.Email == loginVM.Username
                        select new LoginVM
                        {
                            Username = us.Username,
                            Password = acc.Password
                        };
            var data = query.FirstOrDefault();

            if (data != null && Hashing.ValidatePassword(loginVM.Password, data.Password))
            {
                // Password is valid
                return data;
            }
            else
            {
                // Password is invalid or account doesn't exist
                return null;
            }
        }

        public int Register(RegisterVM registerVM)
        {
            try
            {
                var user = new User
                {
                    Username= registerVM.Username,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    BirthDate = registerVM.BirthDate,
                    Gender = registerVM.Gender,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                };
                var result = _userRepository.Create(user);

                var account = new Account
                {
                    Guid = user.Guid,
                    Password = Hashing.HashPassword(registerVM.Password),
                    IsDeleted = false,
                    IsUsed = true,
                    OTP = 0
                };
                Create(account);

                var accountRole = new AccountRole
                {
                    RoleGuid = Guid.Parse("a4c1b16c-9753-4d01-7804-08db60296455"),
                    AccountGuid = user.Guid
                };
                _context.AccountRoles.Add(accountRole);
                _context.SaveChanges();

                return 3;

            }
            catch
            {
                return 0;
            }
        }

        public int RegisterEM(RegisterVM registerVM)
        {
            try
            {
                var user = new User
                {
                    Username = registerVM.Username,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    BirthDate = registerVM.BirthDate,
                    Gender = registerVM.Gender,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                };
                var result = _userRepository.Create(user);

                var account = new Account
                {
                    Guid = user.Guid,
                    Password = Hashing.HashPassword(registerVM.Password),
                    IsDeleted = false,
                    IsUsed = true,
                    OTP = 0
                };
                Create(account);

                var accountRole = new AccountRole
                {
                    RoleGuid = Guid.Parse("5e735041-ce30-43b9-d7aa-08db60bf349a"),
                    AccountGuid = user.Guid
                };
                _context.AccountRoles.Add(accountRole);
                _context.SaveChanges();

                return 3;

            }
            catch
            {
                return 0;
            }
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
