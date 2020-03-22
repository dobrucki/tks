using System;
using System.Collections.Generic;
using VMRent.DomainModel;

namespace VMRent.Repositories
{
    public interface IUserRoleRepository
    {
        UserRole Add(UserRole userRole);

        UserRole Get(string id);

        IEnumerable<UserRole> GetAll();

        IEnumerable<UserRole> GetAll(Func<UserRole, bool> predicate);

        void Update(UserRole userRole);

        void Delete(UserRole userRole);
    }
}