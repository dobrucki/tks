using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VMRent.Models;
using VMRent.ViewModels;

namespace VMRent.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult All()
        {
            var viewModel = new ListUserViewModel
            {
                Users = _userManager.Users.ToList()
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => string.Equals(u.Id, id));
            return View(new DetailUserViewModel
            {
                
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            return View(new EditUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            //var user = _userManager.Users.FirstOrDefault(u => string.Equals(u.Id, viewModel.UserId));
            var user = await _userManager.FindByIdAsync(viewModel.UserId);
            if (user is null) return NotFound();
            user.Email = viewModel.Email;
            user.UserName = viewModel.UserName;
            user.PhoneNumber = viewModel.PhoneNumber;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> AddToRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            return View(new ModifyUserRolesViewModel
            {
                UserId = user.Id,
                UserRoles = await _userManager.GetRolesAsync(user)
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(ModifyUserRolesViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var user = await _userManager.FindByIdAsync(viewModel.UserId);
            if (user is null) return NotFound();
            await _userManager.AddToRoleAsync(user, viewModel.Role);
            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound();
            return View(new ModifyUserRolesViewModel
            {
                UserId = user.Id,
                UserRoles = await _userManager.GetRolesAsync(user)
            });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromRole(ModifyUserRolesViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var user = await _userManager.FindByIdAsync(viewModel.UserId);
            if (user is null) return NotFound();
            await _userManager.RemoveFromRoleAsync(user, viewModel.Role);
            return RedirectToAction("All");
        }
    }
}