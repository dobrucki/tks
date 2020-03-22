using System;
using System.Collections.Generic;
using System.Linq;
using VMRent.Models;

namespace VMRent.Repositories
{
    public class MemoryVmRepository : IVmRepository
    {
        private readonly Dictionary<Guid, Vm> _ctx = new Dictionary<Guid, Vm>();

        public MemoryVmRepository()
        {
            var vm = new Vm
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Machine 1"
            };
            var exVm = new ExtendedVm
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Machine 2",
                Comment = "Sample text"
            };
            _ctx.Add(Guid.Parse(vm.Id), vm);
            _ctx.Add(Guid.Parse(exVm.Id), exVm);
        }

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
            var innerVm = _ctx[Guid.Parse(vm.Id)];
            if (innerVm == null) return;
            innerVm.Name = vm.Name;
            if (innerVm.GetType() == typeof(ExtendedVm))
            {
                ((ExtendedVm) innerVm).Comment = ((ExtendedVm) vm).Comment;
            } 
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