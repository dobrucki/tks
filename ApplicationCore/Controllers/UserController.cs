using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VMRent.DomainModel;
using VMRent.Services;
using VMRent.ViewModels;

namespace VMRent.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userService;

        private readonly ReservationService _reservationService;

        public UserController(UserManager<User> userService, ReservationService reservationService)
        {
            _userService = userService;
            _reservationService = reservationService;
        }

//        [HttpGet]
//        public IActionResult All()
//        {
//            var viewModel = new ListUserViewModel
//            {
//                Users = _userService.Users.ToList()
//            };
//            return View(viewModel);
//        }
        
        [HttpGet]
        public IActionResult All(string name)
        {
            var viewModel = new ListUserViewModel();
            if (string.IsNullOrWhiteSpace(name))
            {
                viewModel.Users = _userService.Users.ToList();
            }
            else
            {
                viewModel.Users = _userService.Users
                    .Where(u => u.UserName.Contains(name))
                    .ToList();
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var user = _userService.Users.FirstOrDefault(u => string.Equals(u.Id, id));
            return View(new DetailUserViewModel
            {
                User = user,
                UserVms = _reservationService.GetReservationsForUserAsync(user).Result
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userService.FindByIdAsync(id);
            if (user is null) return NotFound();
            return View(new EditUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.Active
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            //var user = _userService.Users.FirstOrDefault(u => string.Equals(u.Id, viewModel.UserId));
            var user = await _userService.FindByIdAsync(viewModel.UserId);
            if (user is null) return NotFound();
            user.Email = viewModel.Email;
            user.UserName = viewModel.UserName;
            user.PhoneNumber = viewModel.PhoneNumber;
            user.Active = viewModel.IsActive;
            await _userService.UpdateAsync(user);
            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> AddToRole(string id)
        {
            var user = await _userService.FindByIdAsync(id);
            if (user is null) return NotFound();
            return View(new ModifyUserRolesViewModel
            {
                UserId = user.Id,
                UserRoles = await _userService.GetRolesAsync(user)
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(ModifyUserRolesViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var user = await _userService.FindByIdAsync(viewModel.UserId);
            if (user is null) return NotFound();
            await _userService.AddToRoleAsync(user, viewModel.Role);
            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromRole(string id)
        {
            var user = await _userService.FindByIdAsync(id);
            if (user is null) return NotFound();
            return View(new ModifyUserRolesViewModel
            {
                UserId = user.Id,
                UserRoles = await _userService.GetRolesAsync(user)
            });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromRole(ModifyUserRolesViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var user = await _userService.FindByIdAsync(viewModel.UserId);
            if (user is null) return NotFound();
            await _userService.RemoveFromRoleAsync(user, viewModel.Role);
            return RedirectToAction("All");
        }

        [HttpGet]
        [AllowAnonymous]
        JsonResult IsUsernameInUse(string UserName)
        {
            return Json(!_userService.Users
                .Any(u => string.Equals(u.UserName, UserName, StringComparison.CurrentCultureIgnoreCase)));
        }
    }
}