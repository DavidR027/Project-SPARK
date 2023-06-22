using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.AccountRole;
using API.ViewModels.Event;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : GeneralController<Event, EventVM>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper<Event, EventVM> _mapper;
        public EventController(IEventRepository eventRepository, IMapper<Event, EventVM> mapper) : base(eventRepository, mapper)
        {

        }
    }
}
