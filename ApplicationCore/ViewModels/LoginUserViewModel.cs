using System.ComponentModel.DataAnnotations;

namespace VMRent.ViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Username is required. ")]
        public string Username { get; set; }    
        
        [Required(ErrorMessage = "Password is required. ")]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}