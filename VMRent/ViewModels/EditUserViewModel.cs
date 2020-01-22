using System.Collections.Generic;

namespace VMRent.ViewModels
{
    public class EditUserViewModel
    {
        public string UserId { get; set; }
        
        public string UserName { get; set; }
        
        public bool IsActive { get; set; }

        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
    }
}   