using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VMRent.Models
{
    public class User
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        
        public string NormalizedUserName { get; set; }
        
        public string Email { get; set; }
        
        public string NormalizedEmail { get; set; }
        
        public bool EmailConfirmed { get; set; }
        
        public string PasswordHash { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public bool PhoneNumberConfirmed { get; set; }
        
        public bool TwoFactorEnabled { get; set; }
        
        public bool LockoutEnabled { get; set; }
        
        public DateTimeOffset? LockoutEnd { get; set; }
        
        public bool Active { get; set; }
        
        public IUserType UserType { get; set; }

        public User()
        {
            UserType = new GoldenUserType();
        }
    }
    


    public interface IUserType
    {
        int MaxReservations { get; }

        string ToString();
    }

    public class GoldenUserType : IUserType
    {
        public int MaxReservations => 2;

        public override string ToString()
        {
            return "Golden";
        }
    }
}