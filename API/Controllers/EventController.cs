using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.AccountRole;
using API.ViewModels.Event;
using API.ViewModels.Others;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /*[Authorize]*/
    public class EventController : GeneralController<Event, EventVM>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper<Event, EventVM> _mapper;
        public EventController(IEventRepository eventRepository, IMapper<Event, EventVM> mapper) : base(eventRepository, mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }


        [HttpGet("GetParticipantListByGuid/{guid}")]
        public IActionResult GetParticipantListByGuid(Guid guid)
        {
            var listParticipant = _eventRepository.GetParticipantListByGuid(guid);
            if (listParticipant is null)
            {
                return NotFound(new ResponseVM<ParticipantListVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found",
                    Data = null
                });
            }

            return Ok(new ResponseVM<IEnumerable<ParticipantListVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = listParticipant
            });
        }

        [HttpGet("GetWaitingListByGuid/{guid}")]
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

        [HttpGet("GetMyEvent/{guid}")]
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

        [HttpGet("GetMyEventUser/{guid}")]
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
