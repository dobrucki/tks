using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VMRent.Managers;
using VMRent.Models;
using VMRent.ViewModels;

namespace VMRent.Controllers
{
    [Route("api/vm")]
    [AllowAnonymous]
    public class RestVmController : Controller
    {
        private readonly VmManager _vmManager;

        public RestVmController(VmManager vmManager)
        {
            _vmManager = vmManager;
        }

        // GET: api/vm/
        // GET: api/vm?name=machine
        [HttpGet]
        public async Task<IActionResult> Search(string name)
        {
            var vms = await _vmManager.ListAllVmsAsync();
            if (vms is null) return NotFound();
            if (name != null)
            {
                vms = vms.Where(vm => vm.Name.Contains(name)).ToList();
            }
            return Json(vms);
        }

        // GET: api/vm/c5f41682-de9a-4532-83fb-081aa338bdb8
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var vm = await _vmManager.GetVmById(id);
            if (vm is null) return NotFound();
            return Json(vm);
        }

        // POST: api/vm/
        [HttpPost]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Post(CreateVmViewModel viewModel)
        {
            try
            {
                Vm vm;
                if (viewModel.Type.Equals("ExtendedVm"))
                {
                    vm = await _vmManager.CreateExtendedVmAsync(viewModel.Name, viewModel.Comment);
                }
                else
                {
                    vm = await _vmManager.CreateVmAsync(viewModel.Name);
                }

                return CreatedAtAction("Get", new {id = vm.Id}, vm);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("error", e.Message);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/vm/c5f41682-de9a-4532-83fb-081aa338bdb8
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Put(string id, EditVmViewModel viewModel)
        {
            var vm = await _vmManager.GetVmById(id);
            
            if (vm is null)
            {
                return NotFound();
            }

            vm.Name = viewModel.Name;
            if (vm.GetType() == typeof(ExtendedVm))
            {
                ((ExtendedVm) vm).Comment = viewModel.Comment;
            }

            try
            {
                await _vmManager.UpdateVm(vm);
                return NoContent();
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("error", e.Message);
            }

            return BadRequest(ModelState);
        }
        
        // DELETE: api/vm/c5f41682-de9a-4532-83fb-081aa338bdb8
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Delete(string id)
        {
            var vm = await _vmManager.GetVmById(id);
            if (vm is null)
            {
                return NotFound();
            }

            try
            {
                await _vmManager.DeleteVm(vm);
                return Json(vm);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("error", e.Message);
            }

            return BadRequest(ModelState);
        }
    }
}