using System.ComponentModel.DataAnnotations;

namespace App.Admin.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The {0} field cannot be left blank!"), DataType(DataType.EmailAddress), EmailAddress, MaxLength(200, ErrorMessage = "The {0} cannot exceed 200 characters."), MinLength(5, ErrorMessage = "The {0} must be at least 5 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} field cannot be left blank!"), DataType(DataType.Password), MaxLength(100, ErrorMessage = "The {0} cannot exceed 100 characters."), MinLength(5, ErrorMessage = "The {0} must be at least 5 characters.")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
