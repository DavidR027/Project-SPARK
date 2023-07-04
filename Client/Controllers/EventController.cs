using Client.Models;
using Client.Repositories.Data;
using Client.Repositories.Interface;
using Client.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.IO;

namespace Client.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository repository;
        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;
        public EventController(IEventRepository repository, IEmailService emailService, IUserRepository userRepository)
        {
            this.repository = repository;
            this.emailService = emailService;   
            this .userRepository = userRepository;  
        }

        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var events = new List<Event>();

            if (result.Data != null)
            {
                events = result.Data.Select(e => new Event
                {
                    Guid = e.Guid,
                    Name = e.Name,
                    Description = e.Description,
                    Poster = e.Poster,
                    Status = e.Status,
                    Quota = e.Quota,
                    IsPaid = e.IsPaid,
                    Price = e.Price,
                    Location = e.Location,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Organizer = e.Organizer,
                    IsValid = e.IsValid,
                    CreatedBy = e.CreatedBy,
                }).ToList();
            }

            return View(events);
        }

        [Authorize(Roles = "EventMaker")]
        public async Task<IActionResult> ListParticipant(Guid guid)
        {
            var result = await repository.GetParticipantListByGuid(guid);
            var events = new List<ParticipantList>();

            if (result.Data != null)
            {
                events = result.Data.Select(e => new ParticipantList
                {
                    UserGuid = e.UserGuid,
                    FullName = e.FullName,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber
                }).ToList();
            }

            return View(events);
        }

        [Authorize(Roles = "EventMaker")]
        public async Task<IActionResult> WaitingList(Guid guid)
        {
            var result = await repository.GetWaitingListByGuid(guid);
            var waitingList = new List<WaitingList>();

            if (result.Data != null)
            {
                waitingList = result.Data.Select(e => new WaitingList
                {
                    UserGuid = e.UserGuid,
                    FullName = e.FullName,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    PaymentGuid = e.PaymentGuid,
                    Invoice = e.Invoice
                }).ToList();
            }

            return View(waitingList);
        }

        [HttpGet("Event/Detail/{Guid}")]
        public async Task<IActionResult> Detail(Guid guid)
        {
            var result = await repository.Get(guid);

            if (result.Data == null)
            {
                return NotFound();
            }

            var eventDetail = result.Data;

            return View(eventDetail);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> IndexAdmin()
        {
            var result = await repository.Get();
            var events = new List<Event>();

            if (result.Data != null)
            {
                events = result.Data.Select(e => new Event
                {
                    Guid = e.Guid,
                    Name = e.Name,
                    Description = e.Description,
                    Poster = e.Poster,
                    Status = e.Status,
                    Quota = e.Quota,
                    IsPaid = e.IsPaid,
                    Price = e.Price,
                    Location = e.Location,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Organizer = e.Organizer,
                    IsValid = e.IsValid,
                    CreatedBy = e.CreatedBy,
                }).ToList();
            }

            return View(events);
        }

        [Authorize(Roles = "EventMaker")]
        public async Task<IActionResult> MyEvent(Guid guid)
        {
            var result = await repository.GetMyEvent(guid);
            var events = new List<Event>();

            if (result.Data != null)
            {
                events = result.Data.Select(e => new Event
                {
                    Guid = e.Guid,
                    Name = e.Name,
                    Description = e.Description,
                    Poster = e.Poster,
                    Status = e.Status,
                    Quota = e.Quota,
                    IsPaid = e.IsPaid,
                    Price = e.Price,
                    Location = e.Location,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Organizer = e.Organizer,
                    IsValid = e.IsValid,
                    CreatedBy = e.CreatedBy,
                }).ToList();
            }

            return View(events);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> JoinedEvent(Guid guid)
        {
            var result = await repository.GetMyEventUser(guid);
            var events = new List<Event>();

            if (result.Data != null)
            {
                events = result.Data.Select(e => new Event
                {
                    Guid = e.Guid,
                    Name = e.Name,
                    Description = e.Description,
                    Poster = e.Poster,
                    Status = e.Status,
                    Quota = e.Quota,
                    IsPaid = e.IsPaid,
                    Price = e.Price,
                    Location = e.Location,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Organizer = e.Organizer,
                    IsValid = e.IsValid,
                    CreatedBy = e.CreatedBy,
                }).ToList();
            }

            return View(events);
        }

        [HttpGet]
        [Authorize(Roles = "EventMaker")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "EventMaker")]
        public async Task<IActionResult> Create(Event acara)
        {

            var result = await repository.Post(acara);
            if (result.Code == 200)
            {
                return RedirectToAction(nameof(Index));
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Event acara, Guid guid)
        {
            var result = await repository.Put(acara);
            var user = await userRepository.Get(acara.CreatedBy);
            var email = user.Data.Email;
            if (result.Code == 200)
            {
                if (acara.IsValid)
                {
                     emailService.SetEmail(email)
                     .SetSubject($"SPARK: '{acara.Name}' Event Status")
                     .SetHtmlMessage($"Hello {acara.Organizer}! We would like to inform you that your event '{acara.Name}' has been validated.")
                     .SendEmailAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid guid)
        {
            var result = await repository.Get(guid);
            var acara = new Event();
            if (result.Data?.Guid is null)
            {
                return View(acara);
            }
            else
            {
                acara.Guid = result.Data.Guid;
                acara.Name = result.Data.Name;
                acara.Description = result.Data.Description;
                acara.Poster = result.Data.Poster;
                acara.Status = result.Data.Status;
                acara.Quota = result.Data.Quota;
                acara.IsPaid = result.Data.IsPaid;
                acara.Price = result.Data.Price;
                acara.Location = result.Data.Location;
                acara.StartDate = result.Data.StartDate;
                acara.EndDate = result.Data.EndDate;
                acara.Organizer = result.Data.Organizer;
                acara.IsValid = result.Data.IsValid;
                acara.CreatedBy = result.Data.CreatedBy;
            }

            return View(acara);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "EventMaker")]
        public async Task<IActionResult> EditEM(Event acara, Guid guid)
        {
            var result = await repository.Put(acara);
            var user = await userRepository.Get(acara.CreatedBy);
            if (result.Code == 200)
            {
                return Redirect($"/Event/Detail/{guid}");
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "EventMaker")]
        public async Task<IActionResult> EditEM(Guid guid)
        {
            var result = await repository.Get(guid);
            var acara = new Event();
            if (result.Data?.Guid is null)
            {
                return View(acara);
            }
            else
            {
                acara.Guid = result.Data.Guid;
                acara.Name = result.Data.Name;
                acara.Description = result.Data.Description;
                acara.Poster = result.Data.Poster;
                acara.Status = result.Data.Status;
                acara.Quota = result.Data.Quota;
                acara.IsPaid = result.Data.IsPaid;
                acara.Price = result.Data.Price;
                acara.Location = result.Data.Location;
                acara.StartDate = result.Data.StartDate;
                acara.EndDate = result.Data.EndDate;
                acara.Organizer = result.Data.Organizer;
                acara.IsValid = result.Data.IsValid;
                acara.CreatedBy = result.Data.CreatedBy;
            }

            return View(acara);
        }

        [Authorize(Roles = "Admin, EventMaker")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var result = await repository.Get(guid);
            var acara = new Event();
            if (result.Data?.Guid is null)
            {
                return View(acara);
            }
            else
            {
                acara.Guid = result.Data.Guid;
                acara.Name = result.Data.Name;
                acara.Description = result.Data.Description;
                acara.Poster = result.Data.Poster;
                acara.Status = result.Data.Status;
                acara.Quota = result.Data.Quota;
                acara.IsPaid = result.Data.IsPaid;
                acara.Price = result.Data.Price;
                acara.Location = result.Data.Location;
                acara.StartDate = result.Data.StartDate;
                acara.EndDate = result.Data.EndDate;
                acara.Organizer = result.Data.Organizer;
                acara.IsValid = result.Data.IsValid;
                acara.CreatedBy = result.Data.CreatedBy;
            }
            return View(acara);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, EventMaker")]
        public async Task<IActionResult> Remove(Guid guid, Guid adminguid)
        {
            var acara = await repository.Get(guid);
            var user = await userRepository.Get(acara.Data.CreatedBy);
            var admin = await userRepository.Get(adminguid);
            var email = user.Data.Email;
            var result = await repository.Delete(guid);
            if (result.Code == 200)
            {
                emailService.SetEmail(email)
                     .SetSubject($"SPARK: '{acara.Data.Name}' Event Status")
                     .SetHtmlMessage($"Hello {acara.Data.Organizer}! We regret to inform you that your event '{acara.Data.Name}' has been invalidated by Admin {admin.Data.Username}. Please contact {admin.Data.Email} for further information.")
                     .SendEmailAsync();
                return RedirectToAction(nameof(IndexAdmin));
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChartEvent()
        {
            var result = await repository.Get();
            var events = result.Data?.Select(e => new Event
            {
                Guid = e.Guid,
                Name = e.Name,
                Description = e.Description,
                Poster = e.Poster,
                Status = e.Status,
                Quota = e.Quota,
                IsPaid = e.IsPaid,
                Price = e.Price,
                Location = e.Location,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Organizer = e.Organizer,
                IsValid = e.IsValid,
                CreatedBy = e.CreatedBy,
            }).ToList();

            // Prepare data for chart
            var chartData = events.GroupBy(e => e.StartDate.ToString("MMMM"))
                                  .Select(g => new { Month = g.Key, Count = g.Count() })
                                  .OrderByDescending(g => g.Month)
                                  .ToList();

            // Pass the chart data to the view as a JSON string
            ViewBag.ChartData = JsonConvert.SerializeObject(chartData);

            return View();
        }

    }
}

