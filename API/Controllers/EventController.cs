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
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        public EventController(IEventRepository eventRepository, IMapper<Event, EventVM> mapper, IEmailService emailService, IUserRepository userRepository) : base(eventRepository, mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _emailService = emailService;
            _userRepository = userRepository;
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

        [HttpPut("ValidateEvent")]
        public IActionResult Validate(Event acara)
        {
            var result = _eventRepository.Update(acara);
            var user = _userRepository.GetByGuid(acara.CreatedBy);
            var email = user.Email;
            if (acara.IsValid)
            {
                _emailService.SetEmail(email)
                .SetSubject($"SPARK: '{acara.Name}' Event Status")
                .SetHtmlMessage($"Hello {acara.Organizer}! We would like to inform you that your event '{acara.Name}' has been validated.")
                .SendEmailAsync();
            }
            return Ok();
        }

        [HttpDelete("DeleteEvent/{guid}/{adminGuid}")]
        public IActionResult DeleteEvent(Guid guid, Guid adminGuid)
        {
            var acara = _eventRepository.GetByGuid(guid);
            var user = _userRepository.GetByGuid(acara.CreatedBy);
            var admin = _userRepository.GetByGuid(adminGuid);
            var email = user.Email;
            var result = _eventRepository.Delete(guid);

            if (!result)
            {
                return BadRequest();
            }

            _emailService.SetEmail(email)
                    .SetSubject($"SPARK: '{acara.Name}' Event Status")
                    .SetHtmlMessage($"Hello {acara.Organizer}! We regret to inform you that your event '{acara.Name}' has been invalidated by Admin {admin.Username}. Please contact {admin.Email} for further information.")
                    .SendEmailAsync();

            return Ok();
        }

    }
}

