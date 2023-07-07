using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository repository;

        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var users = new List<User>();

            if (result.Data != null)
            {
                users = result.Data.Select(e => new User
                {
                    Guid = e.Guid,
                    Username = e.Username,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    BirthDate = e.BirthDate,
                    Gender = e.Gender,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                }).ToList();
            }

            return View(users);
        }

        [Authorize]
        [HttpGet("User/Detail/{Guid}")]
        public async Task<IActionResult> Detail(Guid guid)
        {
            var result = await repository.Get(guid);

            if (result.Data == null)
            {
                // If the event is not found, you can handle the error or redirect to an error page
                return NotFound();
            }

            var userDetail = result.Data; // Assuming the returned data is of type Event or replace it with your actual model
            // Pass the event detail to the view
            return View(userDetail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(User user, Guid guid)
        {
            var result = await repository.Put(user);
            if (result.Code == 200)
            {
                return Redirect($"/User/Detail/{guid}");
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            return View();
        }

        [HttpGet("/User/Edit/{guid}")]
        [Authorize]
        public async Task<IActionResult> Edit(Guid guid)
        {
            var result = await repository.Get(guid);
            var user = new User();
            if (result.Data?.Guid is null)
            {
                return View(user);
            }
            else
            {
                user.Guid = result.Data.Guid;
                user.Username = result.Data.Username;
                user.FirstName = result.Data.FirstName;
                user.LastName = result.Data.LastName;
                user.BirthDate = result.Data.BirthDate;
                user.Gender = result.Data.Gender;
                user.Email = result.Data.Email;
                user.PhoneNumber = result.Data.PhoneNumber;
            }

            return View(user);
        }

        [Authorize]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var result = await repository.Get(guid);
            var user = new User();
            if (result.Data?.Guid is null)
            {
                return View(user);
            }
            else
            {
                user.Guid = result.Data.Guid;
                user.Username = result.Data.Username;
                user.FirstName = result.Data.FirstName;
                user.LastName = result.Data.LastName;
                user.BirthDate = result.Data.BirthDate;
                user.Gender = result.Data.Gender;
                user.Email = result.Data.Email;
                user.PhoneNumber = result.Data.PhoneNumber;
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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

