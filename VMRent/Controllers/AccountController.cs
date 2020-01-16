using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VMRent.Models;
using VMRent.ViewModels;

namespace VMRent.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager;

        public AccountController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        #region Login
        
        // GET
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        // POST
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            
            var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password,
                viewModel.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Provided credentials are incorrect. ");

            return View(viewModel);
        }
        
        #endregion

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}