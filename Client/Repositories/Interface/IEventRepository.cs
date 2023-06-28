using Client.Models;
using Client.ViewModels;

namespace Client.Repositories.Interface
{
    public interface IEventRepository : IRepository<Event, Guid>
    {
        public Task<ResponseListVM<ListParticipant>> GetListParticipantByGuid(Guid guid);

        public Task<ResponseListVM<WaitingList>> GetWaitingListByGuid(Guid guid);
        public Task<ResponseListVM<Event>> GetMyEvent(Guid guid);
        public Task<ResponseListVM<Event>> GetMyEventUser(Guid guid);
    }
}
