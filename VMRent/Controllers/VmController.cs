using Microsoft.AspNetCore.Mvc;
using VMRent.Managers;
using VMRent.ViewModels;

namespace VMRent.Controllers
{
    public class VmController : Controller
    {
        private readonly VmManager _vmManager;

        private readonly ReservationManager _reservationManager;

        public VmController(VmManager vmManager, ReservationManager reservationManager)
        {
            _vmManager = vmManager;
            _reservationManager = reservationManager;
        }

        [HttpGet]
        public IActionResult All()
        {
            return View(new ListVmViewModel
            {
                Vms = _vmManager.ListAllVmsAsync().Result
            });
        }

        [HttpGet]
        public IActionResult Details([FromRoute] string id)
        {
            var vm = _vmManager.GetVmById(id).Result;
            return View(new DetailVmViewModel{
                UserVms = _reservationManager.GetReservationsForVmAsync(vm).Result
            });
        }
    }
}