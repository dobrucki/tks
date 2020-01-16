using System;
using System.Collections.Generic;
using VMRent.Models;

namespace VMRent.Repositories
{
    public interface IVmRepository
    {
        Vm Add(Vm vm);

        Vm Get(string id);

        IEnumerable<Vm> GetAll();

        IEnumerable<Vm> GetAll(Func<Vm, bool> predicate);

        void Update(Vm vm);

        void Delete(Vm vm);
    }
}