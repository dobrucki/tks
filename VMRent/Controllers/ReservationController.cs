using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VMRent.Managers;
using VMRent.Models;
using VMRent.ViewModels;

namespace VMRent.Controllers
{
    public class ReservationController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly ReservationManager _reservationManager;

        private readonly VmManager _vmManager;

        public ReservationController(UserManager<User> userManager, 
            ReservationManager reservationManager, VmManager vmManager)
        {
            _userManager = userManager;
            _reservationManager = reservationManager;
            _vmManager = vmManager;
        }

        [HttpGet]
        public IActionResult CreateReservation([FromRoute] string id)
        {
            ViewBag.VmId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationViewModel viewModel)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var vm = _vmManager.GetVmById(viewModel.VmId).Result;
            try
            {
                await _reservationManager.CreateReservationAsync(user, vm, viewModel.StartTime, viewModel.EndTime);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("", e.Message);
                return View(viewModel);
            }

            return RedirectToAction("Details", "User", new {id = user.Id});
        }

        [HttpGet]
        public IActionResult CancelReservation([FromRoute] string id)
        {
            var userVm = _reservationManager.GetReservationById(id);
            _reservationManager.CancelReservationAsync(userVm.Result);
            return RedirectToAction("Details", "User", new {id = userVm.Result.User.Id});
        }
    }
}