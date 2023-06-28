using API.Models;
using API.ViewModels.Event;

namespace API.Contracts
{
    public interface IEventRepository : IGeneralRepository<Event>
    {
        IEnumerable<ListParticipantVM>? GetListParticipantByGuid (Guid guid);

        IEnumerable<WaitingListVM>? GetWaitingListByGuid(Guid guid);
        IEnumerable<EventVM>? GetMyEvent(Guid guid);
        IEnumerable<EventVM>? GetMyEventUser(Guid guid);

    }
}
