using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VMRent.Models;
using VMRent.Repositories;

namespace VMRent.Stores
{
    public class UserStore: IUserStore<User>, IUserEmailStore<User>, IUserPhoneNumberStore<User>,
        IUserTwoFactorStore<User>, IUserPasswordStore<User>, IUserRoleStore<User>
    {
        private readonly IUserRepository _userRepository;

        private readonly IRoleRepository _roleRepository;

        private readonly IUserRoleRepository _userRoleRepository;

        public UserStore(IUserRoleRepository userRoleRepository, IUserRepository userRepository, 
            IRoleRepository roleRepository)
        {
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        
        public void Dispose()
        {
            // Nothing to dispose
        }

        #region IUserStore
        
        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _userRepository.Add(user);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            return IdentityResult.Failed();
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _userRepository.Get(userId);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _userRepository.GetAll(u => u.NormalizedUserName == normalizedUserName).FirstOrDefault();
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return _userRepository.Get(user.Id).NormalizedUserName;
        }

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return _userRepository.Get(user.Id).Id;
        }

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return _userRepository.Get(user.Id).UserName;
        }

        public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            var uUser = _userRepository.Get(user.Id);
            uUser.NormalizedUserName = normalizedName;
            _userRepository.Update(uUser);
        }

        public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            var uUser = _userRepository.Get(user.Id);
            uUser.UserName = userName;
            _userRepository.Update(uUser);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _userRepository.Update(user);
            return IdentityResult.Success;
        }
        
        #endregion

        #region IUserEmailStore
        
        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
        
        #endregion

        #region IUserPhoneNumberStore
    
        public async Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region IUserTwoFactorStore

        public async Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken)
        {
            var copy = _userRepository.Get(user.Id);
            return copy.TwoFactorEnabled;
        }

        public async Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            var copy = _userRepository.Get(user.Id);
            copy.TwoFactorEnabled = enabled;
            _userRepository.Update(copy);
        }

        #endregion
        
        #region IUserPasswordStore

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            var rUser = _userRepository.Get(user.Id);
            return rUser.PasswordHash;
        }

        public async Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return !string.IsNullOrEmpty(_userRepository.Get(user.Id).PasswordHash);
        }

        public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            var copy = _userRepository.Get(user.Id);
            copy.PasswordHash = passwordHash;
            _userRepository.Update(copy);
        }

        #endregion
        
        #region IUserRoleStore
        
        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            bool Predicate(Role r) => 
                string.Equals(r.Name, roleName, StringComparison.CurrentCultureIgnoreCase);
            var rRole = _roleRepository.GetAll(Predicate).FirstOrDefault();
            var rUser = _userRepository.Get(user.Id);
            if (rRole != null && rUser != null)
            {
                var userRole = new UserRole
                {
                    User = rUser,
                    Role = rRole
                };
                _userRoleRepository.Add(userRole);
            }
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            bool Predicate(UserRole userRole) =>
                userRole.User.Id.Equals(user.Id);
            return _userRoleRepository.GetAll(Predicate).Select(userRole => userRole.Role.Name).ToList();
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            bool Predicate(UserRole userRole) =>
                userRole.Role.Name.ToUpper().Equals(roleName.ToUpper());
            return _userRoleRepository.GetAll(Predicate).Select(userRole => userRole.User).ToList();
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            bool Predicate(UserRole userRole) =>
                userRole.User.Id.Equals(user.Id);
            return _userRoleRepository
                .GetAll(Predicate)
                .Any(userRole => userRole.Role.Name.ToUpper().Equals(roleName.ToUpper()));
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            bool Predicate(UserRole userRole1) =>
                userRole1.User.Id.Equals(user.Id);
            var userRole = _userRoleRepository.GetAll(Predicate)
                .FirstOrDefault(userRole2 => userRole2.Role.Name.ToUpper().Equals(roleName.ToUpper()));
            _userRoleRepository.Delete(userRole);
        }   
        
        #endregion
    }
}