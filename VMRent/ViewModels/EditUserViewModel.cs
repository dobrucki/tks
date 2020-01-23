using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VMRent.ViewModels
{
    public class EditUserViewModel
    {
        public string UserId { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        public bool IsActive { get; set; }

        [Required]
        [RegularExpression(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                           + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                           + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                           + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$")]
        public string Email { get; set; }
        
        [Required]
        [RegularExpression(@"^\+(?:[0-9]●?){6,14}[0-9]$")]
        public string PhoneNumber { get; set; }
    }
}   