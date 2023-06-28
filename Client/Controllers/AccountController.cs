using Client.Models;
using Client.Repositories.Data;
using Client.Repositories.Interface;
using Client.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository repository;

        public AccountController(IAccountRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Logins()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logins(LoginVM loginVM)
        {
            var result = await repository.Logins(loginVM);
            if (result is null)
            {
                return RedirectToAction("Error", "Home");
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            else if (result.Code == 200)
            {
                HttpContext.Session.SetString("JWToken", result.Data);
                return RedirectToAction("Index", "Event");
            }
            return View();


        }

        [HttpGet("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Account/Logins");
        }

        [HttpGet]
        public IActionResult Registers()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registers(RegisterVM registerVM)
        {

            var result = await repository.Registers(registerVM);
            if (result is null)
            {
                return RedirectToAction("Error", "Home");
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                TempData["Error"] = $"Something Went Wrong! - {result.Message}!";
                return View();
            }
            else if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
                return RedirectToAction("Logins", "Account");
            }
            return View();
        }


        [HttpGet]
        public IActionResult RegisterEM()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterEM(RegisterVM registerVM)
        {

            var result = await repository.RegisterEM(registerVM);
            if (result is null)
            {
                return RedirectToAction("Error", "Home");
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                TempData["Error"] = $"Something Went Wrong! - {result.Message}!";
                return View();
            }
            else if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
                return RedirectToAction("Logins", "Account");
            }
            return View();
        }

    }
}
