using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Rentora.Application.DTOs.Authentication
{
    public class RegisterModel
    {
        [Required]
        public IFormFile ProfileImage { get; set; }

        [Required]
        public IFormFile IDImageFront { get; set; }

        [Required]
        public IFormFile IDImageBack { get; set; }
        
        [Required, StringLength(100)]
        public string FirstName { get; set; }
        [Required, StringLength(100)]
        public string LastName { get; set; }
        [Required, StringLength(100)]
        public string Username { get; set; }
        [Required, StringLength(100), EmailAddress]
        public string Email { get; set; }

        public bool EmailConfirmed {  get; set; }
        [Required, StringLength(100)]
        public string Password { get; set; }
        [Required]
        public string NationalID { get; set; }
        [Required]
        public string Personal_summary { get; set; }
        [Required, Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Governorate { get; set; }
        [Required]
        public string Town { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
