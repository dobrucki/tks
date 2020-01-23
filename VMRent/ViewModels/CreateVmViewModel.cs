using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace VMRent.ViewModels
{
    public class CreateVmViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Type { get; set; }
        
        public string Comment { get; set; }
    }
}