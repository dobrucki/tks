using RepositoriesAdapters.Entities;

namespace RepositoriesAdapters.Repositories
{
    public static class DeepCopyTools
    {
        public static RoleEnt DeepClone(this RoleEnt role)
        {
            return new RoleEnt
            {
                Id = role.Id,
                Name = role.Name,
                NormalizedName = role.NormalizedName
            };
        }
        
        public static UserEnt DeepClone(this UserEnt user)
        {
            return new UserEnt
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
        
        public static UserRoleEnt DeepClone(this UserRoleEnt userRoleEnt)
        {
            return new UserRoleEnt
            {
                Id = userRoleEnt.Id,
                UserEnt = userRoleEnt.UserEnt.DeepClone(),
                RoleEnt = userRoleEnt.RoleEnt.DeepClone()
            };
        }

        public static VmEnt DeepClone(this VmEnt vm)
        {
            return new VmEnt
            {
                Id = vm.Id,
                Name = vm.Name
            };
        }

        public static VmEnt DeepClone(this ExtendedVmEnt vm)
        {
            return new ExtendedVmEnt
            {
                Id = vm.Id,
                Name = vm.Name,
                Comment = vm.Comment
            };
        }
        
        public static UserVmEnt DeepClone(this UserVmEnt userVm)
        {
            return new UserVmEnt
            {
                Id = userVm.Id,
                StartTime = userVm.StartTime,
                EndTime = userVm.EndTime,
                User = userVm.User?.DeepClone(),
                VmEnt = userVm.VmEnt?.DeepClone()
            };
        }
    }
}