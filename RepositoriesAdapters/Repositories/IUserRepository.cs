using System;
using System.Collections.Generic;
using RepositoriesAdapters.Entities;

namespace RepositoriesAdapters.Repositories
{
    public interface IUserRepository
    {
        UserEnt Add(UserEnt user);

        UserEnt Get(string id);

        IEnumerable<UserEnt> GetAll();

        IEnumerable<UserEnt> GetAll(Func<UserEnt, bool> predicate);

        void Update(UserEnt user);
    }
}