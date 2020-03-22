using VMRent.DomainModel;

namespace VMRent.Repositories
{
    public static class DeepCopyTools
    {
        public static Role DeepClone(this Role role)
        {
            return new Role
            {
                Id = role.Id,
                Name = role.Name,
                NormalizedName = role.NormalizedName
            };
        }
        
        public static User DeepClone(this User user)
        {
            return new User
            {
                Id = user.Id,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                NormalizedEmail = user.NormalizedEmail,
                NormalizedUserName = user.NormalizedUserName,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                Active = user.Active,
                UserType = user.UserType
            };
        }
        
        public static UserRole DeepClone(this UserRole userRole)
        {
            return new UserRole
            {
                Id = userRole.Id,
                User = userRole.User.DeepClone(),
                Role = userRole.Role.DeepClone()
            };
        }

        public static Vm DeepClone(this Vm vm)
        {
            return new Vm
            {
                Id = vm.Id,
                Name = vm.Name
            };
        }

        public static Vm DeepClone(this ExtendedVm vm)
        {
            return new ExtendedVm
            {
                Id = vm.Id,
                Name = vm.Name,
                Comment = vm.Comment
            };
        }
        
        public static UserVm DeepClone(this UserVm userVm)
        {
            return new UserVm
            {
                Id = userVm.Id,
                StartTime = userVm.StartTime,
                EndTime = userVm.EndTime,
                User = userVm.User?.DeepClone(),
                Vm = userVm.Vm?.DeepClone()
            };
        }
    }
}