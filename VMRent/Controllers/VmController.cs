using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VMRent.Managers;
using VMRent.Models;
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateVmViewModel viewModel)
        {
            try
            {
                if (viewModel.Type == "Vm")
                {
                    _vmManager.CreateVmAsync(viewModel.Name);
                }
                else
                {
                    _vmManager.CreateExtendedVmAsync(viewModel.Name, viewModel.Comment);
                }
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("", e.Message);
                return View(viewModel);
            }
            
            return RedirectToAction("All", "Vm");
        }

        [HttpGet]
        public IActionResult Details([FromRoute] string id)
        {
            var vm = _vmManager.GetVmById(id).Result;
            return View(new DetailVmViewModel{
                UserVms = _reservationManager.GetReservationsForVmAsync(vm).Result
            });
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] string id)
        {
            var vm = _vmManager.GetVmById(id).Result;
            return View(new EditVmViewModel
            {
                Id = vm.Id,
                Name = vm.Name,
                Comment = (vm as ExtendedVm)?.Comment
            });
        }

        [HttpPost]
        public IActionResult Edit(EditVmViewModel viewModel)
        {
            var vm = _vmManager.GetVmById(viewModel.Id).Result;
            vm.Name = viewModel.Name;
            if (vm.GetType() == typeof(ExtendedVm))
            {
                ((ExtendedVm) vm).Comment = viewModel.Comment;
            }

            try
            {
                _vmManager.UpdateVm(vm);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("", e.Message);
                return View(viewModel);
            }

            return RedirectToAction("All", "Vm");
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] string id)
        {
            var vm = _vmManager.GetVmById(id).Result;
            _vmManager.DeleteVm(vm);
            return RedirectToAction("All", "Vm");
        }
    }
}