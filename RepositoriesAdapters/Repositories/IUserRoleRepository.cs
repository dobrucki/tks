using System;
using System.Collections.Generic;
using RepositoriesAdapters.Entities;

namespace RepositoriesAdapters.Repositories
{
    public interface IUserRoleRepository
    {
        UserRoleEnt Add(UserRoleEnt userRole);

        UserRoleEnt Get(string id);

        IEnumerable<UserRoleEnt> GetAll();

        IEnumerable<UserRoleEnt> GetAll(Func<UserRoleEnt, bool> predicate);

        void Update(UserRoleEnt userRole);

        void Delete(UserRoleEnt userRole);
    }
}