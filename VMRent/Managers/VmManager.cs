using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMRent.Models;
using VMRent.Repositories;

namespace VMRent.Managers
{
    public class VmManager
    {
        private readonly IVmRepository _vmRepository;

        public VmManager(IVmRepository vmRepository)
        {
            _vmRepository = vmRepository;
        }

        public Task<List<Vm>> ListAllVmsAsync()
        {
            return Task.FromResult(_vmRepository.GetAll().ToList());
        }

        public Task<Vm> CreateVmAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            if (_vmRepository
                .GetAll(i => i.Name.ToUpper().Equals(name.ToUpper())).Any())
                throw new ArgumentException($"Vm with name {name} already exists");
            
            var vm = _vmRepository.Add(new Vm
            {
                Name = name
            });
            return Task.FromResult(vm);
        }

        public Task<Vm> GetVmById(string id)
        {
            return Task.FromResult(_vmRepository.GetAll(vm => vm.Id.Equals(id)).FirstOrDefault());
        }

        public Task<Vm> CreateExtendedVmAsync(string name, string comment)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(comment)) throw new ArgumentNullException(nameof(comment));
            
            if (_vmRepository
                .GetAll(i => i.Name.ToUpper().Equals(name.ToUpper())).Any())
                throw new ArgumentException($"Vm with name {name} already exists");

            var vm = _vmRepository.Add(new ExtendedVm
            {
                Name = name,
                Comment = comment
            });
            return Task.FromResult(vm);
        }

        public Task DeleteVm(Vm vm)
        {
            if (vm is null) throw new ArgumentNullException(nameof(vm));
            _vmRepository.Delete(vm);
            return Task.CompletedTask;
        }
    }
}