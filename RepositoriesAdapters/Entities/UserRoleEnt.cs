namespace RepositoriesAdapters.Entities
{
    public class UserRoleEnt
    {
        public string Id { get; set; }
        
        public UserEnt UserEnt { get; set; }
        
        public RoleEnt RoleEnt { get; set; }
    }
}