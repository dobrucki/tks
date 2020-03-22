using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using RepositoriesAdapters.Entities;

namespace RepositoriesAdapters.Repositories
{
    public class MemoryRoleRepository : IRoleRepository
    {
        private readonly Dictionary<Guid, RoleEnt> _ctx = new Dictionary<Guid, RoleEnt>();

        public MemoryRoleRepository()
        {
            _ctx.Add(Guid.Parse("c2a365aa-3407-4ebb-859b-e481326a06d4"), new RoleEnt
            {
                Id = "c2a365aa-3407-4ebb-859b-e481326a06d4",
                Name = "Administrator",
                NormalizedName = "Administrator".ToUpper()
            });
            _ctx.Add(Guid.Parse("53c3a39d-0ee5-431c-aa8d-d11be2685a44"), new RoleEnt
            {
                Id = "53c3a39d-0ee5-431c-aa8d-d11be2685a44",
                Name = "Employee",
                NormalizedName = "Administrator".ToUpper()
            });
            _ctx.Add(Guid.Parse("cf36325f-3e68-4ffa-bd60-cae4d82a2e64"), new RoleEnt
            {
                Id = "cf36325f-3e68-4ffa-bd60-cae4d82a2e64",
                Name = "Customer",
                NormalizedName = "Administrator".ToUpper()
            });
        }

        public RoleEnt Add(RoleEnt role)
        {
            var id = Guid.NewGuid();
            var innerRole = role.DeepClone();
            innerRole.Id = id.ToString();
            _ctx.Add(id, innerRole);
            role.Id = id.ToString();
            return role;
        }

        public RoleEnt Get(string id)
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

        public IEnumerable<RoleEnt> GetAll()
        {
            var all = _ctx.Values;
            var list = new List<RoleEnt>(_ctx.Count);
            list.AddRange(all.Select(role => role.DeepClone()));
            return list.AsEnumerable();
        }

        public IEnumerable<RoleEnt> GetAll(Func<RoleEnt, bool> predicate)
        {
            try
            {
                var all = _ctx.Values.Where(predicate);
                var list = new List<RoleEnt>();
                list.AddRange(all.Select(role => role.DeepClone()));
                return list.AsEnumerable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Update(RoleEnt role)
        {
            var innerRole = _ctx[Guid.Parse(role.Id)];
            if (innerRole == null) return;
            innerRole.Name = role.Name;
            innerRole.NormalizedName = role.NormalizedName;
        }

        public void Delete(RoleEnt role)
        {
            var innerRole = _ctx[Guid.Parse(role.Id)];
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