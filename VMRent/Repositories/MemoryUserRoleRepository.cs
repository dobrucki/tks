using System;
using System.Collections.Generic;
using System.Linq;
using VMRent.Models;

namespace VMRent.Repositories
{
    public class MemoryUserRoleRepository : IUserRoleRepository
    {
        private readonly Dictionary<Guid, UserRole> _ctx = new Dictionary<Guid, UserRole>();

        public UserRole Add(UserRole userRole)
        {
            var id = Guid.NewGuid();
            var innerUserRole = userRole.DeepClone();
            innerUserRole.Id = id.ToString();
            _ctx.Add(id, innerUserRole);
            userRole.Id = id.ToString();
            return userRole;
        }

        public UserRole Get(string id)
        {
            try
            {
                var innerId = Guid.Parse(id);
                var innerUserRole = _ctx[innerId];
                var userRole = innerUserRole.DeepClone();
                userRole.Id = innerId.ToString();
                return userRole;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<UserRole> GetAll()
        {
            var all = _ctx.Values;
            var list = new List<UserRole>(_ctx.Count);
            list.AddRange(all.Select(userRole => userRole.DeepClone()));
            return list.AsEnumerable();
        }

        public IEnumerable<UserRole> GetAll(Func<UserRole, bool> predicate)
        {
            try
            {
                var all = _ctx.Values.Where(predicate);
                var list = new List<UserRole>();
                list.AddRange(all.Select(userRole => userRole.DeepClone()));
                return list.AsEnumerable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Update(UserRole userRole)
        {
            var innerUserRole = Get(userRole.Id);
            if (innerUserRole == null) return;
            var tmpUserRole = userRole.DeepClone();
            innerUserRole.User = tmpUserRole.User;
            innerUserRole.Role = tmpUserRole.Role;
        }

        public void Delete(UserRole userRole)
        {
            var innerRole = Get(userRole.Id);
            if (innerRole == null) return;
            try
            {
                _ctx.Remove(Guid.Parse(userRole.Id));
            }
            catch (Exception)
            {
                // Desired effect is achieved, item with that id does not exist in db.
                return;
            }
        }
    }
}