using System;
using System.Collections.Generic;
using System.Linq;
using VMRent.Models;

namespace VMRent.Repositories
{
    public class MemoryUserRepository : IUserRepository
    {
        private readonly Dictionary<Guid, User> _ctx = new Dictionary<Guid, User>();

        public MemoryUserRepository()
        {
            var user = new User
            {
                Email = "user@example.com",
                EmailConfirmed = true,
                NormalizedEmail = "user@example.com".ToUpper(),
                UserName = "user",
                NormalizedUserName = "USER",
                PasswordHash = "AQAAAAEAACcQAAAAELchcUyDbMj+/SMAB1IOBEijR4b4UoHGpJTK8A7qokIVX4uHE0Jmjwypltx/sdyn5w==",
                PhoneNumber = "123123123",
                Id = Guid.NewGuid().ToString(),
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                LockoutEnd = null
            };
            Add(user);
        }

        public User Add(User user)
        {
            var id = Guid.NewGuid();
            var innerUser = user.DeepClone();
            innerUser.Id = id.ToString();
            _ctx.Add(id, innerUser);
            user.Id = id.ToString();
            return user;
        }

        public User Get(string id)
        {
            try
            {
                var innerId = Guid.Parse(id);
                var innerUser = _ctx[innerId];
                var user = innerUser.DeepClone();
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<User> GetAll()
        {
            var all = _ctx.Values;
            var list = new List<User>(_ctx.Count);
            list.AddRange(all.Select(user => user.DeepClone()));
            return list.AsEnumerable();
        }

        public IEnumerable<User> GetAll(Func<User, bool> predicate)
        {
            try
            {
                var all = _ctx.Values.Where(predicate);
                var list = new List<User>();
                list.AddRange(all.Select(user => user.DeepClone()));
                return list.AsEnumerable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Update(User user)
        {
            var innerUser = Get(user.Id);
            if (innerUser == null) return;
            innerUser.Email = user.Email;
            innerUser.EmailConfirmed = user.EmailConfirmed;
            innerUser.NormalizedEmail = user.NormalizedEmail;
            innerUser.NormalizedUserName = user.NormalizedUserName;
            innerUser.PasswordHash = user.PasswordHash;
            innerUser.PhoneNumber = user.PhoneNumber;
            innerUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            innerUser.TwoFactorEnabled = user.TwoFactorEnabled;
            innerUser.UserName = user.UserName;
            innerUser.LockoutEnabled = user.LockoutEnabled;
            innerUser.LockoutEnd = user.LockoutEnd;
        }
    }
}
