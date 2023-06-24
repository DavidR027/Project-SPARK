using Client.Models;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Client.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository repository;

        public EventController(IEventRepository repository)
        {
            this.repository = repository;
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
                }).ToList();
            }

            return View(events);
        }

        [HttpGet("Event/Detail/{Guid}")]
        public async Task<IActionResult> Detail(Guid guid)
        {
            var result = await repository.Get(guid);

            if (result.Data == null)
            {
                // If the event is not found, you can handle the error or redirect to an error page
                return NotFound();
            }

            var eventDetail = result.Data; // Assuming the returned data is of type Event or replace it with your actual model

            // Pass the event detail to the view
            return View(eventDetail);
        }

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
                }).ToList();
            }

            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public async Task<IActionResult> Edit(Event acara, Guid guid)
        {
            var result = await repository.Put(acara);
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

        [HttpGet]
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
            }

            return View(acara);
        }

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
            }
            return View(acara);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(Guid guid)
        {
            var result = await repository.Delete(guid);
            if (result.Code == 200)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

    }
}

