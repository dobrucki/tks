using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using VMRent.DomainModel;

namespace VMRent.Repositories
{
    public class MemoryRoleRepository : IRoleRepository
    {
        private readonly Dictionary<Guid, Role> _ctx = new Dictionary<Guid, Role>();

        public MemoryRoleRepository()
        {
            _ctx.Add(Guid.Parse("c2a365aa-3407-4ebb-859b-e481326a06d4"), new Role
            {
                Id = "c2a365aa-3407-4ebb-859b-e481326a06d4",
                Name = "Administrator",
                NormalizedName = "Administrator".ToUpper()
            });
            _ctx.Add(Guid.Parse("53c3a39d-0ee5-431c-aa8d-d11be2685a44"), new Role
            {
                Id = "53c3a39d-0ee5-431c-aa8d-d11be2685a44",
                Name = "Employee",
                NormalizedName = "Administrator".ToUpper()
            });
            _ctx.Add(Guid.Parse("cf36325f-3e68-4ffa-bd60-cae4d82a2e64"), new Role
            {
                Id = "cf36325f-3e68-4ffa-bd60-cae4d82a2e64",
                Name = "Customer",
                NormalizedName = "Administrator".ToUpper()
            });
        }

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
            var innerRole = _ctx[Guid.Parse(role.Id)];
            if (innerRole == null) return;
            innerRole.Name = role.Name;
            innerRole.NormalizedName = role.NormalizedName;
        }

        public void Delete(Role role)
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