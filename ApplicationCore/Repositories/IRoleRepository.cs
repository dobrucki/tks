using System;
using System.Collections.Generic;
using VMRent.Models;

namespace VMRent.Repositories
{
    public interface IRoleRepository
    {
        Role Add(Role role);

        Role Get(string id);

        IEnumerable<Role> GetAll();

        IEnumerable<Role> GetAll(Func<Role, bool> predicate);

        void Update(Role role);

        void Delete(Role role);
    }
}