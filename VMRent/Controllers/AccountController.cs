using Microsoft.AspNetCore.Mvc;
using VMRent.ViewModels;

namespace VMRent.Controllers
{
    public class AccountController : Controller
    {
        // GET
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        // POST
        [HttpPost]
        public IActionResult Login(LoginUserViewModel viewModel)
        {
            return View();
        }
    }
}