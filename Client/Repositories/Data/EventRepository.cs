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

        public async Task<ResponseListVM<ParticipantList>> GetParticipantListByGuid(Guid guid)
        {
            ResponseListVM<ParticipantList> entityVM = null;
            using (var response = httpClient.GetAsync(request + "GetParticipantListByGuid/" + guid).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseListVM<ParticipantList>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseListVM<WaitingList>> GetWaitingListByGuid(Guid guid)
        {
            ResponseListVM<WaitingList> entityVM = null;
            using (var response = httpClient.GetAsync(request + "GetWaitingListByGuid/" + guid).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseListVM<WaitingList>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseListVM<Event>> GetMyEvent(Guid guid)
        {
            ResponseListVM<Event> entityVM = null;
            using (var response = httpClient.GetAsync(request + "GetMyEvent/" + guid).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseListVM<Event>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseListVM<Event>> GetMyEventUser(Guid guid)
        {
            ResponseListVM<Event> entityVM = null;
            using (var response = httpClient.GetAsync(request + "GetMyEventUser/" + guid).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseListVM<Event>>(apiResponse);
            }
            return entityVM;
        }
    }
}
