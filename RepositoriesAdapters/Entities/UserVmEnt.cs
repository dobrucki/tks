using System;

namespace RepositoriesAdapters.Entities
{
    public class UserVmEnt
    {
        public string Id { get; set; }
        
        public UserEnt User { get; set; }
        
        public VmEnt VmEnt { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
    }
}