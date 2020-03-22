using System;
using System.Collections.Generic;
using System.Linq;
using RepositoriesAdapters.Repositories;
using RepositoriesAdapters.Entities;

namespace RepositoriesAdapters.Repositories
{
    public class MemoryUserVmRepository : IUserVmRepository
    {
        private readonly Dictionary<Guid, UserVmEnt> _ctx = new Dictionary<Guid, UserVmEnt>();

        public UserVmEnt Add(UserVmEnt userVm)
        {
            var id = Guid.NewGuid();
            var innerUserVm = userVm.DeepClone();
            innerUserVm.Id = id.ToString();
            _ctx.Add(id, innerUserVm);
            userVm.Id = id.ToString();
            return userVm;
        }

        public UserVmEnt Get(string id)
        {
            try
            {
                var innerId = Guid.Parse(id);
                var innerUserVm = _ctx[innerId];
                var userVm = innerUserVm.DeepClone();
                userVm.Id = innerId.ToString();
                return userVm;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<UserVmEnt> GetAll()
        {
            var all = _ctx.Values;
            var list = new List<UserVmEnt>(_ctx.Count);
            list.AddRange(all.Select(userVm => userVm.DeepClone()));
            return list.AsEnumerable();
        }

        public IEnumerable<UserVmEnt> GetAll(Func<UserVmEnt, bool> predicate)
        {
            try
            {
                var all = _ctx.Values.Where(predicate);
                var list = new List<UserVmEnt>();
                list.AddRange(all.Select(userVm => userVm.DeepClone()));
                return list.AsEnumerable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Update(UserVmEnt userVm)
        {
            var innerUserVm = _ctx[Guid.Parse(userVm.Id)];
            if (innerUserVm == null) return;
            innerUserVm.User = userVm.User?.DeepClone();
            innerUserVm.VmEnt = userVm.VmEnt?.DeepClone();
        }

        public void Delete(UserVmEnt userVm)
        {
            var innerVm = Get(userVm.Id);
            if (innerVm == null) return;
            try
            {
                _ctx.Remove(Guid.Parse(userVm.Id));
            }
            catch (Exception)
            {
                // Desired effect is achieved, item with that id does not exist in db.
                return;
            }
        }
    }
}