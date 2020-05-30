using System.ComponentModel.DataAnnotations;

namespace TechnicalSafetyApplication.Models.ViewModels
{
    public class CreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
