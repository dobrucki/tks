using System;
using System.Collections.Generic;
using VMRent.Models;

namespace VMRent.Repositories
{
    public interface IUserRepository
    {
        User Add(User role);

        User Get(string id);

        IEnumerable<User> GetAll();

        IEnumerable<User> GetAll(Func<User, bool> predicate);

        void Update(User role);
    }
}