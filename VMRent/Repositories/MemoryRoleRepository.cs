using System;
using System.Collections.Generic;
using System.Linq;
using VMRent.Models;

namespace VMRent.Repositories
{
    public class MemoryRoleRepository : IRoleRepository
    {
        private readonly Dictionary<Guid, Role> _ctx = new Dictionary<Guid, Role>();

        public Role Add(Role role)
        {
            var id = Guid.NewGuid();
            var innerRole = role.DeepClone();
            innerRole.Id = id.ToString();
            _ctx.Add(id, innerRole);
            role.Id = id.ToString();
            return role;
        }

        public Role Get(string id)
        {
            try
            {
                var innerId = Guid.Parse(id);
                var innerRole = _ctx[innerId];
                var role = innerRole.DeepClone();
                return role;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Role> GetAll()
        {
            var all = _ctx.Values;
            var list = new List<Role>(_ctx.Count);
            list.AddRange(all.Select(role => role.DeepClone()));
            return list.AsEnumerable();
        }

        public IEnumerable<Role> GetAll(Func<Role, bool> predicate)
        {
            try
            {
                var all = _ctx.Values.Where(predicate);
                var list = new List<Role>();
                list.AddRange(all.Select(role => role.DeepClone()));
                return list.AsEnumerable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Update(Role role)
        {
            var innerRole = Get(role.Id);
            if (innerRole == null) return;
            innerRole.Name = role.Name;
            innerRole.NormalizedName = role.NormalizedName;
        }

        public void Delete(Role role)
        {
            var innerRole = Get(role.Id);
            if (innerRole == null) return;
            try
            {
                _ctx.Remove(Guid.Parse(role.Id));
            }
            catch (Exception)
            {
                // Desired effect is achieved, item with that id does not exist in db.
                return;
            }
        }
    }
}