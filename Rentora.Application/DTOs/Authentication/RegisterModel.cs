using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Rentora.Application.DTOs.Authentication
{
    public class RegisterModel
    {
        [Required]
        public IFormFile ProfileImage { get; set; }
        [Required, StringLength(100)]
        public string FirstName { get; set; }
        [Required, StringLength(100)]
        public string LastName { get; set; }
        [Required, StringLength(100)]
        public string Username { get; set; }
        [Required, StringLength(100)]
        public string Email { get; set; }
        [Required, StringLength(100)]
        public string Password { get; set; }
        public string? NationalID { get; set; }
        public string? Personal_summary { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Governorate { get; set; }
        public string? Town { get; set; }
        public string? Address { get; set; }
    }
}
