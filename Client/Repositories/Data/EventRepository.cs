using Client.Models;
using Client.Repositories.Interface;

namespace Client.Repositories.Data
{
    public class EventRepository : GeneralRepository<Event, Guid>, IEventRepository
    {
        public EventRepository(string request = "Event/") : base(request)
        {

        }
    }
}
