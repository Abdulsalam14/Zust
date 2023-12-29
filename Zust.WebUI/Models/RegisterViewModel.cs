using System.ComponentModel.DataAnnotations;

namespace Zust.WebUI.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "You must accept the privacy policy.")]
        [Display(Name = "I accept the privacy policy")]
        public bool AcceptPrivacy { get; set; }
    }
}
