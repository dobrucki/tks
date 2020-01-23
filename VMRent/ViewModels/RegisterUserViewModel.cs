using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace VMRent.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [RegularExpression(@"^\+(?:[0-9]●?){6,14}[0-9]$", ErrorMessage = "Wrong phone number")]
        public string PhoneNumber { get; set; }
        
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,15}$", ErrorMessage = "Wrong password")]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string PasswordConfirmed { get; set; }
        
        
    }
}