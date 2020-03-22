using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMRent.DomainModel;
using VMRent.Repositories;

namespace VMRent.Services
{
    public class VmService
    {
        private readonly IVmRepository _vmRepository;

        private readonly IUserVmRepository _userVmRepository;

        public VmService(IVmRepository vmRepository, IUserVmRepository userVmRepository)
        {
            _vmRepository = vmRepository;
            _userVmRepository = userVmRepository;
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

            var userVms = _userVmRepository.GetAll(i => i.Vm.Id.Equals(vm.Id));
            foreach (var userVm in userVms)
            {
                userVm.Vm = null;
                _userVmRepository.Update(userVm);
            }
            _vmRepository.Delete(vm);
            return Task.CompletedTask;
        }

        public Task UpdateVm(Vm vm)
        {
            if (vm is null) throw new ArgumentNullException(nameof(vm));

            if (_vmRepository
                .GetAll(i => string.Equals(vm.Name, i.Name) && vm.Id != i.Id)
                .Any())
            {
                throw new ArgumentException($"Machine with name {vm.Name} already exists");
            }

            var userVms = _userVmRepository.GetAll(i => i.Vm.Id.Equals(vm.Id));
            foreach (var userVm in userVms)
            {
                userVm.Vm = vm;
                _userVmRepository.Update(userVm);
            }
            _vmRepository.Update(vm);
            return Task.CompletedTask;
        }
    }
}