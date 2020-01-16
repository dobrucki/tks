using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VMRent.Models;

namespace VMRent.Stores
{
    public class UserStoreService : IUserStore<User>, IUserEmailStore<User>, IUserPhoneNumberStore<User>,
        IUserTwoFactorStore<User>, IUserPasswordStore<User>
    {
        public void Dispose()
        {
            // Nothing to dispose
        }

        #region IUserStore
        
        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public async Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        #endregion
        
        #region IUserPasswordStore

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        #endregion
        
    }
}