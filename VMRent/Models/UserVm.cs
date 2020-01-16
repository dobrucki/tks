using System;

namespace VMRent.Models
{
    public class UserVm
    {
        public string Id { get; set; }
        
        public User User { get; set; }
        
        public Vm Vm { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
    }
}