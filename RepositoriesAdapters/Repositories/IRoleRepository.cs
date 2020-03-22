using System;
using System.Collections.Generic;
using RepositoriesAdapters.Entities;

namespace RepositoriesAdapters.Repositories
{
    public interface IRoleRepository
    {
        RoleEnt Add(RoleEnt role);

        RoleEnt Get(string id);

        IEnumerable<RoleEnt> GetAll();

        IEnumerable<RoleEnt> GetAll(Func<RoleEnt, bool> predicate);

        void Update(RoleEnt role);

        void Delete(RoleEnt role);
    }
}