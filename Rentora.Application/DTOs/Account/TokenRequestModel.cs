using System.ComponentModel.DataAnnotations;

namespace Rentora.Application.DTOs.Account
{
    public class TokenRequestModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
