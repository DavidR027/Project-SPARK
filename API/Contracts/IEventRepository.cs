using API.Models;
using API.ViewModels.Event;

namespace API.Contracts
{
    public interface IEventRepository : IGeneralRepository<Event>
    {
        IEnumerable<ListParticipantVM>? GetListParticipantByGuid (Guid guid);

    }
}
