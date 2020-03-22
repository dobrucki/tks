using System.Collections.Generic;
using VMRent.Models;

namespace VMRent.ViewModels
{
    public class DetailUserViewModel
    {
        public User User { get; set; }
        
        public IList<UserVm> UserVms { get; set; }
    }
}