using System.ComponentModel.DataAnnotations;

namespace VMRent.ViewModels
{
    public class CreateVmViewModel
    {
        [Required]
        public string Name { get; set; }
        
        public string Type { get; set; }
        
        [Required]
        public string Comment { get; set; }
    }
}