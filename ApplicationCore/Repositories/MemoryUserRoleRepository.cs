using System;
using System.Collections.Generic;
using System.Linq;
using VMRent.Models;

namespace VMRent.Repositories
{
    public class MemoryUserRoleRepository : IUserRoleRepository
    {
        private readonly Dictionary<Guid, UserRole> _ctx = new Dictionary<Guid, UserRole>();

        public MemoryUserRoleRepository()
        {
            _ctx.Add(Guid.Parse("b0307dfe-0501-4ec2-a8f2-a9e72726b367"), new UserRole
            {
                Id = "b0307dfe-0501-4ec2-a8f2-a9e72726b367",
                Role = new Role
                {
                    Id = "c2a365aa-3407-4ebb-859b-e481326a06d4",
                    Name = "Administrator",
                    NormalizedName = "Administrator".ToUpper()
                },
                User = new User
                {
                    Id = "782b8a8c-60f7-46f1-9641-44b6e2f36ee9",
                    Email = "user@example.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "user@example.com".ToUpper(),
                    UserName = "user",
                    NormalizedUserName = "USER",
                    PasswordHash = "AQAAAAEAACcQAAAAELchcUyDbMj+/SMAB1IOBEijR4b4UoHGpJTK8A7qokIVX4uHE0Jmjwypltx/sdyn5w==",
                    PhoneNumber = "123123123",
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    LockoutEnd = null,
                    Active = true
                }
            });

            _ctx.Add(Guid.Parse("b8c3a7c3-8506-4804-af6a-90f701974881"), new UserRole
            {
                Id = "b8c3a7c3-8506-4804-af6a-90f701974881",
                Role =  new Role
                {
                    Id = "53c3a39d-0ee5-431c-aa8d-d11be2685a44",
                    Name = "Employee",
                    NormalizedName = "Administrator".ToUpper()
                },
                User = new User
                {
                    Id = "782b8a8c-60f7-46f1-9641-44b6e2f36ee9",
                    Email = "user@example.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "user@example.com".ToUpper(),
                    UserName = "user",
                    NormalizedUserName = "USER",
                    PasswordHash = "AQAAAAEAACcQAAAAELchcUyDbMj+/SMAB1IOBEijR4b4UoHGpJTK8A7qokIVX4uHE0Jmjwypltx/sdyn5w==",
                    PhoneNumber = "123123123",
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    LockoutEnd = null,
                    Active = true
                }
            });
        }

        public UserRole Add(UserRole userRole)
        {
            var id = Guid.NewGuid();
            var innerUserRole = userRole.DeepClone();
            innerUserRole.Id = id.ToString();
            _ctx.Add(id, innerUserRole);
            userRole.Id = id.ToString();
            return userRole;
        }

        public UserRole Get(string id)
        {
            try
            {
                var innerId = Guid.Parse(id);
                var innerUserRole = _ctx[innerId];
                var userRole = innerUserRole.DeepClone();
                userRole.Id = innerId.ToString();
                return userRole;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<UserRole> GetAll()
        {
            var all = _ctx.Values;
            var list = new List<UserRole>(_ctx.Count);
            list.AddRange(all.Select(userRole => userRole.DeepClone()));
            return list.AsEnumerable();
        }

        public IEnumerable<UserRole> GetAll(Func<UserRole, bool> predicate)
        {
            try
            {
                var all = _ctx.Values.Where(predicate);
                var list = new List<UserRole>();
                list.AddRange(all.Select(userRole => userRole.DeepClone()));
                return list.AsEnumerable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Update(UserRole userRole)
        {
            var innerUserRole = _ctx[Guid.Parse(userRole.Id)];
            if (innerUserRole == null) return;
            var tmpUserRole = userRole.DeepClone();
            innerUserRole.User = tmpUserRole.User;
            innerUserRole.Role = tmpUserRole.Role;
        }

        public void Delete(UserRole userRole)
        {
            var innerRole = _ctx[Guid.Parse(userRole.Id)];
            if (innerRole == null) return;
            try
            {
                _ctx.Remove(Guid.Parse(userRole.Id));
            }
            catch (Exception)
            {
                // Desired effect is achieved, item with that id does not exist in db.
                return;
            }
        }
    }
}