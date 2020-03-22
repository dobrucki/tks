using System;
using System.Collections.Generic;
using VMRent.Models;

namespace VMRent.Repositories
{
    public interface IUserVmRepository
    {
        UserVm Add(UserVm userVm);

        UserVm Get(string id);

        IEnumerable<UserVm> GetAll();

        IEnumerable<UserVm> GetAll(Func<UserVm, bool> predicate);

        void Update(UserVm userVm);
        
        void Delete(UserVm userVm);
    }
}