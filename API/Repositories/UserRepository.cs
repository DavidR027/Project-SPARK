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

        public bool CheckEmailAndPhoneAndUsername(string value)
        {
            return _context.Users.Any(e => e.Email == value ||
                        e.PhoneNumber == value || e.Username == value);
        }

        public Guid? FindGuidByEmail(string email)
        {
            try
            {
                var employee = _context.Users.FirstOrDefault(e => e.Email == email);
                if (employee == null)
                {
                    return null;
                }
                return employee.Guid;
            }
            catch
            {
                return null;
            }
        }
    }
}
