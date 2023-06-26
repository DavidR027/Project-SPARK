using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Newtonsoft.Json;

namespace Client.Repositories.Data
{
    public class EventRepository : GeneralRepository<Event, Guid>, IEventRepository
    {
        private readonly HttpClient httpClient;
        private readonly string request;
        public EventRepository(string request = "Event/") : base(request)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7042/api/")
            };
            this.request = request;
        }

        public async Task<ResponseListVM<ListParticipant>> GetListParticipantByGuid(Guid guid)
        {
            ResponseListVM<ListParticipant> entityVM = null;
            using (var response = httpClient.GetAsync(request + "GetListParticipantByGuid?guid=" + guid).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseListVM<ListParticipant>>(apiResponse);
            }
            return entityVM;
        }
    }
}
