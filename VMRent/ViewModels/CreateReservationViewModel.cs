using System;

namespace VMRent.ViewModels
{
    public class CreateReservationViewModel
    {
        public string VmId { get; set; }
        
        public DateTime? StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
    }
}