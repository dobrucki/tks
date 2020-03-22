using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VMRent.DomainModel;
using VMRent.Services;
using VMRent.ViewModels;

namespace VMRent.Controllers
{
    public class ReservationController : Controller
    {
        private readonly UserManager<User> _userService;

        private readonly ReservationService _reservationService;

        private readonly VmService _vmService;

        public ReservationController(UserManager<User> userService, 
            ReservationService reservationService, VmService vmService)
        {
            _userService = userService;
            _reservationService = reservationService;
            _vmService = vmService;
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
            var user = _userService.GetUserAsync(User).Result;
            var vm = _vmService.GetVmById(viewModel.VmId).Result;
            try
            {
                await _reservationService.CreateReservationAsync(user, vm, viewModel.StartTime, viewModel.EndTime);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.VmId = viewModel.VmId;
                return View(viewModel);
            }

            return RedirectToAction("Details", "Vm", new {id = vm.Id});
        }

        [HttpGet]
        public IActionResult CancelReservation([FromRoute] string id)
        {
            var userVm = _reservationService.GetReservationById(id);
            _reservationService.CancelReservationAsync(userVm.Result);
            return RedirectToAction("Details", "User", new {id = userVm.Result.User.Id});
        }
    }
}