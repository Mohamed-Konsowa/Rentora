using MediatR;
using Microsoft.AspNetCore.Http;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Account.Commands.Models
{
    public class RegisterCommand : IRequest<Response<string>>
    {
        public IFormFile? ProfileImage { get; set; }
        public IFormFile? IDImageFront { get; set; }
        public IFormFile? IDImageBack { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string? Password { get; set; }
        public string? NationalID { get; set; }
        public string? Personal_summary { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Governorate { get; set; }
        public string? Town { get; set; }
        public string? Address { get; set; }
    }
}
