using System;
using System.Collections.Generic;
using RepositoriesAdapters.Entities;

namespace RepositoriesAdapters.Repositories
{
    public interface IUserVmRepository
    {
        UserVmEnt Add(UserVmEnt userVm);

        UserVmEnt Get(string id);

        IEnumerable<UserVmEnt> GetAll();

        IEnumerable<UserVmEnt> GetAll(Func<UserVmEnt, bool> predicate);

        void Update(UserVmEnt userVm);
        
        void Delete(UserVmEnt userVm);
    }
}