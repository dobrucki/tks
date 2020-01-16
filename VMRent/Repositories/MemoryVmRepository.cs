using System;
using System.Collections.Generic;
using System.Linq;
using VMRent.Models;

namespace VMRent.Repositories
{
    public class MemoryVmRepository : IVmRepository
    {
        private readonly Dictionary<Guid, Vm> _ctx = new Dictionary<Guid, Vm>();

        public Vm Add(Vm vm)
        {
            var id = Guid.NewGuid();
            var innerVm = vm.DeepClone();
            innerVm.Id = id.ToString();
            _ctx.Add(id, innerVm);
            vm.Id = id.ToString();
            return vm;
        }

        public Vm Get(string id)
        {
            try
            {
                var innerId = Guid.Parse(id);
                var innerVm = _ctx[innerId];
                var vm = innerVm.DeepClone();
                vm.Id = innerId.ToString();
                return vm;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Vm> GetAll()
        {
            var all = _ctx.Values;
            var list = new List<Vm>(_ctx.Count);
            list.AddRange(all.Select(vm => vm.DeepClone()));
            return list.AsEnumerable();
        }

        public IEnumerable<Vm> GetAll(Func<Vm, bool> predicate)
        {
            try
            {
                var all = _ctx.Values.Where(predicate);
                var list = new List<Vm>();
                list.AddRange(all.Select(vm => vm.DeepClone()));
                return list.AsEnumerable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Update(Vm vm)
        {
            var innerVm = Get(vm.Id);
            if (innerVm == null) return;
            var tmpVm = vm.DeepClone();
        }

        public void Delete(Vm vm)
        {
            var innerVm = Get(vm.Id);
            if (innerVm == null) return;
            try
            {
                _ctx.Remove(Guid.Parse(vm.Id));
            }
            catch (Exception)
            {
                // Desired effect is achieved, item with that id does not exist in db.
                return;
            }
        }
    }
}