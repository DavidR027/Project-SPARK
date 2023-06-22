using Client.Models;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository repository;

        public EventController(IEventRepository repository)
        {
            this.repository = repository;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var universities = new List<Event>();

            if (result.Data != null)
            {
                universities = result.Data.Select(e => new Event
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
                    Organizer = e.Organizer
                }).ToList();
            }

            return View(universities);
        }
    }
}

