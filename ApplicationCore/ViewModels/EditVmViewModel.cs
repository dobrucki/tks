using System.ComponentModel.DataAnnotations;

namespace VMRent.ViewModels
{
    public class EditVmViewModel
    {
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }    
        
        public string Comment { get; set; }
    }
}