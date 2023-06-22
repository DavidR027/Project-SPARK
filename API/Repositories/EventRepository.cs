using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class EventRepository : GeneralRepository<Event>, IEventRepository
    {
        public EventRepository(SparkDbContext context) : base(context)
        {

        }
    }
}
