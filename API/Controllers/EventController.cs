using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.AccountRole;
using API.ViewModels.Event;
using API.ViewModels.Others;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

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
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        [HttpGet("GetListParticipantByGuid")]
        public IActionResult GetListParticipantByGuid(Guid guid)
        {
            var listParticipant = _eventRepository.GetListParticipantByGuid(guid);
            if (listParticipant is null)
            {
                return NotFound(new ResponseVM<ListParticipantVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found",
                    Data = null
                });
            }

            return Ok(new ResponseVM<IEnumerable<ListParticipantVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = listParticipant
            });
        }

        [HttpGet("GetWaitingListByGuid")]
        public IActionResult GetWaitingListByGuid(Guid guid)
        {
            var listParticipant = _eventRepository.GetWaitingListByGuid(guid);
            if (listParticipant is null)
            {
                return NotFound(new ResponseVM<WaitingListVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found",
                    Data = null
                });
            }

            return Ok(new ResponseVM<IEnumerable<WaitingListVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = listParticipant
            });
        }

        [HttpGet("GetMyEvent")]
        public IActionResult GetMyEvent(Guid guid)
        {
            var listEvent = _eventRepository.GetMyEvent(guid);
            if (listEvent is null)
            {
                return NotFound(new ResponseVM<EventVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found",
                    Data = null
                });
            }

            return Ok(new ResponseVM<IEnumerable<EventVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = listEvent
            });
        }

        [HttpGet("GetMyEventUser")]
        public IActionResult GetMyEventUser(Guid guid)
        {
            var listEvent = _eventRepository.GetMyEventUser(guid);
            if (listEvent is null)
            {
                return NotFound(new ResponseVM<EventVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found",
                    Data = null
                });
            }

            return Ok(new ResponseVM<IEnumerable<EventVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = listEvent
            });
        }
    }
}
