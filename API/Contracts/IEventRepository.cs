using API.Models;
using API.ViewModels.Event;

namespace API.Contracts
{
    public interface IEventRepository : IGeneralRepository<Event>
    {
        IEnumerable<ParticipantListVM>? GetParticipantListByGuid (Guid guid);

        IEnumerable<WaitingListVM>? GetWaitingListByGuid(Guid guid);
        IEnumerable<EventVM>? GetMyEvent(Guid guid);
        IEnumerable<EventVM>? GetMyEventUser(Guid guid);

    }
}
