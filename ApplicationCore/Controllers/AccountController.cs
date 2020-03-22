using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VMRent.DomainModel;
using VMRent.ViewModels;

namespace VMRent.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager;

        private UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        #region Login
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password,
                    viewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                throw new Exception("User is not active");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(viewModel);
            
        }
        
        #endregion

        #region Logout
        
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        #endregion

        #region Register
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var user = new User
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                EmailConfirmed = true,
                Active = false,
                PhoneNumber = viewModel.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                ModelState.AddModelError("", "Invalid register attempt");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(viewModel);
            
            //await _signInManager.SignInAsync(user, isPersistent: false);
            //await _userManager.SetLockoutEnabledAsync(user, true);
            //await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now.AddYears(100));
            
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}