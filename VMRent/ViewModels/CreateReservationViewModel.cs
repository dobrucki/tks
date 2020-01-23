using System;
using System.ComponentModel.DataAnnotations;

namespace VMRent.ViewModels
{
    public class CreateReservationViewModel
    {
        public string VmId { get; set; }
        
        [Required]
        public DateTime? StartTime { get; set; }
        
        [Required]
        public DateTime EndTime { get; set; }
    }
}