using Microsoft.AspNetCore.Identity;

namespace VMRent.Models
{
    public class Role
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string NormalizedName { get; set; }
    }
}