using System.Collections.Generic;
using VMRent.DomainModel;

namespace VMRent.ViewModels
{
    public class ListUserViewModel
    {
        public IList<User> Users { get; set; }
    }
}