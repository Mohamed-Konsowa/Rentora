using System.ComponentModel.DataAnnotations;

namespace Rentora.Presentation.DTOs.Authentication
{
    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
