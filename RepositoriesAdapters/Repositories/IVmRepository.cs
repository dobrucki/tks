using System;
using System.Collections.Generic;
using RepositoriesAdapters.Entities;

namespace RepositoriesAdapters.Repositories
{
    public interface IVmRepository
    {
        VmEnt Add(VmEnt vm);

        VmEnt Get(string id);

        IEnumerable<VmEnt> GetAll();

        IEnumerable<VmEnt> GetAll(Func<VmEnt, bool> predicate);

        void Update(VmEnt vm);

        void Delete(VmEnt vm);
    }
}