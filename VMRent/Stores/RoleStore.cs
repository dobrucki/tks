using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VMRent.Models;
using VMRent.Repositories;

namespace VMRent.Stores
{
    public class RoleStore : IRoleStore<Role>, IQueryableRoleStore<Role>
    {
        private IRoleRepository _roleRepository;

        public RoleStore(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public void Dispose()
        {
            // Nothing to dispose
        }

        #region IRoleStore

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region IQueryableRoleStore

        public IQueryable<Role> Roles => _roleRepository.GetAll().AsQueryable();

        #endregion
    }
}