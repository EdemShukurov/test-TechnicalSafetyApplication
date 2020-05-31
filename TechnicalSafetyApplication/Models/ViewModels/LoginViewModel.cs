using System.ComponentModel.DataAnnotations;

namespace TechnicalSafetyApplication.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}
