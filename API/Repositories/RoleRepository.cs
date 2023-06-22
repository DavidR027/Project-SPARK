using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class RoleRepository : GeneralRepository<Role>, IRoleRespository
    {
        public RoleRepository(SparkDbContext context) : base(context)
        {

        }
    }
}
