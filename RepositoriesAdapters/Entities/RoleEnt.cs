using Microsoft.AspNetCore.Identity;

namespace RepositoriesAdapters.Entities
{
    public class RoleEnt
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string NormalizedName { get; set; }
    }
}