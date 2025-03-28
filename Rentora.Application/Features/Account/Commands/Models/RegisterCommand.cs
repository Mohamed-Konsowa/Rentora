using MediatR;
using Microsoft.AspNetCore.Http;
using Rentora.Application.Base;
using System.ComponentModel.DataAnnotations;

namespace Rentora.Application.Features.Account.Commands.Models
{
    public class RegisterCommand : IRequest<Response<string>>
    {
        [Required]
        public IFormFile ProfileImage { get; set; }

        [Required]
        public IFormFile IDImageFront { get; set; }

        [Required]
        public IFormFile IDImageBack { get; set; }

        [Required(ErrorMessage = "FirstName: is required")]
        [MinLength(3, ErrorMessage = "FirstName: must be at least 3 characters long")]
        [MaxLength(20, ErrorMessage = "FirstName: must not exceed 20 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName: is required")]
        [MinLength(3, ErrorMessage = "LastName: must be at least 3 characters long")]
        [MaxLength(20, ErrorMessage = "LastName: must not exceed 20 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName: is required")]
        [MinLength(3, ErrorMessage = "UserName: must be at least 3 characters long")]
        [MaxLength(20, ErrorMessage = "UserName: must not exceed 20 characters")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email: is required")]
        [MaxLength(40, ErrorMessage = "Email: must not exceed 40 characters")]
        public string Email { get; set; }

        [Required]
        public bool EmailConfirmed { get; set; }


        [Required, StringLength(100)]
        public string Password { get; set; }

        [Required, StringLength(14, MinimumLength = 14, ErrorMessage = "NationalID: must be exactly 14 digits")]
        [RegularExpression(@"^\d+$", ErrorMessage = "NationalID: Only numbers are allowed")]
        public string NationalID { get; set; }

        [Required]
        public string Personal_summary { get; set; }

        [Required, StringLength(11, MinimumLength = 11, ErrorMessage = "Phone: number must be exactly 11 digits")]
        [RegularExpression(@"^\d+$", ErrorMessage = "PhoneNumber: Only numbers are allowed for Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Governorate: is required.")]
        [MinLength(4, ErrorMessage = "Governorate must be at least 4 characters long")]
        public string Governorate { get; set; }

        [Required(ErrorMessage = "Town: is required.")]
        public string Town { get; set; }

        [Required(ErrorMessage = "Address: is required.")]
        public string Address { get; set; }
    }
}
