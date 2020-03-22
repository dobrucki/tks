namespace VMRent.DomainModel
{
    public class UserRole
    {
        public string Id { get; set; }
        
        public User User { get; set; }
        
        public Role Role { get; set; }
    }
}