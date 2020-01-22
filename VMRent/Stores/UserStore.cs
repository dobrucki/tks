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
        IUserPasswordStore<User>, IUserRoleStore<User>, IQueryableUserStore<User>
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
            var u = _userRepository.Add(user);
            user.Id = u.Id;
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
            return user.NormalizedUserName;
        }

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return user.Id;
        }

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return user.UserName;
        }

        public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
        }

        public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
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
            bool Predicate(User user) =>
                user.NormalizedEmail.Equals(normalizedEmail);
            return _userRepository.GetAll(Predicate).FirstOrDefault();
        }

        public async Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return user.Email;
        }

        public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return user.EmailConfirmed;
        }

        public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return user.NormalizedEmail;
        }

        public async Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
        }

        public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
        }

        public async Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
        }
        
        #endregion

        #region IUserPhoneNumberStore
    
        public async Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            return _userRepository.Get(user.Id).PhoneNumber;
        }

        public async Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return _userRepository.Get(user.Id).PhoneNumberConfirmed;
        }

        public async Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            _userRepository.Update(user);
        }

        public async Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            _userRepository.Update(user);
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

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        #endregion
        
        #region IUserRoleStore
        
        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            //var role = await _roleStore.FindByNameAsync(roleName.Normalize(), cancellationToken);
            var role = _roleRepository
                .GetAll(r => string.Equals(r.Name.ToUpper(), roleName.ToUpper())).FirstOrDefault();
            if (role == null)
            {
                throw new InvalidOperationException("Role not found");
            }

            var userRole = new UserRole
            {
                Role = role,
                User = user
            };
            _userRoleRepository.Add(userRole);
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
//            bool Predicate(UserRole userRole) =>
//                userRole.User.Id.Equals(user.Id);
//            return _userRoleRepository.GetAll(Predicate).Select(userRole => userRole.Role.Name).ToList();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var roles = _userRoleRepository
                .GetAll(userRole => userRole.User.Id.Equals(user.Id))
                .Select(userRole => userRole.Role.Name).ToList();
            return roles;
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }
            
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
        
        #region IUserLockoutStore

        public async Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            return 0;
        }

        public async Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken)
        {
            return user.LockoutEnabled;
        }

        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken)
        {
            return user.LockoutEnd;
        }

        public async Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            return 0;
        }

        public async Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            user.LockoutEnabled = enabled;
        }

        public async Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            user.LockoutEnd = lockoutEnd;
        }
        
        #endregion

        #region IQueryableUserStore

        public IQueryable<User> Users => _userRepository.GetAll().ToList().AsQueryable();

        #endregion
    }
}